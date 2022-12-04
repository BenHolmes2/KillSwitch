using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.InputSystem;



public class PauseV2 : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject debugMenu;

    public TMP_Text sensitivityText;
    public TMP_Text audioText;
    public TMP_Text FOVText;

    //public GameObject sliderObj;
    public Slider volumeSlider;
    public Slider mouseSlider;
    public Slider FOVSlider;
    public Toggle invertToggle;
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
    public bool paused = false;
    private int mouseInt;
    private int volumeInt;
    private int FOVInt;
    private int inverted;
    public bool startPause = false;

    public AudioMixer mixer;

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
    public GameObject respawnSquisher;
    public GameObject respawnFanTube;
    public Button resumeButton;
    public Button settingsBackButton;
    public Button debugBackButton;
    public InputActionReference invertAction;
    public PlayerInput playerInput;
    public GameObject ControllerControlsImage;
    public GameObject MKControlsImage;



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
            volumeInt = (int)volumeSlider.value;
            audioText.text = volumeInt.ToString();
            mixer.SetFloat("MasterVolume", volumeSlider.value);
        }
        if (PlayerPrefs.GetFloat("MouseSensitivity") != 0)
        {
            mouseSlider.value = PlayerPrefs.GetFloat("MouseSensitivity");
            mouseInt = (int)mouseSlider.value;
            sensitivityText.text = mouseInt.ToString();
        }
        if (PlayerPrefs.GetFloat("FOV") != 0)
        {
            FOVSlider.value = PlayerPrefs.GetFloat("FOV");
            FOVInt = (int)FOVSlider.value;
            FOVText.text = FOVInt.ToString();
        }
        if (PlayerPrefs.GetInt("Inverted") == 1)
        {
            invertToggle.isOn = true;
            invertAction.action.ApplyBindingOverride(new InputBinding { overrideProcessors = "invertVector2(invertX=false,invertY=true)" });

        }
        else
        {
            invertToggle.isOn = false;
            invertAction.action.ApplyBindingOverride(new InputBinding { overrideProcessors = "invertVector2(invertX=false,invertY=false)" });
        }
    }

    void Update()
    {


        if (playerInput == null)
        {
            playerInput = gameController.spawnedPlayer.GetComponent<PlayerInput>();
            gameController.spawnedPlayer.GetComponent<PlayerControllerAnimated>().mouseSensitivity = mouseSlider.value;
        }


        if (startPause && !paused)
        {
            PauseGame();
        }
        else if (!startPause && paused)
        {
            ResumeGame();
            Cursor.visible = false;

        }

        if (playerInput.currentControlScheme == "Controller" && paused)
        {
            Cursor.visible = false;
        }
        else if (paused)
        {
            Cursor.visible = true;
        }

        if (playerInput.currentControlScheme == "Controller")
        {
            ControllerControlsImage.SetActive(true);
            MKControlsImage.SetActive(false);
        }
        else
        {
            ControllerControlsImage.SetActive(false);
            MKControlsImage.SetActive(true);
        }

        //if (Input.GetKeyDown(KeyCode.M) && paused)
        //{
        //    DebugMenu();
        //}

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

        mouseInt = (int)mouseSlider.value;
        sensitivityText.text = mouseInt.ToString();
        volumeInt = (int)volumeSlider.value;
        audioText.text = volumeInt.ToString();
        FOVInt = (int)FOVSlider.value;
        FOVText.text = FOVInt.ToString();
    }

    //private void OnPause()
    //{
    //    if (!paused)
    //    {
    //        PauseGame();
    //    }
    //    else
    //    {
    //        ResumeGame();
    //    }
    //}

    //private void OnDebug()
    //{
    //    if (paused)
    //    {
    //        DebugMenu();
    //    }
    //}

    public void ResumeGame()
    {
        startPause = false;
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        debugMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        paused = false;
        //Cursor.visible = false;
        gameController.spawnedPlayer.GetComponent<PlayerControllerAnimated>().enabled = true;
        gameController.spawnedPlayer.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
        Debug.Log(gameController.spawnedPlayer.GetComponent<PlayerInput>().currentActionMap);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        paused = true;
        //Cursor.visible = true;
        gameController.spawnedPlayer.GetComponent<PlayerControllerAnimated>().enabled = false;
        gameController.spawnedPlayer.GetComponent<PlayerInput>().SwitchCurrentActionMap("Menu");
        Debug.Log(gameController.spawnedPlayer.GetComponent<PlayerInput>().currentActionMap);
        resumeButton.Select();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        ResumeGame();
        SceneManager.LoadScene("MainMenuV2"); // add the reference to the main scene her when it is built
    }

    public void SettingsMenu()
    {
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
        debugMenu.SetActive(false);
        settingsBackButton.Select();

    }

    public void DebugMenu()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        debugMenu.SetActive(true);
        debugBackButton.Select();
    }

    public void Back()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        debugMenu.SetActive(false);
        gameController.spawnedPlayer.GetComponent<PlayerControllerAnimated>().mouseSensitivity = mouseSlider.value;
        mixer.SetFloat("MasterVolume", volumeSlider.value);
        gameController.spawnedPlayer.GetComponent<PlayerControllerAnimated>().cameraObj.GetComponent<Camera>().fieldOfView = FOVSlider.value;
        PlayerPrefs.SetFloat("MouseSensitivity", mouseSlider.value);
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.SetInt("Inverted", inverted);
        resumeButton.Select();
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

    public void SetSquisher()
    {
        gameController.respawnPoint.transform.position = respawnSquisher.transform.position;
        gameController.respawnPoint.transform.rotation = respawnSquisher.transform.rotation;
    }

    public void SetFanTube()
    {
        gameController.respawnPoint.transform.position = respawnFanTube.transform.position;
        gameController.respawnPoint.transform.rotation = respawnFanTube.transform.rotation;
    }

    public void ResetRespawn()
    {
        gameController.hitGround = true;
    }

    public void SetInverted()
    {
        if (!invertToggle.isOn)
        {
            invertAction.action.ApplyBindingOverride(new InputBinding { overrideProcessors = "invertVector2(invertX=false,invertY=false)" });
            inverted = 0;
        }
        else
        {
            invertAction.action.ApplyBindingOverride(new InputBinding { overrideProcessors = "invertVector2(invertX=false,invertY=true)" });
            inverted = 1;
        }
    }
}
