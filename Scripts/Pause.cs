using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject controlsMenu;
    public GameObject settingsMenu;
    public GameObject sliderObj;
    private Slider slider;
    private GameObject cameraObj;
    private GameObject cameraHolder;
    public GameController1 gameController;
    public Text mouseSensitivity;
    void Start()
    {
        slider = sliderObj.GetComponent<Slider>();
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        controlsMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) 
        {
            PauseGame();
        }
        //if (Input.GetKeyDown(KeyCode.M)) 
        //{
        //    ResumeGame();
        //}
        gameController.cameraHolder.GetComponentInChildren<MouseLook>().mouseSensitivity = slider.value;
        mouseSensitivity.text = slider.value.ToString();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        //Cursor.visible = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0); // add the reference to the main scene her when it is built
    }

    public void LoadSettingsMenu()
    {
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);

    }

    public void UnloadSettingsMenu()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void LoadControlsMenu()
    {
        controlsMenu.SetActive(true);
        pauseMenu.SetActive(false);

    }

    public void UnloadControlsMenu()
    {
        controlsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
