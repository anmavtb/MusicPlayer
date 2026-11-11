using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MusicManager))]

public class AudioManager : MonoBehaviour
{
    [SerializeField] MusicManager musicManager;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private List<AudioClip> playlist;

    [SerializeField] private AudioClip currentMusic;
    [SerializeField] private TextMeshProUGUI musicTitle;

    private void Awake()
    {
        musicManager = GetComponent<MusicManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicManager.OnListInitialized += InitializePlayer;
    }

    void InitializePlayer()
    {
        foreach (AudioClip music in musicManager.musicLibrary)
        {
            playlist.Add(music);
        }
        currentMusic = playlist[0];
        UpdateDisplay(currentMusic);
        PlayMusic(currentMusic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateDisplay(AudioClip _music)
    {
        if (playlist.Count == 0 || _music == null) return;
        DisplayName(_music);
    }

    void DisplayName(AudioClip _music)
    {
        musicTitle.text = _music.name;
    }

    void PlayMusic(AudioClip _music)
    {
        audioSource.clip = _music;
        audioSource.Play();
    }
}