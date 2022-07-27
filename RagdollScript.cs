using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class RagdollScript : MonoBehaviour
{
    public bool isElectrified = false;
    private GameObject ragdoll;
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

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    void Update()
    {
        if (head != null)
        {
            head.GetComponent<VisualEffect>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LightningPoles")
        {
            isElectrified = true;
            ragdoll = gameObject;
            heldObj = ragdoll;
            heldObj = heldObj.transform.root.gameObject;
            heldObj = heldObj.transform.Find("riggedd body 04").gameObject;
            heldObj = heldObj.transform.Find("QuickRigCharacter_Reference").gameObject;
            heldObj = heldObj.transform.Find("QuickRigCharacter_Hips").gameObject;

            hips = heldObj;
            leftUpLeg = hips.transform.Find("QuickRigCharacter_LeftUpLeg").gameObject;
            leftLeg = leftUpLeg.transform.Find("QuickRigCharacter_LeftLeg").gameObject;
            rightUpLeg = hips.transform.Find("QuickRigCharacter_RightUpLeg").gameObject;
            rightLeg = rightUpLeg.transform.Find("QuickRigCharacter_RightLeg").gameObject;
            spine = hips.transform.Find("QuickRigCharacter_Spine").gameObject;
            spine = spine.transform.Find("QuickRigCharacter_Spine1").gameObject;
            spine = spine.transform.Find("QuickRigCharacter_Spine2").gameObject;
            leftArm = spine.transform.Find("QuickRigCharacter_LeftShoulder").gameObject;
            leftArm = leftArm.transform.Find("QuickRigCharacter_LeftArm").gameObject;
            leftForeArm = leftArm.transform.Find("QuickRigCharacter_LeftForeArm").gameObject;
            leftHand = leftForeArm.transform.Find("QuickRigCharacter_LeftHand").gameObject;
            rightArm = spine.transform.Find("QuickRigCharacter_RightShoulder").gameObject;
            rightArm = rightArm.transform.Find("QuickRigCharacter_RightArm").gameObject;
            rightForeArm = rightArm.transform.Find("QuickRigCharacter_RightForeArm").gameObject;
            rightHand = rightForeArm.transform.Find("QuickRigCharacter_RightHand").gameObject;
            head = spine.transform.Find("QuickRigCharacter_Neck").gameObject;
            head = head.transform.Find("QuickRigCharacter_Head").gameObject;

            hips.GetComponent<RagdollScript>().isElectrified = true;
            leftUpLeg.GetComponent<RagdollScript>().isElectrified = true;
            leftLeg.GetComponent<RagdollScript>().isElectrified = true;
            rightUpLeg.GetComponent<RagdollScript>().isElectrified = true;
            rightLeg.GetComponent<RagdollScript>().isElectrified = true;
            spine.GetComponent<RagdollScript>().isElectrified = true;
            leftArm.GetComponent<RagdollScript>().isElectrified = true;
            leftForeArm.GetComponent<RagdollScript>().isElectrified = true;
            leftHand.GetComponent<RagdollScript>().isElectrified = true;
            rightArm.GetComponent<RagdollScript>().isElectrified = true;
            rightForeArm.GetComponent<RagdollScript>().isElectrified = true;
            rightHand.GetComponent<RagdollScript>().isElectrified = true;
            head.GetComponent<RagdollScript>().isElectrified = true;

            hips.tag = "canPickUpElec";
            leftUpLeg.tag = "canPickUpElec";
            leftLeg.tag = "canPickUpElec";
            rightUpLeg.tag = "canPickUpElec";
            rightLeg.tag = "canPickUpElec";
            spine.tag = "canPickUpElec";
            leftArm.tag = "canPickUpElec";
            leftForeArm.tag = "canPickUpElec";
            leftHand.tag = "canPickUpElec";
            rightArm.tag = "canPickUpElec";
            rightForeArm.tag = "canPickUpElec";
            rightHand.tag = "canPickUpElec";
            head.tag = "canPickUpElec";
        }

        //will need to figure out how to get every body part to have their boolean changed too
    }



}