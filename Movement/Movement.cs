using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float dashCoolDown = 1f;
    
    private Vector2 _movementInput;
    private Rigidbody2D rb;
    private float horizontal;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    public void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        PlayerMovement();


    }

    private void PlayerMovement()
    {
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }
}
