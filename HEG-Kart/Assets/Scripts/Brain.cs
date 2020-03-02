using UnityEngine;
using System.Collections.Generic;
using KartGame.KartSystems;
// This class contains the neural network 
public class Brain : MonoBehaviour
{

    //private readonly string ServerIpAdress = "http://192.168.1.1:5000/api/predict";
    private readonly string ServerIpAdress = "http://127.0.0.1:5000/api/predict";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // ça sert juste à montrer que l'on agit sur la même variable dans le même script
    }

    async void GetDirection(KeyboardInput c)
    {
        float[] distances = GameObject.Find("sensors_object").GetComponent<GetDistancesIA>().CalculateDistances();
        TestingData data = new TestingData();
        data.inputs = new List<float>(distances);
        Prediction prediction = await new Service().SendPut(data);

        c.m_Acceleration = prediction.acceleration;
        c.m_Steering = prediction.turn;
    }
}
