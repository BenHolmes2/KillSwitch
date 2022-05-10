using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Still some bugs that need to be worked out with when a player tries to move while the camera is moving
    public GameObject player;
    public GameObject initialSpawnPos;
    public GameObject cameraHolder;
    public GameObject cameraPosition;
    public GameObject respawnPoint;
    public GameObject deadBody;
    //use ditance calulations
    //why has this lerp time changedf from 2f to 4f????????????????????????????
    public float followSpeed = 100f; // probably a good idea to have this change based on what room the player is in, the larger the room the larger the number needs to be in order to slow down the camera movement for larger distances
    private bool isLerping = true;
    private bool isRespawning = false;
    private float currentLerpTime;
    public float lerpTime = 2f;
    private GameObject spawnedPlayer;
    public AudioClip Music;
    private AudioSource MusicSource;
    private GameObject[] ragdollObjects;

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
        //ragdollObjects = GameObject.FindGameObjectsWithTag("Ragdoll");

        if (Input.GetKeyDown(KeyCode.R) && !isRespawning) //isRespawning makes sure the player cant respawn until the camera has finished moving
        {
            StartCoroutine(respawnPlayer());
        }
        if (cameraHolder.transform.parent == null)
        {
            //for (int i = 0; i < ragdollObjects.Length; i++)
            //{
            //    ragdollObjects[i].GetComponent<CapsuleCollider>().enabled = false;

            //}

            if (isLerping)
            {
                isRespawning = true;
                //increment timer once per frame
                currentLerpTime += Time.deltaTime;
                if (currentLerpTime > lerpTime)
                {
                    currentLerpTime = 0;
                    isLerping = false;
                }
            }
            if (!isLerping)
            {
                //reparents the camera to the player
                cameraHolder.transform.parent = spawnedPlayer.transform;
                cameraHolder.transform.position = cameraPosition.transform.position;
                cameraHolder.transform.rotation = cameraPosition.transform.rotation;
                //allows the player to respawn again
                isRespawning = false;
                //allows the player to move again
                spawnedPlayer.GetComponent<PlayerMoveSlide>().enabled = true;

                //for (int i = 0; i < ragdollObjects.Length; i++)
                //{
                //    ragdollObjects[i].GetComponent<CapsuleCollider>().enabled = true;

                //}
            }

            float percentComplete = currentLerpTime / followSpeed;
            //moves and roates the camera to the from the old body to the new
            // might want to slow them down a bit, but this can be done later
            cameraHolder.transform.position = Vector3.Lerp(cameraHolder.transform.position, cameraPosition.transform.position, percentComplete);
            cameraHolder.transform.rotation = Quaternion.Lerp(cameraHolder.transform.rotation, cameraPosition.transform.rotation, percentComplete); 
        }
    }
    IEnumerator respawnPlayer()
    {
        isLerping = true;
        deadBody.transform.position = spawnedPlayer.transform.position;
        deadBody.transform.rotation = spawnedPlayer.transform.rotation;
        cameraHolder.transform.parent = null; //Removes the player as the cameras parent so they can be moved independantly
        spawnedPlayer.GetComponent<PlayerMoveSlide>().enabled = false; //stops the player from moving while they are being respawned and the camera is moving
        spawnedPlayer.transform.position = respawnPoint.transform.position;
        spawnedPlayer.transform.rotation = respawnPoint.transform.rotation;
        yield return new WaitForSeconds(0.1f);
        Instantiate(deadBody);
    }
}