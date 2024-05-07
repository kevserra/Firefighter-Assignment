using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StatsBar : MonoBehaviour
{
    [SerializeField] private Text timeStatText;
    [SerializeField] private Text waterStatText;
    [SerializeField] private Text damageStatText;
    [SerializeField] private Text fireStatText;

    private void Update()
    {
        UpdateStatusUI();
    }
    
    private void UpdateStatusUI()
    {
        float gameTime = GameManager.instance.PresentTime;
        string minutes = Mathf.Floor(gameTime / 60).ToString("00");
        string seconds = Mathf.RoundToInt(gameTime % 60).ToString("00");
        timeStatText.text = $"{minutes}:{seconds}";

        float waterPercent = GameManager.instance.CurrentWaterNow / 100f;
        waterStatText.text = $"{Mathf.Round(waterPercent * 100)}%";

        float firePercent = GameManager.instance.CurrentFiresNow / 14f;
        fireStatText.text = $"{Mathf.Round(firePercent * 100)}%";                                 //<<<<<<<<<----------------- "mathf." is the math for the unity library

        damageStatText.text = $"{Mathf.Round(GameManager.instance.CurrentStructDmg)}%";
    }
}
