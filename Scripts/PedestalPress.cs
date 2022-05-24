using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalPress : MonoBehaviour
{
    public GameObject doorGameObject;
    public GameObject buttonGameObject;
    private IDoor door;
    private IDoor button;
    private int counter = 0;

    void Awake()
    {
        door = doorGameObject.GetComponent<IDoor>();
        button = buttonGameObject.GetComponent<IDoor>();
        counter = 0;
    }

    void Update()
    {
        if (counter == 0)//PlayerIsOn == true
        {
            door.CloseDoor();
            button.CloseDoor();
        }
        else
        {
            door.OpenDoor();
            button.OpenDoor();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "canPickUpObject")
        {
            counter++;
        }
        
    }

    //private void OnCollisionEmpty(Collision collision)

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "canPickUpObject")
        {
            counter--;
        }

    }
}
