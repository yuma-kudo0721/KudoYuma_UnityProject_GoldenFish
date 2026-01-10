using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[System.Serializable]
public class ScoreData
{
    public string name;
    public int score;
}

public class FirebaseSender : MonoBehaviour
{
    string databaseURL = "https://goldenfish-a66e3-default-rtdb.firebaseio.com/scores.json";

    public void UploadScore(string playerName, int score)
    {
        ScoreData data = new ScoreData { name = playerName, score = score };
        string json = JsonUtility.ToJson(data);
        StartCoroutine(PostScore(json));
    }

    IEnumerator PostScore(string json)
    {
        UnityWebRequest www = new UnityWebRequest(databaseURL, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("送信失敗: " + www.error);
        }
        else
        {
            Debug.Log("スコア送信成功！");
        }
    }
}
