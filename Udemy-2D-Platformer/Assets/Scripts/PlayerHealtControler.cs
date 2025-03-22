using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerHealtControler : MonoBehaviour
{
    public int currentHealth, maxHealth;
    public static PlayerHealtControler instance;

    public float invincibleLenght;
    private float invincibleCounter;

    private SpriteRenderer SpriteRenderer;
    public GameObject pickupEffect;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleCounter > 0) {
            invincibleCounter = invincibleCounter - Time.deltaTime;
        } else
        {
            SpriteRenderer.color = new Color(SpriteRenderer.color.r, SpriteRenderer.color.g, SpriteRenderer.color.b, 1);
        }
    }

    public void DealDamage()
    {
        if (invincibleCounter <= 0) {
            currentHealth--;
            

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                //gameObject.SetActive(false);

                Instantiate(pickupEffect, transform.position, transform.rotation);

                LevelManager.instance.RespawnPlayer();
            } else
            {
                invincibleCounter = invincibleLenght;
                SpriteRenderer.color = new Color(SpriteRenderer.color.r, SpriteRenderer.color.g, SpriteRenderer.color.b, 0.5f);
                PlayerController.instance.KnockBack();

                AudioManager.instance.PlaySFX(9);
            }

            UIController.instance.UpdateHealthDisplay();
        }
    }

    public void HealPlayer()
    {
        currentHealth++;

        if ((currentHealth > maxHealth)) currentHealth = maxHealth;
        UIController.instance.UpdateHealthDisplay();

    }

}
