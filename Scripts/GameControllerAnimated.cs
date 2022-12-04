using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;




public class GameControllerAnimated : MonoBehaviour
{
    public GameObject player;
    public GameObject initialSpawnPos;
    public GameObject respawnPoint;
    public GameObject cameraHolder;
    public GameObject cameraPosition;
    public GameObject deadBody;
    private GameObject tempBody;
    private GameObject currentBody;
    private GameObject temp;
    private GameObject hips;
    private GameObject spine;
    private GameObject head;
    private GameObject leftUpLeg;
    private GameObject leftLeg;
    private GameObject rightUpLeg;
    private GameObject rightLeg;
    private GameObject spineTemp;
    private GameObject leftArm;
    private GameObject leftForeArm;
    private GameObject leftHand;
    private GameObject rightArm;
    private GameObject rightForeArm;
    private GameObject rightHand;
    private GameObject tempObj;
    public GameObject spawnedPlayer;
    public PlayerControllerAnimated spawnedPlayerScript;
    public GameObject blackOutSquare;
    private Image blackOutSquareImage;
    public GameObject reticleCanvas;
    public TMP_Text pickUpText;
    public GameObject pickUpCanvas;

    public bool startRespawn = false;
    public bool isRespawn = false;
    private bool bodyMoved = false;
    public bool fadeOut = true;
    public bool hitGround;
    public bool respawnAllowed;
    public float ragdollTurnOffDelay;

    public Color objectColor;

    public float j;
    public float fadeSpeed = 2;
    private float fadeAmount;

    public int bodyCount;
    public int deathBySpikesCount;
    public int deathByShreddersCount;
    public int deathByElectricityCount;
    public int deathByBuzzSawCount;
    public int deathByGearBoxCount;
    public int deathByFallingCount;
    public int bodyLimit = 9999;
    public int bodiesUsed;
    public float volume;



    void Start()
    {
        Time.timeScale = 1.0f;
        Instantiate(player, initialSpawnPos.transform.position, initialSpawnPos.transform.rotation);
        CheckExistence();


        hitGround = false;
        //objectColor = blackOutSquare.GetComponent<Image>().color;
        objectColor = new Color(0, 0, 0, 0);
        bodyCount = 0;
        blackOutSquareImage = blackOutSquare.GetComponent<Image>();


        StartCoroutine(FadeBlackOutSqaure(false, 0.2f));
    }

