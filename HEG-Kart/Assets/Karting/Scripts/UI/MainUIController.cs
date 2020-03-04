using KartGame.KartSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace KartGame.UI
{
    /// <summary>
    /// A MonoBehaviour for controlling the different panels of the main menu.
    /// </summary>
    public class MainUIController : MonoBehaviour
    {
        [Tooltip("A collection of UI panels, one of which will be active at a time.")]
        public GameObject[] panels;

        [Tooltip("An acceleration slider to be able to reference it later when it is inactive")]
        public Slider accelerationSlider;

        [Tooltip("A speed slider to be able to reference it later when it is inactive")]
        public Slider speedSlider;

        [Tooltip("A fence toggle to enable on fence hit restart mode")]
        public Toggle fenceToggle;

        [Tooltip("An input field to set the server address")]
        public InputField serverAddressInput;

        void Start()
        {
            float speed = PlayerPrefs.GetFloat("kartSpeed");
            if (speed > 0)
            {
                speedSlider.value = speed;
            }
            float acceleration = PlayerPrefs.GetFloat("kartAcceleration");
            if (acceleration > 0)
            {
                accelerationSlider.value = acceleration;
            }
            string fenceMode = PlayerPrefs.GetString("fences");
            if (fenceMode == "True")
            {
                fenceToggle.isOn = true;
            } else
            {
                fenceToggle.isOn = false;
            }
            string serverAddress = PlayerPrefs.GetString("serverIP");
            if (serverAddress != "")
            {
                print("address was already setted and was : ");
                print(serverAddress);
                serverAddressInput.text = serverAddress;
            }


        }

        /// <summary>
        /// Turns off all the panels except the one at the given index which is turned on.
        /// </summary>
        public void SetActivePanel(int index)
        {
            for (var i = 0; i < panels.Length; i++)
            {
                bool active = i == index;
                GameObject panel = panels[i];
                if (panel.activeSelf != active)
                    panel.SetActive(active);
            }
        }

        public  void LoadLevel(string levelName)
        {
            if (levelName == "MainMenu")
            {
                PlayerPrefs.SetString("keepPlayerPref", "true");
            }
            SceneManager.LoadScene(levelName);
        }

        public void setServerAddress(string address)
        {
            PlayerPrefs.SetString("serverIP", address);
            print("address changed to :");
            print(address);
        }

        public void ChangeControl(string control_name)
        {
            switch (control_name) {
                case "keyboard":
                    PlayerPrefs.SetString("input", "keyboard");
                    GameObject.Find("Kart").GetComponent<KartMovement>().input = GameObject.Find("Kart").GetComponent<KeyboardInput>();
                    GameObject.Find("Kart").GetComponent<KartAnimation>().input = GameObject.Find("Kart").GetComponent<KeyboardInput>();
                    break;
                case "steeringWheel":
                    PlayerPrefs.SetString("input", "steeringWheel");
                    GameObject.Find("Kart").GetComponent<KartMovement>().input = GameObject.Find("Kart").GetComponent<SteeringInput>();
                    GameObject.Find("Kart").GetComponent<KartAnimation>().input = GameObject.Find("Kart").GetComponent<SteeringInput>();
                    break;
                case "gamepad":
                    PlayerPrefs.SetString("input", "gamepad");
                    GameObject.Find("Kart").GetComponent<KartMovement>().input = GameObject.Find("Kart").GetComponent<GamepadInput>();
                    GameObject.Find("Kart").GetComponent<KartAnimation>().input = GameObject.Find("Kart").GetComponent<GamepadInput>();
                    break;
                default:
                    break;
            }
        }

        public void setSpeed(float speed)
        {
            PlayerPrefs.SetFloat("kartSpeed", speed);
        }

        public void setAcceleration(float acceleration)
        {
            PlayerPrefs.SetFloat("kartAcceleration", acceleration);
        }

        public void setFences(bool active)
        {
            PlayerPrefs.SetString("fences" , active.ToString());
        }

        void OnEnable()
        {
            SetActivePanel(0);
        }
    }
}