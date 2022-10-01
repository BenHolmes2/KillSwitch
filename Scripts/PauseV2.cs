using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseV2 : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    //public GameObject sliderObj;
    public Slider volumeSlider;
    public Slider mouseSlider;
    //public Slider jumpSlider;
    //public Slider playerSlider;
    //public Slider gravitySlider;
    //public Slider respawnSlider;
    public GameControllerAnimated gameController;
    //public Text volume;
    //public Text mouseSensitivity;
    //public Text jumpForce;
    //public Text playerSpeed;
    //public Text gravity;
    //public Text respawnSpeed;
    private bool paused = false;
    void Start()
    {
        //mouseSlider = sliderObj.GetComponent<Slider>();
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !paused) 
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.P) && paused)
        {
            ResumeGame();
        }
        gameController.spawnedPlayer.GetComponent<PlayerControllerAnimated>().mouseSensitivity = mouseSlider.value;
        //gameController.spawnedPlayer.GetComponent<PlayerControllerAnimated>().jumpForce = jumpSlider.value;
        //gameController.spawnedPlayer.GetComponent<PlayerControllerAnimated>().speed = playerSlider.value;
        //gameController.spawnedPlayer.GetComponent<PlayerControllerAnimated>().gravity = gravitySlider.value;
        //gameController.fadeSpeed = respawnSlider.value;
        gameController.MusicSource.volume = volumeSlider.value;
        //volume.text = volumeSlider.value.ToString();
        //mouseSensitivity.text = mouseSlider.value.ToString();
        //jumpForce.text = jumpSlider.value.ToString();
        //playerSpeed.text = playerSlider.value.ToString();
        //gravity.text = gravitySlider.value.ToString();
        //respawnSpeed.text = respawnSlider.value.ToString();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        paused = false;
        //Cursor.visible = false;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        paused = true;
        //Cursor.visible = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenuV2"); // add the reference to the main scene her when it is built
    }

    public void LoadSettingsMenu()
    {
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);

    }

    public void Back()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
