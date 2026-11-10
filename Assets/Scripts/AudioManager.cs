using TMPro;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip currentMusic;
    [SerializeField] private TextMeshProUGUI musicTitle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentMusic = MusicManager.Instance.musicList[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (currentMusic == null) return;
        DisplayName(currentMusic);
    }

    void DisplayName(AudioClip _music)
    {
        musicTitle.text = _music.name;
    }
}