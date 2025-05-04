using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isGem, isHeal, isFood;
    private bool isCollected;
    public GameObject pickupEffect;

    private bool canBeCollected = false;
    public float delayBeforePickup = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(EnableCollection), delayBeforePickup);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EnableCollection()
    {
        canBeCollected = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected && canBeCollected)
        {
            if (isGem)
            {
                LevelManager.instance.gemsCollected++;

                isCollected = true;

                Destroy(gameObject);

                Instantiate(pickupEffect, transform.position, transform.rotation);

                UIController.instance.UpdateGemCountDisplay();

                AudioManager.instance.PlaySFX(6);
            }

            if (isHeal)
            {
                if(PlayerHealtControler.instance.currentHealth != PlayerHealtControler.instance.maxHealth)
                {
                    PlayerHealtControler.instance.HealPlayer();

                    isCollected = true;
                    Destroy(gameObject);

                    Instantiate(pickupEffect, transform.position, transform.rotation);

                    AudioManager.instance.PlaySFX(7);

                }
            }

            if (isFood) {

                LevelManager.instance.foodCollected++;

                isCollected = true;

                Destroy(gameObject);

                Instantiate(pickupEffect, transform.position, transform.rotation);

                UIController.instance.UpdateGemCountDisplay();

                AudioManager.instance.PlaySFX(6);

            }
        }
    }
}
