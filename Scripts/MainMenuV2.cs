using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine;
using TMPro;

public class MainMenuV2 : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider mouseSlider;
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject CreditsMenu;
    //public AudioClip Music;
    public AudioSource MusicSource;
    public AudioMixer mixer;
    private int mouseInt;
    private int volumeInt;
    public TMP_Text sensitivityText;
    public TMP_Text audioText;
    public Animator creditsAnimator;
    public Button playButton;
    public Button settingsBackButton;
    public Button creditsBackButton;


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
    }

    private void Update()
    {
        PlayerPrefs.SetFloat("MouseSensitivity", mouseSlider.value);
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        mixer.SetFloat("MasterVolume", volumeSlider.value);
        volumeInt = (int)volumeSlider.value;
        audioText.text = volumeInt.ToString();
        mouseInt = (int)mouseSlider.value;
        sensitivityText.text = mouseInt.ToString();

        if (creditsAnimator.GetBool("isFinished") == true)
        {
            mainMenu.SetActive(true);
            settingsMenu.SetActive(false);
            CreditsMenu.SetActive(false);
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

}
