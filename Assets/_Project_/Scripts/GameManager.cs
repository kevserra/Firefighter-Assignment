using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Effects;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private List<ExtinguishableParticleSystem> fires = new List<ExtinguishableParticleSystem>();

    [SerializeField] private int startingFires;
    [SerializeField] private float randomSpawnTime;

    [SerializeField] private float waterUsedPerSecond;
    [SerializeField] private float damagePerSec;

    [SerializeField] private GameObject victorLevelScreen;
    [SerializeField] private GameObject defeatLevelScreen;
    [SerializeField] private GameObject victoryEndGameScreen;
    
    private float nextSpawnTime;
    private int currentLevel;

    
    //public int currentScore { get; set; }

    public int CurrentFiresNow { get; set; }
    public float CurrentWaterNow { get; set; }  
    public float PresentTime { get; set; }
    public float CurrentStructDmg { get; set; }
    public bool NowGameOver { get; set; } = false; //can set a getter/setter to false*

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        DeactivateFires();     
        SpawnInitialFires(startingFires);
        nextSpawnTime = randomSpawnTime;
        CurrentWaterNow = 100;
    }

    void Update()
    {
        if (CurrentFiresNow >= 14 && !NowGameOver) NowGameOver = true; //1 line can just put line right under w/o { }

        if (nextSpawnTime > 0) nextSpawnTime -= Time.deltaTime;
        else
        {
            if (NowGameOver) return;

            SpawnInitialFires();
            nextSpawnTime = randomSpawnTime;
        }

        if (!NowGameOver) PresentTime += Time.deltaTime;
        
        BuildingDmgCalc();
    }

    public void OnLevelWin()
    {
        victorLevelScreen.SetActive(true);
        NowGameOver = false;
       
    }

    public void OnLevelLose()
    {
        defeatLevelScreen.SetActive(true);
        NowGameOver = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void EndGameSuccess()
    {
        victorLevelScreen.SetActive(false);
        victoryEndGameScreen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        NowGameOver = true;
    }

    private void SpawnInitialFires(int litFires = 0)
    {
        StartCoroutine(SpawnInitialFiresRoutine(litFires));
    }

    private IEnumerator SpawnInitialFiresRoutine(int fireCount)
    {
        fireCount = Mathf.Clamp(fireCount, 1, 14);
        int firesRemainingToCreate = fireCount;
        int randomIndexPos = -1;

        while (firesRemainingToCreate > 0)
        {
            randomIndexPos = GetIndexPositions();
            while (fires[randomIndexPos].gameObject.activeSelf)
            {
                randomIndexPos = GetIndexPositions();
            }

            fires[randomIndexPos].gameObject.SetActive(true);
            CurrentFiresNow += 1;
            firesRemainingToCreate -= 1;

            yield return null;
        }
    }

    public void UseWater()
    {
        if (NowGameOver) return;

        CurrentWaterNow -= Time.deltaTime * waterUsedPerSecond;

        if(CurrentWaterNow <= 0) OnLevelLose(); //lose game      
    }

    public void BuildingDmgCalc()
    {
        if (NowGameOver) return;
        CurrentStructDmg += (CurrentFiresNow > 0 ? damagePerSec : 0 * CurrentFiresNow) * Time.deltaTime;
        if (CurrentStructDmg >= 100) OnLevelLose();           
    }

    private int GetIndexPositions()
    {
        int randomIndex = Random.Range(0, fires.Count);
        return randomIndex;     
    }

    private void DeactivateFires()
    {
        foreach (ExtinguishableParticleSystem fire in fires) fire.gameObject.SetActive(false);      
    }

    public void FireGetsExtinguished()
    {
        CurrentFiresNow -= 1;        
        if (CurrentFiresNow == 0) OnLevelWin();       
    }

    public int GetUnlitWindows()
    {
        int amount = 0;
        foreach (ExtinguishableParticleSystem fire in fires) // for each type of fire in fires meaning the list variable name fires in the list above*
        {
            if (!fire.WasExtinguished) amount++;
        }
        return amount;
    }   
}
