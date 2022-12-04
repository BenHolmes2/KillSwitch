using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;


public class MainMenuV2 : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider mouseSlider;
    public Slider FOVSlider;
    public Toggle invertToggle;
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject CreditsMenu;
    //public AudioClip Music;
    public AudioSource MusicSource;
    public AudioMixer mixer;
    private int mouseInt;
    private int volumeInt;
    private int FOVInt;
    private int inverted;
    public TMP_Text sensitivityText;
    public TMP_Text audioText;
    public TMP_Text FOVText;
    public Animator creditsAnimator;
    public Button playButton;
    public Button settingsBackButton;
    public Button creditsBackButton;
    public InputActionReference invertAction;
    public PlayerInput playerInput;
    public GameObject ControllerControlsImage;
    public GameObject MKControlsImage;


    // Start is called before the first frame update

    private void Start()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        CreditsMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        playButton.Select();



        if (PlayerPrefs.GetFloat("Volume") != 0)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
            mixer.SetFloat("MasterVolume", volumeSlider.value);
            volumeInt = (int)volumeSlider.value;
            audioText.text = volumeInt.ToString();
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
        //if (PlayerPrefs.GetInt("Inverted") == 1)
        //{
        //    invertToggle.isOn = true;  
        //}
        //else
        //{
        //    invertToggle.isOn = false;
        //}
    }

    private void Update()
    {
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


        PlayerPrefs.SetFloat("MouseSensitivity", mouseSlider.value);
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.SetFloat("FOV", FOVSlider.value);
        PlayerPrefs.SetInt("Inverted", inverted);

        mixer.SetFloat("MasterVolume", volumeSlider.value);
        volumeInt = (int)volumeSlider.value;
        audioText.text = volumeInt.ToString();
        mouseInt = (int)mouseSlider.value;
        sensitivityText.text = mouseInt.ToString();
        FOVInt = (int)FOVSlider.value;
        FOVText.text = FOVInt.ToString();

        if (creditsAnimator.GetBool("isFinished") == true)
        {
            mainMenu.SetActive(true);
            settingsMenu.SetActive(false);
            CreditsMenu.SetActive(false);
        }

        if (playerInput.currentControlScheme == "Controller")
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
    }
    public void Play()
    {
        SceneManager.LoadScene("Liam");
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void Credits()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        CreditsMenu.SetActive(true);
        creditsAnimator.SetBool("PlayCredits", true);
        creditsBackButton.Select();
    }

    public void Settings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        CreditsMenu.SetActive(false);
        settingsBackButton.Select();
    }

    private void OnBack()
    {
        Back();
    }

    public void Back()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        CreditsMenu.SetActive(false);
        playButton.Select();
        
    }

    public void Quit()
    {
        Application.Quit();
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
