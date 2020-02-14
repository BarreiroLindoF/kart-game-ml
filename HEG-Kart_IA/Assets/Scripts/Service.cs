using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Networking;
using RestSharp;
using System.Threading.Tasks;

class Service
{
    //private readonly string ServerIpAdress = "http://192.168.1.1:5000/api/train";
    private readonly string ServerIpAdress = "http://127.0.0.1:5000/api/predict";

    public async Task<Prediction> SendPut(TestingData testingData)
    {
        if (testingData.inputs.Count == 0) return new Prediction();

        string json = JsonConvert.SerializeObject(testingData);

        RestClient client = new RestClient();
        RestRequest request = new RestRequest(ServerIpAdress, DataFormat.Json);
        request.AddJsonBody(json);
        client.Put(request);
        IRestResponse response = await client.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<Prediction>(response.Content);
        
    }

}
