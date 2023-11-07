using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    private Rigidbody2D rb;
    private bool isGrounded;
    private float horizontalInput;
    public int maxJump = 2;
    private int currentJump;
    private Animator anim;
    private SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Cache the Rigidbody2D component
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Cache the input each frame to avoid calling it multiple times
        horizontalInput = Input.GetAxis("Horizontal");

        // Check if the player pressed the jump button and if they are grounded
        if (Input.GetButtonDown("Jump") && (isGrounded || currentJump < maxJump))
        {

            rb.velocity = new Vector2(rb.velocity.x, 0);

            // Jump with an impulse force for a more natural jump
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            currentJump++;
        }

      
    }

    void FixedUpdate()
    {
        // Move the player using MovePosition for precise control
        Vector2 targetVelocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        rb.velocity = targetVelocity;

        if (rb.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (rb.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("moveSpeed", Mathf.Abs(rb.velocity.x));
    }

    // Assuming you have a way to check if the player is grounded
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Usao u colision");
        // Check if the collision is with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            currentJump = 0;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the collision is with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
