using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//need to change the way the lerp time is calculated, maybe using the distance
//currently there are issues between lerp time when in scene and when the game is built

public class GameController1 : MonoBehaviour
{
    public GameObject player;
    public GameObject initialSpawnPos;
    public GameObject respawnPoint;
    public GameObject cameraHolder;
    public GameObject cameraPosition;
    public GameObject deadBody;
    //use ditance calulations
    //why has this lerp time changedf from 2f to 4f????????????????????????????
    public float followSpeed; // probably a good idea to have this change based on what room the player is in, the larger the room the larger the number needs to be in order to slow down the camera movement for larger distances
    private bool isRespawning = false;
    public float lerpTime = 2f;
    public GameObject spawnedPlayer;
    public AudioClip Music;
    private AudioSource MusicSource;
    private GameObject tempObj;
    private Vector3 lerpThreshold;
    private GameObject tempBody;
    private GameObject temp;
    private GameObject hips;
    private GameObject spine;
    private GameObject head;
    public bool hitGround;
    public float j;
    private bool bodyMoved = false;
    public GameObject blackOutSquare;
    public bool fadeOut = true;
    public Color objectColor;
    public float fadeSpeed = 0.1f;


    float fadeAmount;

    void Start()
    {
        Instantiate(player, initialSpawnPos.transform.position, initialSpawnPos.transform.rotation);
        CheckExistence();

        MusicSource = this.gameObject.AddComponent<AudioSource>();
        MusicSource.loop = true;
        MusicSource.playOnAwake = true;
        if (Music != null)
            MusicSource.clip = Music;
        MusicSource.volume = 0.1f;
        MusicSource.Play();
        lerpThreshold.Set(0, 0, 0);
        hitGround = false;
        objectColor = blackOutSquare.GetComponent<Image>().color;
    }

    void CheckExistence()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            spawnedPlayer = playerObject;
            cameraHolder = GameObject.FindWithTag("CameraHolder");
            cameraPosition = GameObject.FindWithTag("CameraPosition");
        }
        else
        {
            Debug.Log("Can't find Player...");
        }
    }

    void Update()
    {
        // this is the current temporary escape code
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }

        if (Input.GetKeyDown(KeyCode.R) && !isRespawning) //isRespawning makes sure the player cant respawn until the camera has finished moving
        {
            StartCoroutine(respawnPlayer());
        }
        if (cameraHolder.transform.parent == null && tempBody != null)
        {
            
            //temp = tempBody.transform.Find("CameraPos").gameObject;

            tempObj = tempBody.transform.Find("riggedd body 04").gameObject;
            tempObj = tempObj.transform.Find("QuickRigCharacter_Reference").gameObject;
            tempObj = tempObj.transform.Find("QuickRigCharacter_Hips").gameObject;

            hips = tempObj;
            spine = hips.transform.Find("QuickRigCharacter_Spine").gameObject;
            spine = spine.transform.Find("QuickRigCharacter_Spine1").gameObject;
            spine = spine.transform.Find("QuickRigCharacter_Spine2").gameObject;
            head = spine.transform.Find("QuickRigCharacter_Neck").gameObject;
            head = head.transform.Find("QuickRigCharacter_Head").gameObject;
            temp = head.transform.Find("CameraPos").gameObject;
            cameraHolder.transform.parent = temp.transform;
        }
        if (hitGround)
        {
            if (fadeOut)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                j = blackOutSquare.GetComponent<Image>().color.a;
                //Debug.Log(j);
                if (j > 1)
                {
                    fadeOut = false;
                }
            }
            if (bodyMoved)
            {
                if (!fadeOut)
                {
                    fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                    objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                    blackOutSquare.GetComponent<Image>().color = objectColor;
                    j = blackOutSquare.GetComponent<Image>().color.a;
                    //Debug.Log(j);
                    if (j < 0)
                    {
                        fadeOut = true;
                        bodyMoved = false;
                        hitGround = false;
                    }
                }
            }
            if (j >= 1) //this check probably needs to be changed as it is based on the alpha value, should probably be a boolean !!!
            {
                cameraHolder.transform.parent = spawnedPlayer.transform;
                cameraHolder.transform.position = cameraPosition.transform.position;
                cameraHolder.transform.rotation = cameraPosition.transform.rotation;
                tempObj = spawnedPlayer.transform.Find("CameraHolder").gameObject;
                tempObj.GetComponentInChildren<MouseLook>().enabled = true;//stops the player from moving while they are being respawned and the camera is moving
                spawnedPlayer.GetComponent<PlayerMoveSlide>().enabled = true;
                tempBody = null;
                bodyMoved = true;
            }

        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            deadBody.transform.position = respawnPoint.transform.position;
            Instantiate(deadBody);
        }

        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    StartCoroutine(FadeBlackOutSqaure(true, 5));
        //}
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    StartCoroutine(FadeBlackOutSqaure(false, 5));
        //}

        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    deadBody.transform.position = respawnPoint.transform.position;
        //    deadBody.transform.rotation = respawnPoint.transform.rotation;
        //    Instantiate(deadBody);
        //}

    }
    public IEnumerator respawnPlayer()
    {
        deadBody.transform.position = spawnedPlayer.transform.position;
        deadBody.transform.rotation = spawnedPlayer.transform.rotation;
        tempObj = spawnedPlayer.transform.Find("CameraHolder").gameObject;
        tempObj.GetComponentInChildren<MouseLook>().enabled = false;//stops the player from moving while they are being respawned and the camera is moving
        cameraHolder.transform.parent = null; //Removes the player as the cameras parent so they can be moved independantly
        spawnedPlayer.GetComponent<PlayerMoveSlide>().enabled = false;
        tempObj.GetComponent<PickUp>().heldObj = null;
        spawnedPlayer.transform.position = respawnPoint.transform.position;
        spawnedPlayer.transform.rotation = respawnPoint.transform.rotation;
        yield return new WaitForSeconds(0.1f);
        tempBody = Instantiate(deadBody);
    }

    //public IEnumerator FadeBlackOutSqaure(bool fadeToBlack = true, int fadeSpeed = 5)
    //{
    //    Color objectColor = blackOutSquare.GetComponent<Image>().color;
    //    float fadeAmount;

    //    if (fadeToBlack)
    //    {
    //        j = blackOutSquare.GetComponent<Image>().color.a;
    //        while (blackOutSquare.GetComponent<Image>().color.a < 1)
    //        {
    //            fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

    //            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
    //            blackOutSquare.GetComponent<Image>().color = objectColor;
    //            j = blackOutSquare.GetComponent<Image>().color.a;
    //            yield return null;
    //        }
    //    }
    //    else
    //    {
    //        while (blackOutSquare.GetComponent<Image>().color.a > 0)
    //        {
    //            fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

    //            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
    //            blackOutSquare.GetComponent<Image>().color = objectColor;
    //            j = blackOutSquare.GetComponent<Image>().color.a;
    //            yield return null;
    //        }
    //    }
    //}

}
