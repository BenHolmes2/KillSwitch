using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject player;
    public Transform holdPos;
    public GameObject camera1;
    public float throwForce = 500f;
    public float pickUpRange = 20f;

    private GameObject heldObj;
    private GameObject hips;
    private GameObject leftUpLeg;
    private GameObject leftLeg;
    private GameObject rightUpLeg;
    private GameObject rightLeg;
    private GameObject spine;
    private GameObject leftArm;
    private GameObject leftForeArm;
    private GameObject leftHand;
    private GameObject rightArm;
    private GameObject rightForeArm;
    private GameObject rightHand;
    private GameObject head;
    private GameObject temp;

    private Rigidbody heldObjRb;
    private bool canThrow = true;
    CharacterController controller;

    //Rough script to enable the picking up and dropping of bodies
    //Will need a bunch more work to eliminate collisions between the body and the player
    //Waiting until we have our own model for the rag doll before i proceed further




    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

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
                    if (hit.transform.gameObject.tag == "canPickUp" || hit.transform.gameObject.tag == "canPickUpElec")
                    {
                        PickUpBody(hit.transform.gameObject);
                    }
                    if (hit.transform.gameObject.tag == "canPickUpObject")
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
    void PickUpBody(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>())
        {
            heldObj = pickUpObj;
            heldObj = heldObj.transform.root.gameObject;
            heldObj = heldObj.transform.Find("riggedd body 04").gameObject;
            heldObj = heldObj.transform.Find("QuickRigCharacter_Reference").gameObject;
            heldObj = heldObj.transform.Find("QuickRigCharacter_Hips").gameObject;

            //finding all of the colliders so we can ignore collsions from them
            //move this to a function
            // dont think these do anything
            //hips = heldObj;
            //leftUpLeg = hips.transform.Find("QuickRigCharacter_LeftUpLeg").gameObject;
            //leftLeg = leftUpLeg.transform.Find("QuickRigCharacter_LeftLeg").gameObject;
            //rightUpLeg = hips.transform.Find("QuickRigCharacter_RightUpLeg").gameObject;
            //rightLeg = rightUpLeg.transform.Find("QuickRigCharacter_RightLeg").gameObject;
            //spine = hips.transform.Find("QuickRigCharacter_Spine").gameObject;
            //spine = spine.transform.Find("QuickRigCharacter_Spine1").gameObject;
            //spine = spine.transform.Find("QuickRigCharacter_Spine2").gameObject;
            //leftArm = spine.transform.Find("QuickRigCharacter_LeftShoulder").gameObject;
            //leftArm = leftArm.transform.Find("QuickRigCharacter_LeftArm").gameObject;
            //leftForeArm = leftArm.transform.Find("QuickRigCharacter_LeftForeArm").gameObject;
            //leftHand = leftForeArm.transform.Find("QuickRigCharacter_LeftHand").gameObject;
            //rightArm = spine.transform.Find("QuickRigCharacter_RightShoulder").gameObject;
            //rightArm = rightArm.transform.Find("QuickRigCharacter_RightArm").gameObject;
            //rightForeArm = rightArm.transform.Find("QuickRigCharacter_RightForeArm").gameObject;
            //rightHand = rightForeArm.transform.Find("QuickRigCharacter_RightHand").gameObject;
            //head = spine.transform.Find("QuickRigCharacter_Neck").gameObject;
            //head = head.transform.Find("QuickRigCharacter_Head").gameObject;
            heldObjRb = heldObj.GetComponent<Rigidbody>();
            heldObjRb.transform.position = holdPos.transform.position;
            //// dont think these do anything
            ////not sure how to ignore collisions on the player since it is made up of multiple colliders
            //Physics.IgnoreCollision(controller, hips.GetComponent<Collider>(), true);
            //Physics.IgnoreCollision(controller, leftUpLeg.GetComponent<Collider>(), true);
            //Physics.IgnoreCollision(controller, leftLeg.GetComponent<Collider>(), true);
            //Physics.IgnoreCollision(controller, rightUpLeg.GetComponent<Collider>(), true);
            //Physics.IgnoreCollision(controller, rightLeg.GetComponent<Collider>(), true);
            //Physics.IgnoreCollision(controller, spine.GetComponent<Collider>(), true);
            //Physics.IgnoreCollision(controller, leftArm.GetComponent<Collider>(), true);
            //Physics.IgnoreCollision(controller, leftForeArm.GetComponent<Collider>(), true);
            //Physics.IgnoreCollision(controller, leftHand.GetComponent<Collider>(), true);
            //Physics.IgnoreCollision(controller, rightArm.GetComponent<Collider>(), true);
            //Physics.IgnoreCollision(controller, rightForeArm.GetComponent<Collider>(), true);
            //Physics.IgnoreCollision(controller, rightHand.GetComponent<Collider>(), true);
            //Physics.IgnoreCollision(controller, head.GetComponent<Collider>(), true);
        }
    }

    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>())
        {
            heldObj = pickUpObj;
            Debug.Log(heldObj);
            heldObjRb = heldObj.GetComponent<Rigidbody>();
            heldObjRb.transform.position = holdPos.transform.position;

        }
    }
    void DropObject()
    {
        // dont think these do anything
        //Physics.IgnoreCollision(controller, hips.GetComponent<Collider>(), false);
        //Physics.IgnoreCollision(controller, leftUpLeg.GetComponent<Collider>(), false);
        //Physics.IgnoreCollision(controller, leftLeg.GetComponent<Collider>(), false);
        //Physics.IgnoreCollision(controller, rightUpLeg.GetComponent<Collider>(), false);
        //Physics.IgnoreCollision(controller, rightLeg.GetComponent<Collider>(), false);
        //Physics.IgnoreCollision(controller, spine.GetComponent<Collider>(), false);
        //Physics.IgnoreCollision(controller, leftArm.GetComponent<Collider>(), false);
        //Physics.IgnoreCollision(controller, leftForeArm.GetComponent<Collider>(), false);
        //Physics.IgnoreCollision(controller, leftHand.GetComponent<Collider>(), false);
        //Physics.IgnoreCollision(controller, rightArm.GetComponent<Collider>(), false);
        //Physics.IgnoreCollision(controller, rightForeArm.GetComponent<Collider>(), false);
        //Physics.IgnoreCollision(controller, rightHand.GetComponent<Collider>(), false);
        //Physics.IgnoreCollision(controller, head.GetComponent<Collider>(), false);
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