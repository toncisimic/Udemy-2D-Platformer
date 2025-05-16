using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossMusic : MonoBehaviour
{
    public AudioClip bossMusic; // dodaj boss glazbu u Inspectoru
    private AudioSource musicSource;
    private bool isStarted  = false;
    // Start is called before the first frame update
    void Start()
    {
        GameObject musicObject = GameObject.Find("MainMusic");
        if (musicObject != null)
        {
            musicSource = musicObject.GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isStarted)
            {
                Debug.Log("Boss trigger aktiviran – glazba se mijenja");
                musicSource.Stop();
                musicSource.clip = bossMusic;
                musicSource.Play();
                isStarted = true;
            }
        }
    }
    }
