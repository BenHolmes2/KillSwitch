using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public GameController gameController;
    public int fanForceBody = 100;
    public int fanForceBodyInitial = 0;
    public float fanForcePlayer = 0.1f;
    public float fanForcePlayerInitial = 0f;
    public bool isVertical = false;
    private GameObject player;
    /// Start is called before the first frame update
    void Start()
    {
        player = gameController.spawnedPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        player = gameController.spawnedPlayer;

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
                    player.GetComponent<PlayerController>().movementDir += this.transform.forward * fanForcePlayerInitial;
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
        if (other.gameObject.CompareTag("Player"))
        {
            //this currently doesnt work very well when the fan is trying to push the player upwards
            if (!gameController.isRespawn)
            {
                if (isVertical)
                {
                    player.GetComponent<PlayerController>().movementDir += this.transform.forward * fanForcePlayer;
                }
                else
                {
                    player.GetComponent<CharacterController>().Move(this.transform.forward * fanForcePlayer);
                }
            }
        }
        if (other.gameObject.CompareTag("canPickUp") || other.gameObject.CompareTag("canPickUpDeath"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(this.transform.forward * fanForceBody);
        }
        //if we want to make it so that the player is pushed back faster while holding a body then we should access the player controller
        //so we can see if the player is carrying a body

    }
}
