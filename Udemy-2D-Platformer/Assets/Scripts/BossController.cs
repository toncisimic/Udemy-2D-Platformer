using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public enum bossStates { shooting, hurt, moving};

    public bossStates currentState;
    public Transform theBoss;
    public Animator anim;
    public int bossHealt;
    public int currentHealt;
    public GameObject deathEffect, door;
    private Animator doorAnimator;

    [Header("Movement")]
    public float moveSpeed;
    public Transform lefPoint, rightPoint;
    private bool moveRight;

    [Header("Shooting")]
    public GameObject bullet;
    public float timeBetweenShots;
    private float shotCounter;
    public Transform firePoint;

    [Header("Hurt")]
    public float hurtTime;
    private float hurtCounter;
    public GameObject hitBox;

    // Start is called before the first frame update
    void Start()
    {
        currentHealt = bossHealt;
        currentState = bossStates.shooting;
        doorAnimator = door.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState){
            case bossStates.shooting:

                shotCounter -= Time.deltaTime;

                if( shotCounter <= 0)
                {
                    shotCounter = timeBetweenShots;

                    var newBullet = Instantiate(bullet,firePoint.position,firePoint.rotation);
                    newBullet.transform.localScale = theBoss.transform.localScale;
                    newBullet.tag = "EnemyBullet";
                }

                break;
            case bossStates.hurt:

                if (hurtCounter > 0f)
                {
                    hurtCounter = Mathf.Max(hurtCounter - Time.deltaTime, 0f);

                    if (hurtCounter == 0f)
                        currentState = bossStates.moving;
                }

                break;
            case bossStates.moving:
                
                if (moveRight)
                {
                    theBoss.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                    if(theBoss.position.x > rightPoint.position.x)
                    {
                        theBoss.localScale = new Vector3(1f, 1f, 1f);
                        moveRight = false;

                        currentState = bossStates.shooting;

                        shotCounter = timeBetweenShots;

                        anim.SetTrigger("StopMoving");

                        hitBox.SetActive(true);
                    }
                } else
                {
                    theBoss.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                    if (theBoss.position.x < lefPoint.position.x)
                    {
                        theBoss.localScale = new Vector3(-1f, 1f, 1f);

                        moveRight = true;

                        currentState = bossStates.shooting;

                        shotCounter = timeBetweenShots;

                        anim.SetTrigger("StopMoving");

                        hitBox.SetActive(true);

                    }
                }

                    break;
        }
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.H)) {
            TakeHit();
        }
#endif
    }


    public void TakeHit()
    {
        currentState = bossStates.hurt;
        hurtCounter = hurtTime;

        currentHealt--;

        if (currentHealt <= 0) {
            Instantiate(deathEffect, theBoss.position, theBoss.rotation);
            gameObject.SetActive(false);

            GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
            Debug.Log("Dosao do bullet");
            foreach (GameObject bullet in enemyBullets)
            {
                Destroy(bullet);
            }

            AudioManager.instance.PlaySFX(8);
            PlayerController.instance.Bounce();
            doorAnimator.SetTrigger("openDoor");
        }

        anim.SetTrigger("Hit");
    }
}
