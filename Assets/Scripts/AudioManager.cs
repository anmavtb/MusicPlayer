using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(MusicManager))]

public class AudioManager : MonoBehaviour
{
    [SerializeField] MusicManager musicManager;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private List<AudioClip> playlist;

    [SerializeField] private AudioClip currentMusic;
    [SerializeField] private TextMeshProUGUI musicTitle;

    [SerializeField] GameObject buttonPlay;
    [SerializeField] GameObject buttonPause;

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
        if (currentMusic == null) return;
        UpdateDisplay(currentMusic);
    }

    void UpdateDisplay(AudioClip _music)
    {
        if (playlist.Count == 0 || audioSource.clip == null || _music == null) return;
        DisplayMusic(_music);
    }

    void DisplayMusic(AudioClip _music)
    {
        musicTitle.text = _music.name;
    }

    void PlayMusic(AudioClip _music)
    {
        audioSource.clip = _music;
        audioSource.Play();
    }

    public void PlayPauseMusic()
    {
        if (audioSource.isPlaying)
        {
            buttonPlay.SetActive(false);
            buttonPause.SetActive(true);
            audioSource.Pause();
        }
        else
        {
            buttonPlay.SetActive(true);
            buttonPause.SetActive(false);
            if (audioSource.time > 0)
            {
                audioSource.UnPause();
            }
            else PlayMusic(currentMusic);
        }
    }
}