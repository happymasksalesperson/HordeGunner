using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerView : MonoBehaviour
{
    public LevelTimer timer;

    public TMP_Text timerText;

    public void OnEnable()
    {
        timer = GetComponentInParent<LevelTimer>();
        timer.AnnounceTime += DisplayTime;
    }

    private void DisplayTime(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        int milliseconds = (int)((time * 100) % 100);

        string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}",
            minutes,
            seconds,
            milliseconds);
        timerText.text = formattedTime;
    }

    public void OnDisable()
    {
        timer.AnnounceTime -= DisplayTime;
    }
}