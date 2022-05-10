using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GameObject doorGameObject;
    private IDoor door;

    public bool typeOpen = true;

    void Awake()
    {
        door = doorGameObject.GetComponent<IDoor>();
    }

    private void OnTriggerEnter(Collider other)
    {
        door = doorGameObject.GetComponent<IDoor>();
        if (typeOpen) {
            door.OpenDoor();
        }
        else if (!typeOpen)
        {
            door.CloseDoor();
        }
    }
}
