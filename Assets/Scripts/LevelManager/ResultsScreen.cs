using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultsScreen : MonoBehaviour
{
    public GameObject resultsScreenObj;
    
    public TMP_Text resultsText;

    public TMP_Text highscoreText;

    public GameObject newRecordObj;
    
    //TODO: LOAD HIGHSCORE
    public int previousHighScore = 999;

    public void DisplayResults(string results, int roundScore, bool newRecord)
    {
        resultsScreenObj.SetActive(true);

        resultsText.text = results;

        highscoreText.text = "ALL TIME RECORD:           " + previousHighScore + "\n\nTHIS ROUND:           " +
                             roundScore;
        
        if(newRecord)
            newRecordObj.SetActive(true);
    }

    public void CloseResults()
    {
        newRecordObj.SetActive(false);
        resultsScreenObj.SetActive(false);
    }
}
