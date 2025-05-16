using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreLoadScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadNextSceneAfterDelay(20f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Level_1-1"); // zamijeni "ImeScene" stvarnim imenom tvoje scene
    }
}
