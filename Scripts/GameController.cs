using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject initialSpawnPos;
    public GameObject respawnPoint;
    public GameObject cameraHolder;
    public GameObject cameraPosition;
    public GameObject deadBody;
    private GameObject tempBody;
    private GameObject tempBody1;
    private GameObject temp;
    private GameObject hips;
    private GameObject spine;
    private GameObject head;
    private GameObject tempObj;
    public GameObject spawnedPlayer;
    public GameObject blackOutSquare;
    public GameObject reticleCanvas;
    public GameObject pickUpCanvas;

    public bool isRespawn = false;
    private bool bodyMoved = false;
    public bool fadeOut = true;
    public bool hitGround;

    public AudioClip Music;
    public AudioSource MusicSource;

    public Color objectColor;

    public float j;
    public float fadeSpeed = 2;
    private float fadeAmount;

    public int bodyCount;
    public int deathBySpikesCount;
    public int deathByShreddersCount;
    public int deathByElectricityCount;
    public int bodyLimit = 9999;
    public int bodiesUsed;


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
        bodyCount = 0;
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
        if (Input.GetKeyDown(KeyCode.R) && !isRespawn && bodiesUsed < bodyLimit) //isRespawning makes sure the player cant respawn until the camera has finished moving
        {
            StartCoroutine(respawnPlayer());
            //respawnPlayer();
        }

        //use this as a base if we want to implement the colour changing for the player
        //if (Input.GetKeyDown(KeyCode.M) && !isRespawn) //isRespawning makes sure the player cant respawn until the camera has finished moving
        //{
        //    tempBody1 = tempBody1.transform.Find("riggedd body 04").gameObject;
        //    tempBody1 = tempBody1.transform.Find("Layer_1").gameObject;
        //    tempBody1.GetComponent<Renderer>().material.color = Color.green;
        //}


        if (cameraHolder.transform.parent == null && tempBody != null)
        {
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
            cameraHolder.transform.position = temp.transform.position;
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
                spawnedPlayer.GetComponent<PlayerController>().enabled = true;
                tempBody = null;
                bodyMoved = true;
                bodyCount++;
                bodiesUsed++;
            }
        }
        else
        {
            hitGround = false;
        }

        //debug respawn
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    deadBody.transform.position = respawnPoint.transform.position;
        //    Instantiate(deadBody);
        //}

        //debug inputs for testing the fade to black canvas
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
        cameraHolder.transform.parent = null; //Removes the player as the cameras parent so they can be moved independantly
        spawnedPlayer.GetComponent<PlayerController>().heldObj = null;
        spawnedPlayer.GetComponent<PlayerController>().enabled = false;
        spawnedPlayer.transform.position = respawnPoint.transform.position;
        spawnedPlayer.transform.rotation = respawnPoint.transform.rotation;
        yield return new WaitForSeconds(0.1f); // change this to the lowest pissible value without breaking it or change it to ignore collisions?????
        tempBody = Instantiate(deadBody);
        tempBody1 = tempBody;
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
