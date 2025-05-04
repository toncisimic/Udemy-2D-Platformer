using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float waitToRespawn;
    public int gemsCollected;
    public int foodCollected;

    private void Awake()
    {
        instance = this;    
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo());
    }

    private IEnumerator RespawnCo()
    {
        PlayerController.instance.gameObject.SetActive(false);
        AudioManager.instance.PlaySFX(8);
    
        yield return new WaitForSeconds(waitToRespawn - (1f / UIController.instance.fadeSpeed));

        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + .2f);

        UIController.instance.FadeFromBlack();

        PlayerController.instance.gameObject.SetActive(true);

        PlayerController.instance.transform.position = CheckpointBehaviorController.instance.spawnPoint;

        PlayerHealtControler.instance.currentHealth = PlayerHealtControler.instance.maxHealth;

        UIController.instance.UpdateHealthDisplay();


    }

    public void EndLevelF()
    {
        StartCoroutine(EndLevelCo());
    }

    public IEnumerator EndLevelCo()
    {
        PlayerController.instance.stopInput = true;

        yield return new WaitForSeconds(1.5f);

        CameraController.instance.follow = true;
        PlayerController.instance.StopPlayer();

        yield return new WaitForSeconds(1.5f);

        EndLevel.instance.StartEndAnimation();
    }


}
