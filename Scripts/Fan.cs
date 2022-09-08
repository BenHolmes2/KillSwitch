using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public GameController gameController;
    public int fanForceBody = 100;
    public float fanForcePlayer = 0.1f;
    public bool isVertical = false;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = gameController.spawnedPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        player = gameController.spawnedPlayer;

    }

    private void OnTriggerStay(Collider other)
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
