using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class npc1 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject npc1Sprite;
    private Animator anim;
    public bool canPress=false;
    [Multiline(20)]
    public string text1;
    public Text text2;
    public InputControls controls1;
    public int num=0;
    public Sprite sprite1;
    public dialogue dialogue1;
    public GameObject canvas;
    public Dictionary<string, Sprite> spritedic;
    public Sprite[] sprites;

    public GameObject prefab;
    public SpriteList spriteList;
    public voidEventSO dialogueSO;

    void Start()
    {
        canvas=dialogue.instance.transform.gameObject;
    }
    private void Awake()
    {
        spritedic = new Dictionary<string, Sprite>();
        spriteList=UnityEditor.AssetDatabase.LoadAssetAtPath<SpriteList>("Assets/setting/SpriteList.asset");
        spritedic=spriteList.spritedic;
        controls1=new InputControls();
        controls1.UI.talk.started += talk;
        anim =npc1Sprite.GetComponent<Animator>();
        //dialogue1 = GetComponentInParent<dialogue>();
        canPress = false;
        npc1Sprite.SetActive(false);
        
        
        
    }

    private void talkover()
    {
       

        InventoryManager.instance.addItem(prefab);
        
        dialogueSO.onEventRaised -= talkover;
    }

    // Update is called once per frame
    private void OnEnable()
    {
        controls1.Enable();
    }

    private void OnDisable()
    {
        controls1.Disable();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player")) { 
            canPress = true;
            
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPress = false;
            if(canvas.activeInHierarchy)
            canvas.SetActive(false);
        }
    }
    private void talk()
    {
      
        
            //canvas.SetActive(true);
           //SetCoversation(transform.parent.GetComponent<Sprite>(), "npc1", text1, false);
        
    }
    private void talk(InputAction.CallbackContext context)
    {
        
        num += 1;
        if (canPress)
        {
            canPress = false;
            //Debug.Log("talk");
            canvas.SetActive(true);
            if (!spritedic.ContainsKey("星愿"))
                spritedic.Add("星愿",sprites[0]);
            if (!spritedic.ContainsKey("村长"))
                spritedic.Add("村长", sprites[1]);
            //SetActive(true);
            dialogue.instance.SetCoversation(spritedic,text1, false);
            dialogueSO.onEventRaised += talkover;
 
        }
    }
    void Update()
    {
        
        npc1Sprite.SetActive(canPress);
    }
}
