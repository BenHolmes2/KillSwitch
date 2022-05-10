using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBody : MonoBehaviour
{
    public GameObject player;
    public Transform holdPos;
    public GameObject camera1;
    public float throwForce = 500f;
    public float pickUpRange = 20f;

    private GameObject heldObj;
    private GameObject temp;
    private GameObject temp1;
    private GameObject temp2;
    private GameObject temp3;
    private Rigidbody heldObjRb;
    private bool canThrow = true;


    //Rough script to enable the picking up and dropping of bodies
    //Will need a bunch more work to eliminate collisions between the body and the player
    //Waiting until we have our own model for the rag doll before i proceed further

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObj == null)
            {

                RaycastHit hit; // why is this casting out of the characters feet
                if (Physics.Raycast(camera1.transform.position, camera1.transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    Debug.DrawLine(camera1.transform.position, hit.point, Color.white, 5f);
                    Debug.Log(hit.transform.gameObject.tag);
                    Debug.Log(hit.transform.gameObject.name);
                    if (hit.transform.gameObject.tag == "canPickUp" || hit.transform.gameObject.tag == "canPickUpHips")
                    {
                        PickUpObject(hit.transform.gameObject);
                    }
                }
            }
            else
            {
                DropObject();
            }
        }
        if (heldObj != null)
        {
            MoveObject();
            if (Input.GetKeyDown(KeyCode.Mouse0) && canThrow == true) 
            {
                ThrowObject();
            }

        }
    }
    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>())
        {
            heldObj = pickUpObj;
            heldObj = heldObj.transform.root.gameObject;
            heldObj = heldObj.transform.Find("riggedd body 04").gameObject;
            heldObj = heldObj.transform.Find("QuickRigCharacter_Reference").gameObject;
            heldObj = heldObj.transform.Find("QuickRigCharacter_Hips").gameObject;
            heldObjRb = heldObj.GetComponent<Rigidbody>();
            heldObjRb.transform.position = holdPos.transform.position;
            //not sure how to ignore collisions on the player since it is made up of multiple colliders
            //Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }
    void DropObject()
    {
        //Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        //heldObj.transform.parent = null;
        heldObj = null;
    }
    void MoveObject()
    {
        heldObj.transform.position = holdPos.transform.position;
    }

    void ThrowObject()
    {
        //Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        //heldObj.transform.parent = null;
        heldObjRb.AddForce(transform.forward * throwForce);
        heldObj = null;
    }
}