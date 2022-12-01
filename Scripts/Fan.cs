using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public GameControllerAnimated gameController;
    private GameObject tempObj;
    public int fanForceBody = 100;
    public int fanForceBodyInitial = 0;
    public float fanForcePlayer = 0.1f;
    public float fanForcePlayerInitial = 0f;
    public int verticalFanPlayerExit;
    public bool isVertical = false;
    public int counterFPS;
    public int exitMulitplier = 1;
    private GameObject player;
    /// Start is called before the first frame update
    /// 

    private void Start()
    {
        tempObj = GameObject.Find("GameController");
        gameController = tempObj.GetComponent<GameControllerAnimated>();
        player = gameController.spawnedPlayer;
    }

    private void Update()
    {
        if (player == null)
        {
            tempObj = GameObject.Find("GameController");
            gameController = tempObj.GetComponent<GameControllerAnimated>();
            player = gameController.spawnedPlayer;
        }
    }

    private void OnTriggerEnter(Collider other) //this provides the player or body with some initial velocity
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //this currently doesnt work very well when the fan is trying to push the player upwards
            if (!gameController.isRespawn)
            {
                if (isVertical)
                {
                    player.GetComponent<PlayerControllerAnimated>().movementDir += this.transform.forward * fanForcePlayerInitial;
                    player.GetComponent<PlayerControllerAnimated>().throwForce = 10;
                }
            }
        }
        if (other.gameObject.CompareTag("canPickUp") || other.gameObject.CompareTag("canPickUpDeath"))
        {
            if (isVertical)
            {
                other.gameObject.GetComponent<Rigidbody>().AddForce(this.transform.forward * fanForceBodyInitial);
            }
        }
    }

    private void OnTriggerStay(Collider other) //this provides the player or body with some velocity as long as they remain within the collider
    {
        //The if statement below provides force for the player when they are in the fan collider
        if (other.gameObject.CompareTag("Player"))
        {
            Physics.IgnoreLayerCollision(6, 3, true);
            counterFPS++; //this is a counter that checks how many frames the player has been in the fan collider 
            if (!gameController.isRespawn) //this checks to see if the player is respawning
            {
                //The if-else statement below checks whether the fan is vertical or not, this is important as the movement needs to be applied differently
                //between vertical and horizontal fans.
                if (isVertical)
                {
                    player.GetComponent<PlayerControllerAnimated>().movementDir += this.transform.forward * fanForcePlayer;
                }
                else
                {
                    player.GetComponent<CharacterController>().Move(this.transform.forward * fanForcePlayer);
                }
            }
            //this is used is used to increment this value roughly every second the player stays in the collider
            if (counterFPS % 60 == 0)
            {
                verticalFanPlayerExit++;
            }
        }
        //The if statement below applies force to each part of the ragdoll that is within the fan collider
        if (other.gameObject.CompareTag("canPickUp") || other.gameObject.CompareTag("canPickUpDeath"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(this.transform.forward * fanForceBody);
        }
        //if we want to make it so that the player is pushed back faster while holding a body then we should access the player controller
        //so we can see if the player is carrying a body

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Physics.IgnoreLayerCollision(6, 3, false);
            //this currently doesnt work very well when the fan is trying to push the player upwards
            if (!gameController.isRespawn)
            {
                if (isVertical)
                {
                    Debug.Log("check1");
                    player.GetComponent<PlayerControllerAnimated>().movementDir += new Vector3(0, exitMulitplier * verticalFanPlayerExit, 0);
                    player.GetComponent<PlayerControllerAnimated>().throwForce = 4;
                }
            }
        }
        counterFPS = 0;
        verticalFanPlayerExit = 0;
    }
}
