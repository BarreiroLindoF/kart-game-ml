using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [Tooltip("A collection of UI panels that could be set active or inactive")]
    public GameObject[] panels;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void leaveGame()
    {
        Application.Quit();
    }
}
