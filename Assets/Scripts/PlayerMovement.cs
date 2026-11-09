using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    InputAction moveInput;
    InputAction jumpInput;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    Animator anim;

    [SerializeField] float moveSpeed = 5;
    [SerializeField] float maxSpeed = 5;
    [SerializeField] float jumpForce = 10;
    [field:SerializeField] public int numOfJumps {get; set;} = 1;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        moveInput = InputSystem.actions.FindAction("Move");
        jumpInput = InputSystem.actions.FindAction("Jump");
    }

    void Update()
    {
        if(numOfJumps > 0 && jumpInput.WasPressedThisFrame())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            numOfJumps--;
        }

        Vector2 moveVect = moveInput.ReadValue<Vector2>();
        if(moveVect.x > 0)
        {
            sprite.flipX = false;
        }
        else if(moveVect.x < 0)
        {
            sprite.flipX = true;
        }

        if(moveVect.x != 0)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
    }

    void FixedUpdate()
    {
        Vector2 moveVect = moveInput.ReadValue<Vector2>();
        rb.AddForce(new Vector2(moveVect.x, 0) * moveSpeed);                //Move

        Vector2 velXNormalized = rb.linearVelocity;                         //Normalize X ONLY
        velXNormalized.x = velXNormalized.x / velXNormalized.magnitude;     

        if(rb.linearVelocityX > maxSpeed || rb.linearVelocityX < -maxSpeed) // Cap speed
        {
            rb.linearVelocity = new Vector2(velXNormalized.x * maxSpeed, velXNormalized.y);
        }
    }
}
