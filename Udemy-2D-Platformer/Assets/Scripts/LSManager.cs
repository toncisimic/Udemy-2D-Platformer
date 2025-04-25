using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LSManager : MonoBehaviour
{
    public LevelPalyer thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadlLevelCo());
    }

    public IEnumerator LoadlLevelCo()
    {
        LSUIController.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / LSUIController.instance.fadeSpeed)*.25f);

        LSUIController.instance.FadeFromBlack();

        SceneManager.LoadScene(thePlayer.currentPoint.levelToLoad);
    }
}
