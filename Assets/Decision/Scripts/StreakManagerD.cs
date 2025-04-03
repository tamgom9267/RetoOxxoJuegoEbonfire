using UnityEngine;
using System;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json;
using Unity.Android.Gradle.Manifest;

public class StreakManagerD : MonoBehaviour
{
    private const string API_URL = "https://10.227.1.80:7220/Streaks";
    public int currentStreak;
    private string userId;
    
    void Start()
    {
        //userId = PlayerPrefs.GetString();
        userId = "2";
        StartCoroutine(LoadStreak());
    }

    public void IncrementStreak()
    {
        currentStreak++;
        StartCoroutine(SaveStreak());
    }
    
    public void ResetStreak()
    {
        currentStreak = 0;
        StartCoroutine(SaveStreak());
    }

    public void getStreak()
    {
        StartCoroutine(LoadStreak());
    }
    
    private IEnumerator SaveStreak()
    {
    UnityWebRequest web = UnityWebRequest.Put($"{API_URL}/{userId}", JsonConvert.SerializeObject(new StreakData { userId = userId, streak = currentStreak }));
    web.certificateHandler = new ForceAcceptAll();
    web.SetRequestHeader("Content-Type", "application/json");
    
    yield return web.SendWebRequest();

    if(web.result != UnityWebRequest.Result.Success)
    {
        Debug.LogError("Error API: " + web.error);
    }
    }
    
    public IEnumerator LoadStreak()
    {
        string JSONurl = $"{API_URL}/{userId}";
        UnityWebRequest web = UnityWebRequest.Get(JSONurl);
        web.certificateHandler = new ForceAcceptAll();
        yield return web.SendWebRequest();

        if(web.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error API: " + web.error);
            currentStreak = 0;
        }
        else
        {
            StreakData streakData = JsonConvert.DeserializeObject<StreakData>(web.downloadHandler.text);
            currentStreak = streakData.streak;
            Debug.Log(currentStreak);
        }
    }

    public int GetCurrentStreak()
    {
        return currentStreak;
    }

}


[Serializable]
public class StreakData
{
    public string userId;
    public int streak;
}
