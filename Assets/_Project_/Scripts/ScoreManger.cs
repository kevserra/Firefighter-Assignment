using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ScoreManger : MonoBehaviour
{
    public static ScoreManger instance;

    private const int TIMER_SCORE = 175;
    private const int FIRELESS_SCORE = 75;
    private const int PENALTY_SCORE = -10;
    private const int SCORE_WATER = 1000;
    public float bestHighScore { get; set; }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        instance = this;
    }
    
    public ScoreCache AcquireScoreData()
    {
        ScoreCache data = new ScoreCache(CalculateTotalScores(), GameManager.instance.PresentTime, GameManager.instance.CurrentWaterNow, GameManager.instance.CurrentStructDmg);
        return data;
    }

    public int CalculateTotalScores()
    {
        int totalUnlitWindows = GameManager.instance.GetUnlitWindows();
        int timeElapsed = (int) GameManager.instance.PresentTime;      
        int damage = (int)GameManager.instance.CurrentStructDmg;
        damage *= PENALTY_SCORE;

        int water = (int)GameManager.instance.CurrentWaterNow;
        water *= SCORE_WATER;

        int score = ((14 - totalUnlitWindows) * FIRELESS_SCORE) + ((15 / timeElapsed) * TIMER_SCORE) + damage + water;
        return score;
    }

    //public void SaveHighScore()
    //{
    //    if (CalculateTotalScores() > bestHighScore)
    //    {
    //        PlayerPrefs.SetInt("HIGHSCORE:", (int)bestHighScore);      Couldn't get this to work. Tried my best on it.
    //        PlayerPrefs.Save();
    //    }
    //}

    //private int GetRandomScoreValue()
    //{
    //    int score = Random.Range(MIN_SCORE, MAX_SCORE);
    //    return score;
    //}

}

public struct ScoreCache
{
    public int score;
    public float time;
    public float water;
    public float damage;

    public ScoreCache(int _score, float _time, float _water, float _damage)
    {
        score = _score;
        time = _time;
        water = _water;
        damage = _damage;
    }
}
