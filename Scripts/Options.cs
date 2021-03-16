using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{ 
    public GameObject button;
    public TMP_Text text ; 
    public static bool isOn = false;
    private int counter;
    private int mode_counter;
    public TMP_Text mode_text;


    private void Start()
    {
        mode_counter = 0;
        counter = 0; //even refers to limitless time mode, odds time limited
        text.text = PlayerPrefs.GetString("ButtonText");
        mode_text.text = PlayerPrefs.GetString("ModeText");
        if(text.text == "Limitless Time")
        {
            counter = 1;
        }
        else if(text.text == "Limited Time")
        {
            counter = 0;
        }
        else
        {
            text.text = "Click to choose mode";
        }

        if(mode_text.text == "Normal Mode")
        {
            mode_counter = 1;
        }
        else if(mode_text.text == "Hard Mode")
        {
            mode_counter = 0;
        }
        else
        {
            mode_text.text = "Choose difficulty";
        }
    }
    /*public void clickUndo()
    {
        text.text = "Limited Time";
        isOn = false;
        button.SetActive(false);
        if (!isOn)
        {
            PlayerPrefs.SetInt("TimeLimiter", 0);
            PlayerPrefs.SetString("ButtonText", "Limited Time");
        }
        
    }*/


    public void ChooseMode()
    {
        if(mode_counter % 2 == 0)
        {
            PlayerPrefs.SetString("ModeText", "Normal Mode");
            mode_text.text = "Normal Mode";
            PlayerPrefs.SetInt("Difficulty", 0);
        }
        if(mode_counter % 2 == 1)
        {
            PlayerPrefs.SetString("ModeText", "Hard Mode");
            mode_text.text = "Hard Mode";
            PlayerPrefs.SetInt("Difficulty", 1);
        }
        mode_counter++;
    }

    public void ClickLimitlessTime()
    {

        if(counter % 2 == 0)
        {
            PlayerPrefs.SetString("ButtonText", "Limitless Time");
            PlayerPrefs.SetInt("TimeLimiter", 1);
            text.text = "Limitless Time";
        }
        if(counter % 2 == 1)
        {
            PlayerPrefs.SetString("ButtonText", "Limited Time");
            PlayerPrefs.SetInt("TimeLimiter", 0);
            text.text = "Limited Time";
        }
        counter++;
    }

    public static bool getBoolean()
    {
        return isOn;
    }

    public void backToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
