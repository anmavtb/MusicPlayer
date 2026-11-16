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

    [SerializeField] bool isMusicPaused = false;
    [SerializeField] bool isPlayListShuffle = false;

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
        UpdateMusic(playlist[0]);
        StartMusic(currentMusic);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateMusic(AudioClip _music)
    {
        currentMusic = _music;
        audioSource.clip = currentMusic;
        UpdateDisplay(currentMusic);
        StartMusic(currentMusic);
    }

    void UpdateDisplay(AudioClip _music)
    {
        if (playlist.Count == 0 || _music == null) return;
        DisplayMusic(_music);
    }

    void DisplayMusic(AudioClip _music)
    {
        musicTitle.text = _music.name;
    }

    void StartMusic(AudioClip _music)
    {
        audioSource.Play();
        if (isMusicPaused) audioSource.Pause();
    }

    public void PlayPauseMusic()
    {
        if (!isMusicPaused)
        {
            isMusicPaused = true;
            buttonPlay.SetActive(false);
            buttonPause.SetActive(true);
            audioSource.Pause();
        }
        else
        {
            isMusicPaused = false;
            buttonPlay.SetActive(true);
            buttonPause.SetActive(false);
            audioSource.UnPause();
        }
    }

    public void MusicNext()
    {
        UpdateIndex(1);
    }

    public void MusicBack()
    {
        if (audioSource.time >= (audioSource.clip.length * .1))
        {
            audioSource.time = 0;
            return;
        }
        UpdateIndex(-1);
    }

    void UpdateIndex(int _direction)
    {
        int index = playlist.IndexOf(currentMusic);
        if (index == -1) return;
        index = (index + _direction + playlist.Count) % playlist.Count;
        UpdateMusic(playlist[index]);
        if (buttonPlay.activeSelf) StartMusic(currentMusic);
    }

    public void ShufflePlaylist()
    {
        if (!isPlayListShuffle)
        {
            isPlayListShuffle = true;
            for (int i = playlist.Count - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                AudioClip temp = playlist[i];
                playlist[i] = playlist[randomIndex];
                playlist[randomIndex] = temp;
            }
        }
        else
        {
            isPlayListShuffle = false;
            for (int i = 0; i < playlist.Count; i++)
            {
                playlist[i] = musicManager.musicLibrary[i];
            }
        }
    }
}