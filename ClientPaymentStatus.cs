using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;


public class ClientPaymentStatus : MonoBehaviour
{
    public string url;
    public GameObject Blocker;

    void Start()
    {
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonString = webRequest.downloadHandler.text;
                Debug.Log(jsonString);

                if (jsonString == "0")
                {
                    //Client has not paid
                    Debug.Log("Client has not paid");
                    Blocker.SetActive(true);
                }
                else if (jsonString == "1")
                {
                    //Client has paid
                    Debug.Log("Client has paid");
                    Blocker.SetActive(false);
                }
            }
            else
            {
                Debug.LogError("Error: " + webRequest.error);
            }
        }
    }

}