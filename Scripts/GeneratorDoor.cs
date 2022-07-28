using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorDoor : MonoBehaviour
{
    public GameObject doorGameObject;
    //public GameObject electricityArc;
    //public GameObject colliderObject;
    //private Vector3 midPoint;
    private IDoor door;
    private IDoor button;
    //private GameObject pos;
    //private bool electrified = false;
    private int counter = 0;
    //private int elecCounter = 0;

    void Awake()
    {
        door = doorGameObject.GetComponent<IDoor>();
        counter = 0;
        //elecCounter = 0;
    }

    void Update()
    {
        if (counter == 0)//PlayerIsOn == true
        {
            door.CloseDoor();
        }
        else
        {
            door.OpenDoor();
        }

       //  midPoint = pos.transform.position - colliderObject.transform.position;

       // //cameraHolder.transform.rotation = Quaternion.Lerp(cameraHolder.transform.rotation, cameraPosition.transform.rotation, percentComplete);

       //// electricityArc.transform.s = midPoint;

       // electricityArc.transform.localScale = Vector3.Lerp(pos.transform.position, colliderObject.transform.position, 1f);
       // electricityArc.transform.position = midPoint;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<RagdollScript>() != null)
        {
            if (collision.gameObject.GetComponent<RagdollScript>().isElectrified)
            {
                counter++;
                //elecCounter = 1;
            }
        }

        //if (elecCounter == 1)
        //{
        //    pos = collision.gameObject;
        //    electricityArc.transform.position = pos.transform.position;
        //    electricityArc = Instantiate(electricityArc);
        //}
        
    }

    //private void OnCollisionEmpty(Collision collision)

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<RagdollScript>() != null)
        {
            if (collision.gameObject.GetComponent<RagdollScript>().isElectrified)
            {
                counter--;
            }
        }

    }
}
