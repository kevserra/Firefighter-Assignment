using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingGameMethod : MonoBehaviour
{
    public void applyStartGame()
    {
        SceneGerante.instance.GoLoadNextLevel();
    }
    
}
