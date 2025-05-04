using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacaLukasContrller : MonoBehaviour
{
    [Header("Patrol")]
    public float moveSpeed;
    public Transform endPoint, startPoint;
    private Rigidbody2D rb;
    public GameObject deadthEffect;

    private Animator animator;

    public GameObject collectible;

    public bool canCatMove =false;

    public bool killAtEndpoint = true;
    public bool killEnemy = true;

    private bool playerInZone = false;
    private bool switchStatus = false;

    private bool movingRight = true;

    [Range(0, 100)] public float chanceToDrop;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        endPoint.parent = null;
        startPoint.parent = null;

        if (canCatMove) animator.SetTrigger("canCatMove");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInZone && Input.GetKeyDown(KeyCode.E) && LevelManager.instance.foodCollected > 0)
        {
            canCatMove = true;
            LevelManager.instance.foodCollected--;
            UIController.instance.UpdateGemCountDisplay();
            animator.SetTrigger("canCatMove");
        }

        if (canCatMove)
        {
            if (movingRight)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

                if (transform.position.x > endPoint.position.x)
                {
                    if (killAtEndpoint)
                    {
                        transform.gameObject.SetActive(false);

                        Instantiate(deadthEffect, transform.position, transform.rotation);
                        AudioManager.instance.PlaySFX(3);
                    }
                    else
                    {
                        movingRight = false;
                        Flip();
                    }
                }
            }
            else
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);

                if (transform.position.x < startPoint.position.x)
                {
                    movingRight = true;
                    Flip();
                }
            }
        }
    }

private void Flip()
{
    Vector3 localScale = transform.localScale;
    localScale.x *= -1;
    transform.localScale = localScale;
}

private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            Debug.Log("Igrač ušao u zonu");
        }

        if (other.CompareTag("Enemy"))
        {
            if (killEnemy)
            {
                other.transform.parent.gameObject.SetActive(false);

                Instantiate(deadthEffect, other.transform.position, other.transform.rotation);

                float dropSelect = Random.Range(0, 100f);

                if (dropSelect <= chanceToDrop)
                {
                    Instantiate(collectible, other.transform.position, other.transform.rotation);
                }

                AudioManager.instance.PlaySFX(3);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            Debug.Log("Igrač napustio zonu");
        }
    }
}
