using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    

    public void Play()
    {
        SceneManager.LoadScene("PLaytest");
    }

    public void Controls()
    {
        SceneManager.LoadScene("Control");
    }

    public void Settings() 
    {
        SceneManager.LoadScene("Setting");
    }
}
