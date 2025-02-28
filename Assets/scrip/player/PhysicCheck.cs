using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicCheck : MonoBehaviour
{
    private Rect rect1;

    [Header("×´Ì¬")]
    public bool isOnGround;
    public bool isOnWall;

    [Header("¼ì²â²ÎÊý")]
    public float checkRadius;
    public Vector2 checkWall;
    public Vector2 leftOffset;
    public Vector2 budyOffset;
    public LayerMask groundLayer;
    public Vector2 bottomOffset;
    // Start is called before the first frame update
    void Start()
    {
        rect1 = new Rect(transform.position.x+budyOffset.x,transform.position.y+budyOffset.y,checkWall.x,checkWall.y);
    }

    // Update is called once per frame
    public void Update()
    {
        check();    
    }

    public void check() {
       isOnGround = Physics2D.OverlapCircle((Vector2)transform.position+bottomOffset, checkRadius, groundLayer);
        //isOnWall = Physics2D.OverlapBox((Vector2)transform.position + budyOffset,checkWall, 0,groundLayer) ;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRadius);
        //Gizmos.DrawWireCube((Vector2)transform.position + leftOffset, checkWall);
        //Gizmos.DrawWireCube((Vector2)transform.position + budyOffset,checkWall);

    }
}
