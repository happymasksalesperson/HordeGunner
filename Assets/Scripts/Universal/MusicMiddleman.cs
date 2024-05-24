using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMiddleman : MonoBehaviour
{
    public MusicPlayer SFX;

    public SpawnerController sc;

    public RotateTransform rotater;

    public float BPM;

    public float beatTime;

    private float originalBeatTime;

    // Calculate rotate speed based on waitTime
    private float minWaitTime;
    private float maxWaitTime;
    private float minRotateSpeed;
    private float maxRotateSpeed;
    private bool playing = false;

    public void OnEnable()
    {
        SFX.AnnounceSongPlay += SetBPM;
    }

    private void SetBPM(AudioClip clip, float newBPM)
    {
        BPM = newBPM;
        beatTime = (60 / BPM);
        originalBeatTime = beatTime;
        sc.waitTime = beatTime * 8;
        StartCoroutine(IncreaseTimer());

        minWaitTime = originalBeatTime / 4; // Minimum wait time
        maxWaitTime = originalBeatTime * 8; // Maximum wait time
        minRotateSpeed = 1f; // Minimum rotate speed
        maxRotateSpeed = 10f; // Maximum rotate speed
        playing = true;
        sc.StartSpawnCoroutine();
    }

    public void Update()
    {
        if (playing)
        {
            // Calculate the normalized value between min and max wait time
            float normalizedWaitTime = Mathf.Clamp01((sc.waitTime - minWaitTime) / (maxWaitTime - minWaitTime));

            // Use the normalized wait time to interpolate the rotate speed between min and max rotate speed
            rotater.rotateSpeed = Mathf.Lerp(minRotateSpeed, maxRotateSpeed, normalizedWaitTime);
        }
    }

    IEnumerator IncreaseTimer()
    {
        yield return new WaitForSeconds(originalBeatTime * 8);

        sc.waitTime = originalBeatTime * 8;

        yield return new WaitForSeconds(originalBeatTime * 8);

        sc.waitTime = originalBeatTime * 4;

        yield return new WaitForSeconds(originalBeatTime * 8);

        sc.waitTime = originalBeatTime * 2;

        yield return new WaitForSeconds(originalBeatTime * 8);

        sc.waitTime = originalBeatTime;

        yield return new WaitForSeconds(originalBeatTime * 8);

        sc.waitTime = originalBeatTime / 2;

        yield return new WaitForSeconds(originalBeatTime * 8);

        sc.waitTime = originalBeatTime / 4;
    }

    void OnDisable()
    {
        SFX.AnnounceSongPlay -= SetBPM;
    }
}