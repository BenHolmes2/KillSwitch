using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseV2 : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject debugMenu;

    public GameObject respawnInitial;
    public GameObject respawnButton;
    public GameObject respawnWall;
    public GameObject respawnSpikes;
    public GameObject respawnElecIntro;
    public GameObject respawnBuzzSawIntro;
    public GameObject respawnFanIntro;
    public GameObject respawnSpaceJam;
    public GameObject respawnFanSpikeSaw;
    public GameObject respawnBridge;
    public GameObject respawnCrissCross;
    public GameObject respawnCatapult;

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

    public AudioMixer mixer;

    void Start()
    {
        //mouseSlider = sliderObj.GetComponent<Slider>();
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        debugMenu.SetActive(false);
        //Debug.Log(PlayerPrefs.GetFloat("Volume"));
        //Debug.Log(PlayerPrefs.GetFloat("MouseSensitivity"));
        if (PlayerPrefs.GetFloat("Volume") != 0)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
            mixer.SetFloat("MasterVolume", volumeSlider.value);
        }
        if (PlayerPrefs.GetFloat("MouseSensitivity") != 0)
        {
            mouseSlider.value = PlayerPrefs.GetFloat("MouseSensitivity");
        }
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

        if (Input.GetKeyDown(KeyCode.M) && paused)
        {
            DebugMenu();
        }
        gameController.spawnedPlayer.GetComponent<PlayerControllerAnimated>().mouseSensitivity = mouseSlider.value;
        //gameController.spawnedPlayer.GetComponent<PlayerControllerAnimated>().jumpForce = jumpSlider.value;
        //gameController.spawnedPlayer.GetComponent<PlayerControllerAnimated>().speed = playerSlider.value;
        //gameController.spawnedPlayer.GetComponent<PlayerControllerAnimated>().gravity = gravitySlider.value;
        //gameController.fadeSpeed = respawnSlider.value;
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
        debugMenu.SetActive(false);
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

    public void SettingsMenu()
    {
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
        debugMenu.SetActive(false);

    }

    public void DebugMenu()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        debugMenu.SetActive(true);
    }

    public void Back()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        debugMenu.SetActive(false);
        PlayerPrefs.SetFloat("MouseSensitivity", mouseSlider.value);
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }

    public void SetInitial()
    {
        gameController.respawnPoint.transform.position = respawnInitial.transform.position;
        gameController.respawnPoint.transform.rotation = respawnInitial.transform.rotation;
    }

    public void SetButton()
    {
        gameController.respawnPoint.transform.position = respawnButton.transform.position;
        gameController.respawnPoint.transform.rotation = respawnButton.transform.rotation;
    }

    public void SetWall()
    {
        gameController.respawnPoint.transform.position = respawnWall.transform.position;
        gameController.respawnPoint.transform.rotation = respawnWall.transform.rotation;
    }

    public void SetSpike()
    {
        gameController.respawnPoint.transform.position = respawnSpikes.transform.position;
        gameController.respawnPoint.transform.rotation = respawnSpikes.transform.rotation;
    }

    public void SetElecIntro()
    {
        gameController.respawnPoint.transform.position = respawnElecIntro.transform.position;
        gameController.respawnPoint.transform.rotation = respawnElecIntro.transform.rotation;
    }

    public void SetBuzzSawIntro()
    {
        gameController.respawnPoint.transform.position = respawnBuzzSawIntro.transform.position;
        gameController.respawnPoint.transform.rotation = respawnBuzzSawIntro.transform.rotation;
    }

    public void SetFanIntro()
    {
        gameController.respawnPoint.transform.position = respawnFanIntro.transform.position;
        gameController.respawnPoint.transform.rotation = respawnFanIntro.transform.rotation;
    }

    public void SetSpaceJam()
    {
        gameController.respawnPoint.transform.position = respawnSpaceJam.transform.position;
        gameController.respawnPoint.transform.rotation = respawnSpaceJam.transform.rotation;
    }

    public void SetFanSpikeSaw()
    {
        gameController.respawnPoint.transform.position = respawnFanSpikeSaw.transform.position;
        gameController.respawnPoint.transform.rotation = respawnFanSpikeSaw.transform.rotation;
    }

    public void SetBridge()
    {
        gameController.respawnPoint.transform.position = respawnBridge.transform.position;
        gameController.respawnPoint.transform.rotation = respawnBridge.transform.rotation;
    }

    public void SetCrissCross()
    {
        gameController.respawnPoint.transform.position = respawnCrissCross.transform.position;
        gameController.respawnPoint.transform.rotation = respawnCrissCross.transform.rotation;
    }

    public void SetCatapult()
    {
        gameController.respawnPoint.transform.position = respawnCatapult.transform.position;
        gameController.respawnPoint.transform.rotation = respawnCatapult.transform.rotation;
    }

    public void ResetRespawn()
    {
        gameController.hitGround = true;
    }
}
