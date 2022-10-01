using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MainMenuV2 : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject CreditsMenu;

    // Start is called before the first frame update

    private void Start()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        CreditsMenu.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene("Liam");
    }

    public void Credits()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        CreditsMenu.SetActive(true);
    }

    public void Settings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        CreditsMenu.SetActive(false);
    }

    public void Back()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        CreditsMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
