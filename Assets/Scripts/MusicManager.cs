using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Networking;

public class MusicManager : Singleton<MusicManager>
{
    [SerializeField] public List<AudioClip> musicList;
    private string playlistFolder;

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
        GetMusicPaths(playlistFolder);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetMusicPaths(string _playlistFolder)
    {
        string[] files = Directory.GetFiles(_playlistFolder,"*.mp3");

        foreach (string file in files)
        {
            string uri = new System.Uri(file).AbsoluteUri;
            StartCoroutine(LoadMusic(uri));
        }
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
            musicList.Add(DownloadHandlerAudioClip.GetContent(request));
        }
    }
}