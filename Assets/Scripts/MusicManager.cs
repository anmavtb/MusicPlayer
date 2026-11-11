using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Networking;
using System;

public class MusicManager : MonoBehaviour
{
    private string playlistFolder;
    public List<AudioClip> musicLibrary;
    public event Action OnListInitialized;

    private void Awake()
    {
        playlistFolder = Path.Combine(Application.dataPath, "PLAYLIST");

        if (!Directory.Exists(playlistFolder))
        {
            Debug.LogWarning("PLAYLIST folder not found, creating it.");
            Directory.CreateDirectory(playlistFolder);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(GetMusicPaths(playlistFolder));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator GetMusicPaths(string _playlistFolder)
    {
        string[] files = Directory.GetFiles(_playlistFolder,"*.mp3");

        foreach (string file in files)
        {
            string uri = new System.Uri(file).AbsoluteUri;
            yield return StartCoroutine(LoadMusic(uri));
        }
        OnListInitialized?.Invoke();
    }

    private IEnumerator LoadMusic(string _uri)
    {
        using (UnityWebRequest request =
        UnityWebRequestMultimedia.GetAudioClip(_uri, AudioType.MPEG))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error : " + request.error);
                yield break;
            }
            musicLibrary.Add(DownloadHandlerAudioClip.GetContent(request));
        }
    }
}