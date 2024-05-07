using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneGerante : MonoBehaviour
{
    public static SceneGerante instance = null;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void RestartLevel()
    {
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex; //When retry works, main menu doesnt.I tried to fix it but it didnt want to work
        SceneManager.LoadScene(currentBuildIndex);                        // and vice versa, yesterday they both worked, and my time victory screen display as well as
        Cursor.visible = false;                                           // the water remaining % display stopped showing and just shows the default i wrote....when
        Cursor.lockState = CursorLockMode.Locked;                         //earlier today that was working.No idea why its bugging out. If you figure it out i'd love to
    }                                                                     //know why.Thanks!
                                                                          

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoLoadLevels(int levelIndex)
    {
        int buildIndex = levelIndex - 1;
        Scene scene = SceneManager.GetSceneByBuildIndex(buildIndex);

        if (scene == null) return;
      
        SceneManager.LoadScene(buildIndex);
    }

    public void GoLoadNextLevel()
    {
        int nextBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;
    
        if (nextBuildIndex < SceneManager.sceneCountInBuildSettings) SceneManager.LoadScene(nextBuildIndex);
        else
        {
            GameManager.instance.EndGameSuccess();
            return;
        }
    }

    public int GetLevelIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