    void CheckExistence()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            spawnedPlayer = playerObject;
            cameraHolder = GameObject.FindWithTag("CameraHolder");
            cameraPosition = GameObject.FindWithTag("CameraPosition");
            spawnedPlayerScript = spawnedPlayer.GetComponent<PlayerControllerAnimated>();
        }
        else
        {
            Debug.Log("Can't find Player...");
        }
    }

    void Update()
    {
        if (startRespawn && !isRespawn && bodiesUsed < bodyLimit && respawnAllowed) //isRespawning makes sure the player cant respawn until the camera has finished moving
        {
            //add timer in to stop soft lock
            //StartCoroutine(respawnPlayer());
            respawnPlayer();
            blackOutSquareImage.color = new Color(0, 0, 0, 0);
        }

        if (cameraHolder.transform.parent == null && tempBody != null)
        {
            temp = tempBody.transform.Find("parent").gameObject;
            hips = temp.transform.Find("mixamorig:Hips1").gameObject;
            spine = hips.transform.Find("mixamorig:Spine").gameObject;
            spine = spine.transform.Find("mixamorig:Spine1").gameObject;
            spineTemp = spine.transform.Find("mixamorig:Spine2").gameObject;
            head = spineTemp.transform.Find("mixamorig:Neck").gameObject;
            temp = head.transform.Find("mixamorig:Head").gameObject;

            cameraHolder.transform.position = temp.transform.position;
            cameraHolder.transform.parent = temp.transform.parent;
        }
        if (hitGround && isRespawn)
        {
            if (fadeOut)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquareImage.color = objectColor;
                j = blackOutSquareImage.color.a;
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
                    blackOutSquareImage.color = objectColor;
                    j = blackOutSquareImage.color.a;
                    //Debug.Log(j);
                    if (j < 0)
                    {
                        fadeOut = true;
                        bodyMoved = false;
                        hitGround = false;
                        isRespawn = false;
                        startRespawn = false;
                        SetLayer(currentBody);
                        Physics.IgnoreLayerCollision(6, 3, false);
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
                spawnedPlayerScript.enabled = true;
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

        //debug inputs for testing the fade to black canvas
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    StartCoroutine(FadeBlackOutSqaure(true, 0.2f));
        //}
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    StartCoroutine(FadeBlackOutSqaure(false, 0.2f));
        //}

        //debug respawn 
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    deadBody.transform.position = spawnedPlayer.transform.position;
        //    deadBody.transform.rotation = spawnedPlayer.transform.rotation;
        //    spawnedPlayer.transform.position = respawnPoint.transform.position;
        //    spawnedPlayer.transform.rotation = respawnPoint.transform.rotation;

        //    for (int i = 0; i < 10; i++)
        //    {
        //        Instantiate(deadBody);
        //    }
        //}
    }

    //private void OnRespawn()
    //{
    //    if (!isRespawn && bodiesUsed < bodyLimit && respawnAllowed) //isRespawning makes sure the player cant respawn until the camera has finished moving
    //    {
    //        //add timer in to stop soft lock
    //        respawnPlayer();
    //        blackOutSquareImage.color = new Color(0, 0, 0, 0);
    //    }
    //}

    public void respawnPlayer()
    {
        isRespawn = true;
        deadBody.transform.position = spawnedPlayer.transform.position;
        deadBody.transform.rotation = spawnedPlayer.transform.rotation;
        //Debug.Log(spawnedPlayer.transform.Find("CameraHolder").gameObject);
        tempObj = spawnedPlayer.transform.Find("CameraHolder").gameObject;
        //Debug.Log(tempObj);
        //tempObj = tempObj.transform.Find("CameraHolder").gameObject; 
        cameraHolder.transform.parent = null; //Removes the player as the cameras parent so they can be moved independantly
        spawnedPlayerScript.DropObject();
        spawnedPlayerScript.PlayDeathSound();
        spawnedPlayerScript.enabled = false;
        Physics.IgnoreLayerCollision(6, 3, true);
        spawnedPlayer.transform.position = respawnPoint.transform.position;
        spawnedPlayer.transform.rotation = respawnPoint.transform.rotation;
        tempBody = Instantiate(deadBody);
        currentBody = tempBody;
        temp = tempBody.transform.Find("parent").gameObject;
        hips = temp.transform.Find("mixamorig:Hips1").gameObject;
        spine = hips.transform.Find("mixamorig:Spine").gameObject;
        spine = spine.transform.Find("mixamorig:Spine1").gameObject;
        spineTemp = spine.transform.Find("mixamorig:Spine2").gameObject;
        head = spineTemp.transform.Find("mixamorig:Neck").gameObject;
        temp = head.transform.Find("mixamorig:Head").gameObject;
        temp = temp.transform.Find("CameraPos").gameObject;
        cameraHolder.transform.position = temp.transform.position;
        cameraHolder.transform.parent = temp.transform.parent;

    }

    private void SetLayer(GameObject currentBody)
    {
        hips = currentBody.transform.Find("parent").gameObject;
        hips = hips.transform.Find("mixamorig:Hips1").gameObject;
        leftUpLeg = hips.transform.Find("mixamorig:LeftUpLeg").gameObject;
        leftLeg = leftUpLeg.transform.Find("mixamorig:LeftLeg").gameObject;
        rightUpLeg = hips.transform.Find("mixamorig:RightUpLeg").gameObject;
        rightLeg = rightUpLeg.transform.Find("mixamorig:RightLeg").gameObject;
        spine = hips.transform.Find("mixamorig:Spine").gameObject;
        spine = spine.transform.Find("mixamorig:Spine1").gameObject;

        spineTemp = spine.transform.Find("mixamorig:Spine2").gameObject;

        leftArm = spineTemp.transform.Find("mixamorig:LeftShoulder").gameObject;
        leftArm = leftArm.transform.Find("mixamorig:LeftArm").gameObject;

        leftForeArm = leftArm.transform.Find("mixamorig:LeftForeArm").gameObject;
        leftHand = leftForeArm.transform.Find("mixamorig:LeftHand").gameObject;

        rightArm = spineTemp.transform.Find("mixamorig:RightShoulder").gameObject;
        rightArm = rightArm.transform.Find("mixamorig:RightArm").gameObject;

        rightForeArm = rightArm.transform.Find("mixamorig:RightForeArm").gameObject;
        rightHand = rightForeArm.transform.Find("mixamorig:RightHand").gameObject;
        head = spineTemp.transform.Find("mixamorig:Neck").gameObject;
        head = head.transform.Find("mixamorig:Head").gameObject;

        hips.layer = 0;
        leftUpLeg.layer = 0;
        leftLeg.layer = 0;
        rightUpLeg.layer = 0;
        rightLeg.layer = 0;
        spine.layer = 0;
        leftArm.layer = 0;
        leftForeArm.layer = 0;
        leftHand.layer = 0;
        rightArm.layer = 0;
        rightForeArm.layer = 0;
        hips.layer = 0;
        rightHand.layer = 0;
        head.layer = 0;
    }

    public IEnumerator FadeBlackOutSqaure(bool fadeToBlack = true, float fadeSpeed = 5f)
    {
        Color objectColorA = blackOutSquareImage.color;
        float fadeAmount;

        if (fadeToBlack)
        {
            while (blackOutSquareImage.color.a < 1)
            {
                fadeAmount = objectColorA.a + (fadeSpeed * Time.deltaTime);
                objectColorA = new Color(objectColorA.r, objectColorA.g, objectColorA.b, fadeAmount);
                blackOutSquareImage.color = objectColorA;
                yield return null;
            }
        }
        else
        {
            while (blackOutSquareImage.color.a > 0)
            {
                fadeAmount = objectColorA.a - (fadeSpeed * Time.deltaTime);
                objectColorA = new Color(objectColorA.r, objectColorA.g, objectColorA.b, fadeAmount);
                blackOutSquareImage.color = objectColorA;
                yield return null;
            }
        }
    }

}
