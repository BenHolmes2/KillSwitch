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
    public AudioSource deathSource;
    private AudioClip[] deathSounds = new AudioClip[4];
    private int deathGruntInt;


    public Color objectColor;

    public float j;
    public float fadeSpeed = 2;
    private float fadeAmount;

    public int bodyCount;
    public int deathBySpikesCount;
    public int deathByShreddersCount;
    public int deathByElectricityCount;
    public int deathByBuzzSawCount;
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
        MusicSource.volume = 0.0001f;
        //MusicSource.Play();
        hitGround = false;
        objectColor = blackOutSquare.GetComponent<Image>().color;
        bodyCount = 0;

        //deathSounds[0] = Resources.Load("DeathGrunt1") as AudioClip;
        //deathSounds[1] = Resources.Load("DeathGrunt2") as AudioClip;
        //deathSounds[2] = Resources.Load("DeathGrunt3") as AudioClip;
        //deathSounds[3] = Resources.Load("DeathWilhelm") as AudioClip;
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
            //add timer in to stop soft lock
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
            //tempObj = tempBody.transform.Find("riggedd body 04").gameObject;
            //tempObj = tempObj.transform.Find("QuickRigCharacter_Reference").gameObject;
            //tempObj = tempObj.transform.Find("QuickRigCharacter_Hips").gameObject;
            //hips = tempObj;
            //spine = hips.transform.Find("QuickRigCharacter_Spine").gameObject;
            //spine = spine.transform.Find("QuickRigCharacter_Spine1").gameObject;
            //spine = spine.transform.Find("QuickRigCharacter_Spine2").gameObject;
            //head = spine.transform.Find("QuickRigCharacter_Neck").gameObject;
            //head = head.transform.Find("QuickRigCharacter_Head").gameObject;
            //temp = head.transform.Find("CameraPos").gameObject;
            //cameraHolder.transform.position = temp.transform.position;
            //cameraHolder.transform.parent = temp.transform;

            hips = tempBody.transform.Find("Hip_Root_JNT").gameObject;
            spine = hips.transform.Find("Spine_01_Bottom_JNT").gameObject;
            spine = spine.transform.Find("Spine_02_Mid_JNT").gameObject;
            spine = spine.transform.Find("Spine_03_Top_JNT").gameObject;
            spine = spine.transform.Find("Spine_00_Top_JNT").gameObject;

            head = spine.transform.Find("Neck_JNT").gameObject;
            head = head.transform.Find("Head_JNT").gameObject;


            temp = head.transform.Find("CameraPos").gameObject;

            cameraHolder.transform.position = temp.transform.position;
            cameraHolder.transform.parent = temp.transform.parent;
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
                //tempObj = tempObj.transform.Find("CameraHolder").gameObject;
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

            for (int i = 0; i < 10; i++)
            {
                Instantiate(deadBody);
            }
        }
    }

    public IEnumerator respawnPlayer()
    {
        isRespawn = true;
        deadBody.transform.position = spawnedPlayer.transform.position;
        deadBody.transform.rotation = spawnedPlayer.transform.rotation;
        tempObj = spawnedPlayer.transform.Find("CameraHolder").gameObject;
        //tempObj = tempObj.transform.Find("CameraHolder").gameObject; 
        cameraHolder.transform.parent = null; //Removes the player as the cameras parent so they can be moved independantly
        spawnedPlayer.GetComponent<PlayerController>().DropObject();
        spawnedPlayer.GetComponent<PlayerController>().enabled = false;
        spawnedPlayer.transform.position = respawnPoint.transform.position;
        spawnedPlayer.transform.rotation = respawnPoint.transform.rotation;
        yield return new WaitForSeconds(0.1f); // change this to the lowest pissible value without breaking it or change it to ignore collisions?????
        tempBody = Instantiate(deadBody);
        tempBody1 = tempBody;
        spawnedPlayer.GetComponent<PlayerController>().PlayDeathSound();
        //deathGruntInt = Random.Range(0, 3); //this randomly pick what death grunt to play
        //if (deathGruntInt == 4) //i dont want the wilhelm scream to play as often as the others and this keeps it rare
        //{
        //    deathGruntInt = Random.Range(0, 3);
        //}
        //spawnedPlayer.GetComponent<AudioSource>().PlayOneShot(deathSounds[deathGruntInt]);
        //deathSource.PlayOneShot(deathSounds[deathGruntInt]);
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
