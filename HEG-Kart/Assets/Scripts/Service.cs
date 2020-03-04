using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Networking;
using RestSharp;
using System.Threading.Tasks;

class Service
{
    /*Developpement*/
    //private readonly string trainRoute = "http://127.0.0.1:5000/api/train";
    //private readonly string predictRoute = "http://127.0.0.1:5000/api/predict";

    /*Production*/
    private readonly string trainRoute = "/api/train";
    private readonly string predictRoute = "/api/predict";


    public void SendPost(TrainingData trainingData)
    {
        if (trainingData.inputs.Count == 0 || trainingData.turnOutputs.Count == 0 || trainingData.accelerationOutputs.Count == 0) return;

        string json = JsonConvert.SerializeObject(trainingData);

        // JSON does not work with POST, so I need to use PUT
        UnityWebRequest www = UnityWebRequest.Put(PlayerPrefs.GetString("serverIP") + trainRoute, json);
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

    public async Task<Prediction> SendPut(TestingData testingData)
    {
        if (testingData.inputs.Count == 0) return new Prediction();

        string json = JsonConvert.SerializeObject(testingData);

        RestClient client = new RestClient();
        RestRequest request = new RestRequest(PlayerPrefs.GetString("serverIP") + predictRoute, DataFormat.Json);
        request.AddJsonBody(json);
        client.Put(request);
        IRestResponse response = await client.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<Prediction>(response.Content);

    }

}
