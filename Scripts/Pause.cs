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
    //public GameObject sliderObj;
    public Slider volumeSlider;
    public Slider mouseSlider;
    public Slider jumpSlider;
    public Slider playerSlider;
    public Slider gravitySlider;
    public Slider respawnSlider;
    public GameControllerAnimated gameController;
    public Text volume;
    public Text mouseSensitivity;
    public Text jumpForce;
    public Text playerSpeed;
    public Text gravity;
    public Text respawnSpeed;
    void Start()
    {
        //mouseSlider = sliderObj.GetComponent<Slider>();
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
        gameController.spawnedPlayer.GetComponent<PlayerControllerAnimated>().mouseSensitivity = mouseSlider.value;
        gameController.spawnedPlayer.GetComponent<PlayerControllerAnimated>().jumpForce = jumpSlider.value;
        gameController.spawnedPlayer.GetComponent<PlayerControllerAnimated>().speed = playerSlider.value;
        gameController.spawnedPlayer.GetComponent<PlayerControllerAnimated>().gravity = gravitySlider.value;
        gameController.fadeSpeed = respawnSlider.value;
        //gameController.MusicSource.volume = volumeSlider.value;
        volume.text = volumeSlider.value.ToString();
        mouseSensitivity.text = mouseSlider.value.ToString();
        jumpForce.text = jumpSlider.value.ToString();
        playerSpeed.text = playerSlider.value.ToString();
        gravity.text = gravitySlider.value.ToString();
        respawnSpeed.text = respawnSlider.value.ToString();
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
