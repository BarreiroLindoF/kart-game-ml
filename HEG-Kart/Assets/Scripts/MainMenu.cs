using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [Tooltip("A collection of UI panels that could be set active or inactive")]
    public GameObject[] panels;

    [Tooltip("An acceleration slider to be able to reference it later when it is inactive")]
    public Slider accelerationSlider;

    [Tooltip("A speed slider to be able to reference it later when it is inactive")]
    public Slider speedSlider;

    [Tooltip("A fence toggle to enable on fence hit restart mode")]
    public Toggle fenceToggle;

    [Tooltip("An input field to set the server address")]
    public InputField serverAddressInput;


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("keepPlayerPref") != "true") {
            PlayerPrefs.DeleteAll();
        }
        
        float speed = PlayerPrefs.GetFloat("kartSpeed");
        if (speed > 0) {
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
        }
        else
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setFences(bool active)
    {
        PlayerPrefs.SetString("fences", active.ToString());
    }

    public void setActive(int index)
    {
        for (var i = 0; i < panels.Length; i++)
        {
            bool active = i == index;
            GameObject panel = panels[i];
            if (panel.activeSelf != active)
            {
                panel.SetActive(active);
            }
        }
        if (index == 1)
        {
            panels[0].SetActive(true);
        }
    }

    public void loadMode(string modeName)
    {
        if (modeName == "IA")
        {
            PlayerPrefs.SetString("IA", "true");
        }
        else
        {
            PlayerPrefs.SetString("IA", "false");
        }
        SceneManager.LoadScene("Circuit_1");
    }

    public void selectControl(string controlName)
    {
        switch (controlName)
        {
            case "keyboard":
                PlayerPrefs.SetString("input", "keyboard");
                break;
            case "steeringWheel":
                print("wheel selected");
                PlayerPrefs.SetString("input", "steeringWheel");
                break;
            case "gamepad":
                PlayerPrefs.SetString("input", "gamepad");
                break;
        }
    }

    public void setKartSpeed(float speed) {
        PlayerPrefs.SetFloat("kartSpeed", speed);
    }

    public void setKartAcceleration(float acceleration)
    {
        PlayerPrefs.SetFloat("kartAcceleration", acceleration);
    }

    void OnGUI()
    {
        if (Input.GetKey(KeyCode.Escape)) {
            for (var i = 0; i < panels.Length; i++)
            {
                panels[i].SetActive(false);
            }
            panels[0].SetActive(true);
        }
    }

    public void setServerAddress(string address)
    {
        PlayerPrefs.SetString("serverIP", address);
        print("address changed to :");
        print(address);
    }

    public void leaveGame()
    {
        PlayerPrefs.SetString("keepPlayerPref", "false");
        Application.Quit();
    }
}
