using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class RagdollScript : MonoBehaviour
{
    public bool isElectrified = false;
    private GameObject ragdoll;
    private GameObject currObj;
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
    private GameObject gameController;


    //// Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController");
        ragdoll = gameObject;
        currObj = ragdoll;
        currObj = currObj.transform.root.gameObject;
        currObj = currObj.transform.Find("riggedd body 04").gameObject;
        currObj = currObj.transform.Find("QuickRigCharacter_Reference").gameObject;
        hips = currObj.transform.Find("QuickRigCharacter_Hips").gameObject;
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
    }

    //// Update is called once per frame
    void Update()
    {
        if (head.GetComponent<RagdollScript>().isElectrified)
        {
            head.GetComponent<VisualEffect>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LightningPoles")
        {
            isElectrified = true;

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
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(gameObject.transform.root.gameObject.name + gameObject.name);
        Debug.Log(collision.gameObject.transform.root.gameObject.name + collision.gameObject.name);
        //how do i get it to ignore the respawn tube 
        if (collision.gameObject.tag == "RespawnTube")
        {
            //these need to be turned off at somepoint
            Physics.IgnoreCollision(collision.gameObject.GetComponent<MeshCollider>(), hips.GetComponent<Collider>(), true);
            Physics.IgnoreCollision(collision.gameObject.GetComponent<MeshCollider>(), leftUpLeg.GetComponent<Collider>(), true);
            Physics.IgnoreCollision(collision.gameObject.GetComponent<MeshCollider>(), leftLeg.GetComponent<Collider>(), true);
            Physics.IgnoreCollision(collision.gameObject.GetComponent<MeshCollider>(), rightUpLeg.GetComponent<Collider>(), true);
            Physics.IgnoreCollision(collision.gameObject.GetComponent<MeshCollider>(), rightLeg.GetComponent<Collider>(), true);
            Physics.IgnoreCollision(collision.gameObject.GetComponent<MeshCollider>(), spine.GetComponent<Collider>(), true);
            Physics.IgnoreCollision(collision.gameObject.GetComponent<MeshCollider>(), leftArm.GetComponent<Collider>(), true);
            Physics.IgnoreCollision(collision.gameObject.GetComponent<MeshCollider>(), leftForeArm.GetComponent<Collider>(), true);
            Physics.IgnoreCollision(collision.gameObject.GetComponent<MeshCollider>(), leftHand.GetComponent<Collider>(), true);
            Physics.IgnoreCollision(collision.gameObject.GetComponent<MeshCollider>(), rightArm.GetComponent<Collider>(), true);
            Physics.IgnoreCollision(collision.gameObject.GetComponent<MeshCollider>(), rightForeArm.GetComponent<Collider>(), true);
            Physics.IgnoreCollision(collision.gameObject.GetComponent<MeshCollider>(), rightHand.GetComponent<Collider>(), true);
            Physics.IgnoreCollision(collision.gameObject.GetComponent<MeshCollider>(), head.GetComponent<Collider>(), true);
        }

        if (collision.gameObject != gameObject)
        {
            Debug.Log(gameObject.transform.root.gameObject.name + gameObject.name);
            Debug.Log(collision.gameObject.transform.root.gameObject.name + collision.gameObject.name);
            if (gameObject.tag == "canPickUpDeath")
            {
                if ((collision.gameObject.tag == "DeathSurface" || collision.gameObject.tag == "RespawnTube" || collision.gameObject.tag == "canPickUp") && gameController.GetComponent<GameController>().hitGround == false)
                {
                    gameController.GetComponent<GameController>().hitGround = true;
                }
            }
        }
    }
}
