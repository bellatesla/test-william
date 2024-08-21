using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameConditionManager : MonoBehaviour
{

    [Header("Game Setting per level")]
    public string newSceneToLoad;
    public int platformDestroyAmount = 1;
    private EndUiManager endUiManager;
    public int currentLevel = 1;
    [Header("Set up for delay time")]
    public int nextSceneDelaySeconds = 3;


    void Start()
    {
        endUiManager = GetComponent<EndUiManager>();
    }

    //UI button
    public void LoadNextScene()
    {
        SceneManager.LoadScene(newSceneToLoad);
    }

    //UI Button
    public void LoadLevelSelectScene()
    {
        LevelSelectManagement levelManager = FindObjectOfType<LevelSelectManagement>();
        Destroy(levelManager.gameObject);

        SceneManager.LoadScene("LevelSelect");
    }

    public void RestartScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void PlatformDestroyed()
    {
        platformDestroyAmount -= 1;

        if (platformDestroyAmount <= 0 ) 
        {
           // UnlockedLevel(); //not used
           StartCoroutine(LoadSceneDelayed(nextSceneDelaySeconds));
           endUiManager.ActivateUi();
        }
    }


    /// <summary>
    /// Not Used today
    /// </summary>
    void UnlockedLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }

    IEnumerator LoadSceneDelayed(int seconds)
    {
        yield return new WaitForSeconds( seconds );
        LoadNextScene();
    }    

    
}
