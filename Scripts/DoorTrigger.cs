using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GameObject doorGameObject;
    private IDoor door;

    public bool typeOpen = true;
    public bool typeAllowSpawn = true;

    void Awake()
    {
        door = doorGameObject.GetComponent<IDoor>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            door = doorGameObject.GetComponent<IDoor>();
            if (typeOpen)
            {
                door.OpenDoor();
            }
            else if (!typeOpen)
            {
                door.CloseDoor();
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            door = doorGameObject.GetComponent<IDoor>();
            if (typeOpen)
            {
                door.CloseDoor();
            }
            if (!typeAllowSpawn)
            {
                other.gameObject.GetComponent<PlayerControllerAnimated>().gameController.GetComponent<GameControllerAnimated>().respawnAllowed = false;
            }
        }
    }
}
