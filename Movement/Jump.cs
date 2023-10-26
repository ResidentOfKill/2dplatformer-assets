using System;
using UnityEngine;



public class Jump : MonoBehaviour
{
    
    //todo feature doublejump / boost is in comments 
    private float horizontal;
    [SerializeField] private float jumpPower = 2f;
    [SerializeField] private float mass = 1f;
    [SerializeField] private AnimationCurve jumpDampMod;
    [SerializeField] private int doubleJumpAmount = 1;

    // [SerializeField] private float boostMaxTime = 3f ;
    // [SerializeField] private float boost = 1f;
    // [SerializeField] private int boostAmount = 1;
    
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private int doubleJumpAmountCurrent;
    private Rigidbody2D rb;
    private float jumpUpTime;
    // private float boostTime;
    // private int boostAmountCurrent = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.mass = mass;
        rb.mass = rb.mass; // mass needs to get called to be actually set https://discussions.unity.com/t/rigidbody-mass-not-updating-in-inspector-after-calling-setdensity/84755

        var velocity = rb.velocity;
        jumpUpTime += Time.deltaTime;
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpUpTime = 0f;
            rb.AddForce(new Vector2(velocity.x, -Physics2D.gravity.y * jumpPower));
           
        }
        // boostTime += Time.deltaTime;
        // if (Input.GetButton("Jump") && !IsGrounded())
        // {
        //     if (boostAmountCurrent != boostAmount)
        //     {
        //         boostTime = 0f;
        //         // rb.velocity = new Vector2(velocity.x ,0);
        //         // rb.AddForce(new Vector2(velocity.x, -Physics2D.gravity.y * jumpPower));
        //         rb.AddForce(new Vector2(velocity.x, -Physics2D.gravity.y * boost.Evaluate(Mathf.Clamp(boostTime,0,boostMaxTime))));
        //         if (fuelCharge == 0)
        //         {
        //             boostAmountCurrent++;
        //         } 
        //     }
        //     
        // }
        if (Input.GetButtonDown("Jump") && !IsGrounded()) 
        {
            if (doubleJumpAmount > doubleJumpAmountCurrent)
            {
                doubleJumpAmountCurrent++;
                rb.velocity = new Vector2(rb.velocity.x,0f);
                rb.AddForce(new Vector2(velocity.x, -Physics2D.gravity.y * jumpPower));
            }
            
        }
        
        if (rb.velocity.y == 0 && IsGrounded())
        {
            doubleJumpAmountCurrent = 0;
            // boostAmountCurrent = 0;
        }
        
        
        if (rb.velocity.y == 0)
        {

        } 

        if (rb.velocity.y >= 0 && !IsGrounded())
        {

            rb.gravityScale = jumpDampMod.Evaluate(Mathf.Clamp(jumpUpTime,0,2));
        }
        
        
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }
}
