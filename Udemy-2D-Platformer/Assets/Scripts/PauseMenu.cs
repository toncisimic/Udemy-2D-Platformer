using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;   
    public string scene;

    public GameObject pauseScreen;

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
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseUnpause();
        }
    }

    public void PauseUnpause()
    {
        if (pauseScreen.active)
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;

        }
        else if (!pauseScreen.active) 
        {
            Debug.Log(pauseScreen.ToString());
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ChangeScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }
}
