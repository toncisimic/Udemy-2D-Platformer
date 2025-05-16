using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StumpBox : MonoBehaviour
{
    public GameObject deadthEffect;

    public GameObject collectible;
    [Range(0, 100)]public float chanceToDrop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check for enemy contact - original functionality
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponentInParent<EnemyController>();

            Debug.Log("Hit Enemy");

            other.transform.parent.gameObject.SetActive(false);

            Instantiate(deadthEffect, other.transform.position, other.transform.rotation);
            
            PlayerController.instance.Bounce();

            float dropSelect = Random.Range(0, 100f);

            if(dropSelect <= chanceToDrop)
            {
                Instantiate(collectible,other.transform.position, other.transform.rotation);
            }

            AudioManager.instance.PlaySFX(3);
        }
        // Check for ground contact - new functionality similar to DoubleJump
        else if (other.CompareTag("Ground") || other.CompareTag("Platform"))
        {
            // Set player as grounded and reset jump counter
            PlayerController.instance.isGrounded = true;
            PlayerController.instance.ResetJumpCounter();
            
            // If it's a platform, set parent
            if (other.CompareTag("Platform"))
            {
                transform.parent = other.transform;
            }
        }
    }



    void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the collision is with the ground
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            PlayerController.instance.isGrounded = false;
        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.parent = null;
        }
    }
}
