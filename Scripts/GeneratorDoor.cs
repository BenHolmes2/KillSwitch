using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorDoor : MonoBehaviour
{
    public GameObject doorGameObject;
    public GameObject electricityArc;
    private GameObject electricityArc1;
    public GameObject arcStart;
    private Vector3 midPoint;
    private IDoor door;
    //private IDoor button;
    private GameObject pos;
    //private bool electrified = false;
    private int counter = 0;
    private int elecCounter = 0;

    void Awake()
    {
        door = doorGameObject.GetComponent<IDoor>();
        counter = 0;
        elecCounter = 0;
    }

    private void FixedUpdate()
    {
        if (counter == 0)//PlayerIsOn == true
        {
            door.CloseDoor();
        }
        else
        {
            door.OpenDoor();
        }

        if (elecCounter == 2)
        {
            midPoint = (pos.transform.position + arcStart.transform.position) / 2;
            electricityArc1.transform.position = midPoint;
            electricityArc1.GetComponent<LineRenderer>().SetPosition(0, pos.transform.position);
            electricityArc1.GetComponent<LineRenderer>().SetPosition(1, arcStart.transform.position);
        }
    }

    //void Update()
    //{
    //    if (counter == 0)//PlayerIsOn == true
    //    {
    //        door.CloseDoor();
    //    }
    //    else
    //    {
    //        door.OpenDoor();
    //    }

    //    if (elecCounter == 2)
    //    {
    //        midPoint = (pos.transform.position + arcStart.transform.position) / 2;
    //        electricityArc1.transform.position = midPoint;
    //        electricityArc1.GetComponent<LineRenderer>().SetPosition(0, pos.transform.position);
    //        electricityArc1.GetComponent<LineRenderer>().SetPosition(1, arcStart.transform.position);
    //    }



    //    ////cameraHolder.transform.rotation = Quaternion.Lerp(cameraHolder.transform.rotation, cameraPosition.transform.rotation, percentComplete);

    //    ////electricityArc.transform.s = midPoint;

    //    //electricityArc.transform.localScale = Vector3.Lerp(pos.transform.position, colliderObject.transform.position, 1f);
    //    //electricityArc.transform.position = midPoint;

    //}

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "QuickRigCharacter_Hips")
        {
            if (collision.gameObject.GetComponent<RagdollScript>().isElectrified)
            {
                counter++;
                if (elecCounter == 0)
                {
                    elecCounter = 1;
                }
            }
        }

        //if (collision.gameObject.GetComponent<RagdollScript>() != null)
        //{
        //    if (collision.gameObject.GetComponent<RagdollScript>().isElectrified)
        //    {
        //        counter++;
        //        if (elecCounter == 0)
        //        {
        //            elecCounter = 1;
        //        }
        //    }
        //}

        if (elecCounter == 1)
        {
            pos = collision.gameObject;
            midPoint = (pos.transform.position + arcStart.transform.position) / 2;
            electricityArc.transform.position = midPoint;
            //electricityArc.transform.position = pos.transform.position;
            electricityArc1 = Instantiate(electricityArc);
            electricityArc1.GetComponent<LineRenderer>().SetPosition(0, pos.transform.position);
            electricityArc1.GetComponent<LineRenderer>().SetPosition(1, arcStart.transform.position);
            elecCounter = 2;
        }

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

        if (collision.gameObject == pos)
        {
            if (electricityArc1 != null)
            {
                Destroy(electricityArc1);
            }
            
            elecCounter = 0;
        }

    }
}
