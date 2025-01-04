using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    
    public InputControls inputControl;
    public bool isinput;
    public Vector2 playerDerection;
    private Rigidbody2D rb;
    private PhysicCheck check;
    private Animator animator;
    private qiPaoanimation qipaoanimation;
    [Header("��������")]
    public float speed=290;
    public float jumpForce;
    public GameObject died_img;
    [Header("��¼����")]
    public int died_times=0; 
    // Start is called before the first frame update
    private void Awake()
    {
        //speed = 290;
        inputControl = new InputControls();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        check = GetComponent<PhysicCheck>();
        qipaoanimation = GetComponentInChildren<qiPaoanimation>();
        inputControl.Player.jump.started += jump;
        inputControl.Player.Love.started += Love;
        inputControl.Player.ItemUse.started += ItemUse;
        inputControl.UI.inventory.started += inventoryChange;
        //died_img.SetActive(false);
    }

    private void ItemUse(InputAction.CallbackContext context)
    {
                

        try {
            string text = InventoryManager.instance.thisobject.GetComponentInChildren<Item>().itemDetail.description;
            Debug.Log(text);
            dialogue.instance.SetCoversation("星愿:" + text,false);
        }

        catch (Exception)
        {

            Debug.Log("这里没有物品");
        }
    }

    private void inventoryChange(InputAction.CallbackContext context)
    {
        int number=0;
        do{
        InventoryManager.instance.index +=(int)inputControl.UI.inventory.ReadValue<float>();
        
        if(InventoryManager.instance.index<0){
            InventoryManager.instance.index+=8;
        }
        InventoryManager.instance.index%=8;
        number++;
        }
        while(!InventoryManager.instance.items[InventoryManager.instance.index].GetComponentInChildren<Item>()||number>8);
    }

    IEnumerable OnWaitMethod()
    {   
        yield return new WaitForSeconds(3); 
    
    }
    private void Love(InputAction.CallbackContext context)
    {
        qipaoanimation.animationqipao.Play("qipao"); 
    }

    private void OnEnable()
    {
        inputControl.Enable();

    }
    private void OnDisable()
    {
        inputControl.Disable();
    }
    void Start()
    {
        
    }
    public void go() {
        inputControl.Enable();
        isinput = true;
    }
    public void pause()
    {
        inputControl.Disable();
        isinput=false;
    }
    // Update is called once per frame
    void Update()
    {
        playerDerection = inputControl.Player.Move.ReadValue<Vector2>();
        
    }
    private void Wite() {
        died_img.SetActive(false);
    }
    private void FixedUpdate()
    {
        Vector3 vector3 = new Vector3(15,-170,0);
        vector3.y = -5;
        
        if (transform.position.y < -100)
        {
            //died_img.SetActive(true);
            transform.position = vector3;
            died_times += 1;
            Invoke("Wite", 3f);
            
        }
        Move();
    }
    private SpriteRenderer sr;

    public void Move()
    {
       
        rb.velocity = new Vector2(playerDerection.x*Time.deltaTime*speed,rb.velocity.y);
        int facedir = (int)transform.localScale.x;
        sr=GetComponent<SpriteRenderer>();
        if (rb.velocity.x > 0) 
            sr.flipX = true;
        if(rb.velocity.x<0)
            sr.flipX = false;

    }
    private void jump(InputAction.CallbackContext context)
    {
        //throw new NotImplementedException();
        //Debug.Log("jump");
        if (check.isOnGround)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
    //public void jump() {
    //    rb.AddForce(transform.up * jumpForce,ForceMode.Impulse);
    //}
   
}
