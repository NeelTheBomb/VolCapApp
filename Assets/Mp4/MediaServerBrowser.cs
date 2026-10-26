using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;
using TMPro;

[Serializable]
public class MediaServerResponse
{
    public string sessionName;
    public MediaFile[] files;
}

[Serializable]
public class MediaFile
{
    public string relativePath;
    public string url;
}

public class MediaServerBrowser : MonoBehaviour
{
    [Header("Server")]
    public string serverBaseUrl; // change to your PC IP

    [Header("UI")]
    public TMP_Dropdown dropdown;

    [Header("Video Player")]
    public VideoPlayer videoPlayer;

    private MediaFile[] files;

    void Start()
    {
        RefreshList();
    }

    public void RefreshList()
    {
        StartCoroutine(FetchLatestSession());
    }

    IEnumerator FetchLatestSession()
    {
        string endpoint = serverBaseUrl + "/api/latest_session";

        using UnityWebRequest req = UnityWebRequest.Get(endpoint);
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Media server request failed: " + req.error);
            yield break;
        }

        MediaServerResponse resp = JsonUtility.FromJson<MediaServerResponse>(req.downloadHandler.text);
        if (resp == null || resp.files == null)
        {
            Debug.LogError("Failed to parse JSON:\n" + req.downloadHandler.text);
            yield break;
        }

        files = resp.files;

        dropdown.ClearOptions();
        List<string> options = new List<string>();
        foreach (var f in files) options.Add(f.relativePath);
        dropdown.AddOptions(options);

        dropdown.onValueChanged.RemoveAllListeners();
        dropdown.onValueChanged.AddListener(OnPickFile);

        if (files.Length > 0)
            OnPickFile(0);
    }

    void OnPickFile(int index)
    {
        if (files == null || index < 0 || index >= files.Length) return;

        string url = files[index].url;

        videoPlayer.Stop();
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = url;
        videoPlayer.Play();

        Debug.Log("Playing: " + url);
    }
}
