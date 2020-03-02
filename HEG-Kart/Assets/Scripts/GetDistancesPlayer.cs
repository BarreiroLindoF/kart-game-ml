using KartGame.KartSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GetDistancesPlayer : MonoBehaviour
{

    [SerializeField]
    private TrainingData trainingData;

    int frameCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        // trainingUnits = new List<TrainUnit>();
        trainingData = new TrainingData();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        frameCount++;
        if (frameCount > 150)
        {
            if (frameCount % 5 == 0)
            {
                // Set up a raycast hit for knowing what we hit
                RaycastHit hit;

                /*
                // Set up out 5 feelers for undertanding the world
                Vector3[] feeler = new Vector3[]
                {
                // 0 = L
                transform.TransformDirection(Vector3.left),
                // 1 - FL
                transform.TransformDirection(Vector3.left+Vector3.forward),
                // 2 - F
                transform.TransformDirection(Vector3.forward),
                // 3 = FR
                transform.TransformDirection(Vector3.right + Vector3.forward),
                // 4 = R
                transform.TransformDirection(Vector3.right),
                };
                */

                // Set up out 5 feelers for undertanding the world
                Vector3[] feeler = new Vector3[]
                {
            transform.TransformDirection(Vector3.left),
            transform.TransformDirection(Vector3.left + Vector3.forward),
            /*transform.TransformDirection(Vector3.left + Vector3.forward*2),
            transform.TransformDirection(Vector3.left + Vector3.forward*3),
            transform.TransformDirection(Vector3.left + Vector3.forward*4),
            transform.TransformDirection(Vector3.left + Vector3.forward*5),
            transform.TransformDirection(Vector3.left + Vector3.forward*6),
            transform.TransformDirection(Vector3.left + Vector3.forward*7),
            transform.TransformDirection(Vector3.left + Vector3.forward*8),
            transform.TransformDirection(Vector3.left + Vector3.forward*9),
            transform.TransformDirection(Vector3.left + Vector3.forward*10),
            */
            transform.TransformDirection(Vector3.forward),

            transform.TransformDirection(Vector3.right + Vector3.forward),
            /*transform.TransformDirection(Vector3.right + Vector3.forward*2),
            transform.TransformDirection(Vector3.right + Vector3.forward*3),
            transform.TransformDirection(Vector3.right + Vector3.forward*4),
            transform.TransformDirection(Vector3.right + Vector3.forward*5),
            transform.TransformDirection(Vector3.right + Vector3.forward*6),
            transform.TransformDirection(Vector3.right + Vector3.forward*7),
            transform.TransformDirection(Vector3.right + Vector3.forward*8),
            transform.TransformDirection(Vector3.right + Vector3.forward*9),
            transform.TransformDirection(Vector3.right + Vector3.forward*10),*/
            transform.TransformDirection(Vector3.right),

                };

                // Use this to collect all feeler distances, then well pass them through our NN for an output
                float[] inp = new float[feeler.Length];

                // Loop through all feelers
                for (int i = 0; i < feeler.Length; i++)
                {
                    // See what all feelers feel
                    if (Physics.Raycast(transform.position, feeler[i], out hit))
                    {
                        inp[i] = hit.distance;
                        Debug.DrawRay(transform.position, feeler[i] * 5, Color.cyan);

                    }

                    // Draw the feelers in the Scene mode

                }
                trainingData.inputs.Add(new List<float>(inp));

                // Add user acceleration and turn in function of the input used
                switch (PlayerPrefs.GetString("input"))
                {
                    case "keyboard":
                        trainingData.turnOutputs.Add(CrossPlatformInputManager.GetAxis("Horizontal"));
                        trainingData.accelerationOutputs.Add(CrossPlatformInputManager.GetAxis("Vertical"));
                        break;
                    case "steeringWheel":
                        trainingData.turnOutputs.Add(GameObject.Find("Kart").GetComponent<SteeringInput>().Steering);
                        trainingData.accelerationOutputs.Add(GameObject.Find("Kart").GetComponent<SteeringInput>().Acceleration);
                        break;
                    case "gamepad":
                        trainingData.turnOutputs.Add(GameObject.Find("Kart").GetComponent<GamepadInput>().Steering);
                        trainingData.accelerationOutputs.Add(GameObject.Find("Kart").GetComponent<GamepadInput>().Acceleration);
                        break;
                    default:
                        trainingData.turnOutputs.Add(CrossPlatformInputManager.GetAxis("Horizontal"));
                        trainingData.accelerationOutputs.Add(CrossPlatformInputManager.GetAxis("Vertical"));
                        PlayerPrefs.SetString("input", "keyboard");
                        break;
                }
            }
        }
       
    }

    public void PostTrainingUnits()
    {
        new Service().SendPost(trainingData);
    }

    public void ClearTrainingUnits()
    {
        trainingData.inputs.Clear();
        trainingData.turnOutputs.Clear();
        trainingData.accelerationOutputs.Clear();
    }
}
