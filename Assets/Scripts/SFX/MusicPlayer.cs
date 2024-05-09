using UnityEngine;
using System;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip song;
    public event Action<AudioClip, float> AnnounceSongPlay;

    public float BPM;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        PlaySong();
    }

    [ContextMenu("Play Song")]
    private void PlaySong()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop(); 
        }

        audioSource.clip = song; 
        audioSource.Play(); 
        AnnounceSongPlay?.Invoke(song, BPM); 
    }
}