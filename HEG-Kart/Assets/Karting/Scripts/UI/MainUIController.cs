using KartGame.KartSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KartGame.UI
{
    /// <summary>
    /// A MonoBehaviour for controlling the different panels of the main menu.
    /// </summary>
    public class MainUIController : MonoBehaviour
    {
        [Tooltip("A collection of UI panels, one of which will be active at a time.")]
        public GameObject[] panels;

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
            SceneManager.LoadScene(levelName);
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

        void OnEnable()
        {
            SetActivePanel(0);
        }
    }
}