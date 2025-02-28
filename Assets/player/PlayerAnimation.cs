using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animationplayer;
    
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        
        animationplayer= GetComponent<Animator>();
        rb= GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        SetAnimation();      
    }
    void SetAnimation()
    {
        animationplayer.SetFloat("velocityX",Mathf.Abs(rb.velocity.x));
        
    }
}
