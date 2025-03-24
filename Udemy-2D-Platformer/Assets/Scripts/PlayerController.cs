using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float moveSpeed;
    public float jumpForce;
    private Rigidbody2D rb;
    private bool isGrounded;
    private float horizontalInput;
    public int maxJump = 2;
    private int currentJump;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    public CameraController cameraController;
    private bool isHeightTriggered;

    public float knockBackLenght, knockBackForce;
    private float knockBackCounter;
    public float bounceForce;

    public bool stopInput;

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Cache the Rigidbody2D component
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isHeightTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.instance.pauseScreen.active && !stopInput)
        {
            if (knockBackCounter <= 0)
            {
                // Cache the input each frame to avoid calling it multiple times
                horizontalInput = Input.GetAxis("Horizontal");

                // Check if the player pressed the jump button and if they are grounded
                if (Input.GetButtonDown("Jump") && (isGrounded || currentJump < maxJump))
                {
                    AudioManager.instance.PlaySFX(10);
                    rb.velocity = new Vector2(rb.velocity.x, 0);

                    // Jump with an impulse force for a more natural jump
                    rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                    currentJump++;
                }

            }
        }
    }

    void FixedUpdate()
    {
        if (!PauseMenu.instance.pauseScreen.active && !stopInput)
        {
            if (knockBackCounter <= 0)
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
            }
            else
            {
                knockBackCounter = knockBackCounter - Time.deltaTime;
                if (!spriteRenderer.flipX)
                {
                    rb.velocity = new Vector2(-knockBackForce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(knockBackForce, rb.velocity.y);
                }
            }

            anim.SetBool("isGrounded", isGrounded);
            anim.SetFloat("moveSpeed", Mathf.Abs(rb.velocity.x));
        }
    }

    // Assuming you have a way to check if the player is grounded
    void OnCollisionEnter2D(Collision2D collision)
    {
        
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


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("HTrigger"))
        {
            Debug.Log("Usao u trigger");
            cameraController.maxHeight = cameraController.maxHeight + 100;
        }
    }
    
    public void KnockBack()
    {
        knockBackCounter = knockBackLenght;
        rb.velocity = new Vector2(0f,knockBackForce);

        anim.SetTrigger("hurt");
    }

    public void Bounce()
    {
        rb.velocity = new Vector2(rb.velocity.x,bounceForce);
        AudioManager.instance.PlaySFX(9);

    }

    public void StopPlayer()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        anim.SetTrigger("isEnd");
    }
}
