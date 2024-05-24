using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public ComboTracker comboTracker;
    public int highScore;

    public MusicPlayer SFX;

    public int objectiveMaxHP;
    public List<HealthComponent> objectiveHP = new List<HealthComponent>();

    public LevelTimer timer;

    public ResultsScreen resultsScreen;

    public SpawnerController sc;

    public GameObject uiObj;

    public bool playerWon = true;

    void Start()
    {
        comboTracker.AnnounceCombo += TrackHighScore;
        timer.AnnounceTime += TimeOver;
        StartGame();
    }

    public void StartGame()
    {
        playerWon = true;
        resultsScreen.CloseResults();
        uiObj.SetActive(true);
        SetObjectiveHP();
        timer.StartTimer();
        SFX.StartGame();
    }

    public void SetObjectiveHP()
    {
        foreach (HealthComponent HP in objectiveHP)
        {
            HP.maxHP = objectiveMaxHP;
            HP.Resurrect();
            HP.AnnounceDeath += ObjectiveDestroyedGameOver;
        }
    }

    private void ObjectiveDestroyedGameOver()
    {
        playerWon = false;
        timer.StopTimer();
        DisplayResults(false);
    }

    private void TrackHighScore(int input)
    {
        if (input > highScore)
        {
            highScore = input;
        }
    }

    private void TimeOver(float input)
    {
        if (input <= 0 && playerWon)
        {
            DisplayResults(true);
        }
    }

    public void DisplayResults(bool input)
    {
        SFX.GameOver();
        sc.StopSpawn();

        uiObj.SetActive(false);

        string results;
        if (input)
            results = "NICE! YOU MADE IT!";
        else
            results = "YOU LOSE! GAME OVER!";

        bool newRecord = true;
        //checkloaddata

        resultsScreen.DisplayResults(results, highScore, newRecord);
    }

    void OnEnable()
    {
        comboTracker.AnnounceCombo -= TrackHighScore;
        timer.AnnounceTime -= TimeOver;
    }
}