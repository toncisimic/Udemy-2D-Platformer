using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{

    private bool playerInZone = false;
    private bool switchStatus = false;

    public GameObject GameObject;

    private Animator doorAnimator;

    private SpriteRenderer sr;

    public Sprite switchOn, switchOff;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        doorAnimator = GameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInZone && Input.GetKeyDown(KeyCode.E))
        {
            ActivateSwitch();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            Debug.Log("Igrač ušao u zonu");
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


    private void ActivateSwitch()
    {
        sr.sprite = switchOn;

        doorAnimator.SetTrigger("openDoor");

        Debug.Log($"Tipka E pritisnuta u zoni – nešto se događa! Switch je {switchStatus}");
    }
}
