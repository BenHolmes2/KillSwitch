using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerGenerator : MonoBehaviour
{
    public GameObject doorGameObject;
    public GameObject doorOpenTrigger;
    private IDoor door;

    public bool typeOpen = true;

    void Awake()
    {
        door = doorGameObject.GetComponent<IDoor>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            door = doorGameObject.GetComponent<IDoor>();
            if (!typeOpen)
            {
                doorOpenTrigger.GetComponent<GeneratorDoor>().doorShut = true;
                doorOpenTrigger.GetComponent<GeneratorDoor>().counter = 0;
                door.CloseDoor();
            }
        }

    }
}
