using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class Service
{
    //private readonly string ServerIpAdress = "http://192.168.1.1:5000/api/train";
    private readonly string ServerIpAdress = "http://127.0.0.1:5000/api/train";

    public void SendPost(TrainingData trainingData)
    {
        if (trainingData.inputs.Count == 0 || trainingData.turnOutputs.Count == 0 || trainingData.accelerationOutputs.Count == 0) return;

        string json = JsonConvert.SerializeObject(trainingData);

        // JSON does not work with POST, so I need to use PUT
        UnityWebRequest www = UnityWebRequest.Put(ServerIpAdress, json);
        www.SetRequestHeader("Content-Type", "application/json");
        www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogAssertion(www.error);
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Data successfully sent to server !");
        }
    }

}
