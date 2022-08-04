using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController1 : MonoBehaviour
{
    public GameObject player;
    public GameObject initialSpawnPos;
    public GameObject respawnPoint;
    public GameObject cameraHolder;
    public GameObject cameraPosition;
    public GameObject deadBody;
    private GameObject tempBody;
    private GameObject temp;
    private GameObject hips;
    private GameObject spine;
    private GameObject head;
    private GameObject tempObj;
    public GameObject spawnedPlayer;
    public GameObject blackOutSquare;

    public bool isRespawn = false;
    private bool bodyMoved = false;
    public bool fadeOut = true;
    public bool hitGround;

    public AudioClip Music;
    private AudioSource MusicSource;

    public Color objectColor;

    public float j;
    public float fadeSpeed = 2;
    private float fadeAmount;

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
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    SceneManager.LoadScene("Menu");
        //}

        if (Input.GetKeyDown(KeyCode.R) && !isRespawn) //isRespawning makes sure the player cant respawn until the camera has finished moving
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
        if (hitGround && isRespawn)
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
                        //because the ragdoll has multiple colliders this gets set to true multiple times and cant be set back to false, needs to be fixed!!!!!!!!!!!!!
                        hitGround = false;
                        isRespawn = false;
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
        else
        {
            hitGround = false;
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

        //debug respawn 
        if (Input.GetKeyDown(KeyCode.T))
        {
            deadBody.transform.position = spawnedPlayer.transform.position;
            deadBody.transform.rotation = spawnedPlayer.transform.rotation;
            spawnedPlayer.transform.position = respawnPoint.transform.position;
            spawnedPlayer.transform.rotation = respawnPoint.transform.rotation;
            Instantiate(deadBody);

        }

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
        isRespawn = true;
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
