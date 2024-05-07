using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuittingGameMethod : MonoBehaviour
{
    public void applyExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
