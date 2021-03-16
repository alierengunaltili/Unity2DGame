using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen1 : MonoBehaviour
{
    private bool flag = false;
    private float counter = 0;
    public void Setup()
    {
        Invoke("timeCounter", 1);
    }
    
    public void timeCounter()
    {
        gameObject.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("StartingMenu");
    }
}
