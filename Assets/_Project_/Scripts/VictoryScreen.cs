using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text timeStatText;
    [SerializeField] private TMP_Text waterStatText;
    [SerializeField] private TMP_Text damageStatText;
    [SerializeField] private TMP_Text scoreStatText;

    private ScoreCache scoreData;

    private void OnEnable() //when the object attached to this is enabled, this code will exe = prints the sscore for that object
    {
        float gameTime = scoreData.time;
        string minutes = Mathf.Floor(gameTime / 60).ToString("00");
        string seconds = Mathf.RoundToInt(gameTime % 60).ToString("00");
        timeStatText.text = ($"Time: {minutes}:{seconds}");

        float waterPercent = scoreData.water / 100f;
        waterStatText.SetText($"Water: {Mathf.Round(waterPercent * 100)}%");

        damageStatText.text = ($"Damage taken: {Mathf.Round(GameManager.instance.CurrentStructDmg)}%");
     
        scoreData = ScoreManger.instance.AcquireScoreData();
        scoreStatText.SetText($"SCORE: {scoreData.score}");
    }
    private void Update()
    {      
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            SceneGerante.instance.GoLoadNextLevel();
        }
    }
}
