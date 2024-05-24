using UnityEngine;
using System;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip song;
    public event Action<AudioClip, float> AnnounceSongPlay;

    public float BPM;

    private AudioSource audioSource;

    void OnEnable()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void StartGame()
    {
        PlaySong();
    }

    [ContextMenu("Play Song")]
    public void PlaySong()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop(); 
        }

        audioSource.clip = song; 
        audioSource.Play(); 
        AnnounceSongPlay?.Invoke(song, BPM); 
    }

    public void GameOver()
    {
        audioSource.Stop();
    }
}