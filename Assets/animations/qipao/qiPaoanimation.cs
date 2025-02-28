using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class qiPaoanimation : MonoBehaviour
{
    public Animator animationqipao;
    public Loveinput inputs;
    public bool ispush;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
       inputs.Enable();

    }
    private void OnDisable()
    {
        inputs.Disable();
    }

    private void Awake()
    {
        animationqipao = GetComponent<Animator>();
        inputs = new Loveinput();
        inputs.Love.love.started += SetLove;
        inputs.Love.zhengjin.started += SetZhenjin;
        inputs.Love.music.started += SetMusic;
    }
    void SetZhenjin(InputAction.CallbackContext context)
    {
        animationqipao.Play("zhengjin");
    }

    // 
    // Update is called once per frame
    void Update()
    {
        
        //animationqipao.SetBool("love", false);
    }

    void SetLove(InputAction.CallbackContext context)
    {
        ispush= true;
        animationqipao.SetBool("love", true);
        animationqipao.Play("qipao");
       
    }   
    void SetMusic(InputAction.CallbackContext context)
        {
            animationqipao.Play("music");

        } 


    }
