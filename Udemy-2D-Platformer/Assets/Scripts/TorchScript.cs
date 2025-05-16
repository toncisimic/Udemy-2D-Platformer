using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite torchGreen,torchRed;
    public bool isRed = false;
    public SpriteRenderer sr;
    public bool playerInZone = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInZone && Input.GetKeyDown(KeyCode.E))
        {
            if (isRed)
            {
                sr.sprite = torchGreen;
                isRed = false;
            }
            else
            {
                sr.sprite = torchRed;
                isRed = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            
        }
    }
}
