using KartGame.KartSystems;
using KartGame.Track;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GetDistancesIA : MonoBehaviour
{

    Vector3 startingPos;
    Quaternion startingRot;

    void Awake()
    {
        startingPos = this.transform.position;
        startingRot = this.transform.rotation;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public float[] CalculateDistances()
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
            transform.TransformDirection(Vector3.left * 2 + Vector3.forward),
            transform.TransformDirection(Vector3.left + Vector3.forward * 2),
            transform.TransformDirection(Vector3.left + Vector3.forward * 4),

            transform.TransformDirection(Vector3.forward),

            transform.TransformDirection(Vector3.right + Vector3.forward),
            transform.TransformDirection(Vector3.right * 2 + Vector3.forward),
            transform.TransformDirection(Vector3.right + Vector3.forward * 2),
            transform.TransformDirection(Vector3.right + Vector3.forward * 4),
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
                Debug.DrawRay(transform.position, feeler[i] * 5, Color.red);

                // Reset Kart position if it's a fence and if fences mode is enable
                if (PlayerPrefs.GetString("fences") == "True")
                {
                    bool isCheckpoint = hit.collider.gameObject.GetComponent<Checkpoint>() != null;
                    if (hit.distance < .8f && !isCheckpoint)
                    {
                        TrackManager trackManager = GameObject.FindObjectOfType<TrackManager>();
                        KartMovement kart = GameObject.FindObjectOfType<KartMovement>();
                        kart.transform.position = startingPos;
                        kart.transform.rotation = startingRot;
                        kart.ForceMove(Vector3.zero, Quaternion.identity);
                        trackManager.RestartRace();
                    }
                }
            }

            // Draw the feelers in the Scene mode
        }
        return inp;
    }

}
