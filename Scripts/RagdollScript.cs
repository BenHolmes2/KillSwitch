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
    private GameObject spineTemp;
    private GameObject leftArm;
    private GameObject leftForeArm;
    private GameObject leftHand;
    private GameObject rightArm;
    private GameObject rightForeArm;
    private GameObject rightHand;
    private GameObject head;
    private GameObject gameController;
    private GameObject player;
    private bool turningOff;
    private float turnOffDelay;
    private int frames = 0;


    //// Start is called before the first frame update
    void Start()
    {
        //Debug.Log("????");
        gameController = GameObject.Find("GameController");
        turnOffDelay = gameController.GetComponent<GameController>().ragdollTurnOffDelay;
        player = gameController.GetComponent<GameController>().spawnedPlayer;
        ragdoll = gameObject;
        currObj = ragdoll;
        currObj = currObj.transform.root.gameObject;
        //currObj = currObj.transform.Find("riggedd body 04").gameObject;
        //currObj = currObj.transform.Find("QuickRigCharacter_Reference").gameObject;
        //hips = currObj.transform.Find("QuickRigCharacter_Hips").gameObject;
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
        //Debug.Log("THIS IS THE THIGN IM LOOKING FOR");
        //Debug.Log(currObj.name);
        hips = currObj.transform.Find("Hip_Root_JNT").gameObject;
        leftUpLeg = hips.transform.Find("Left_Leg_JNT").gameObject;
        leftLeg = leftUpLeg.transform.Find("Left_Knee_JNT").gameObject;
        rightUpLeg = hips.transform.Find("Right_Leg_JNT").gameObject;
        rightLeg = rightUpLeg.transform.Find("Right_Knee_JNT").gameObject;
        spine = hips.transform.Find("Spine_01_Bottom_JNT").gameObject;
        spine = spine.transform.Find("Spine_02_Mid_JNT").gameObject;

        spineTemp = spine.transform.Find("Spine_03_Top_JNT").gameObject;
        spineTemp = spineTemp.transform.Find("Spine_00_Top_JNT").gameObject;

        leftArm = spineTemp.transform.Find("Left_Clavicle_JNT").gameObject;
        leftArm = leftArm.transform.Find("Left_Shoulder_JNT").gameObject;

        leftForeArm = leftArm.transform.Find("Left_Forarm_JNT").gameObject;
        leftHand = leftForeArm.transform.Find("Left_Wrist_JNT").gameObject;

        rightArm = spineTemp.transform.Find("Right_Clavicle_JNT").gameObject;
        rightArm = rightArm.transform.Find("Right_Shoulder_JNT").gameObject;

        rightForeArm = rightArm.transform.Find("Right_Forarm_JNT").gameObject;
        rightHand = rightForeArm.transform.Find("Right_Wrist_JNT").gameObject;
        head = spineTemp.transform.Find("Neck_JNT").gameObject;
        head = head.transform.Find("Head_JNT").gameObject;
    }

    //// Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerController>().heldObj != null)
        {
            frames = 0;
        }

        if (head != null)
        {
            if (head.GetComponent<RagdollScript>().isElectrified)
            {
                head.GetComponent<VisualEffect>().enabled = true;
            }
        }

        if (frames > 60)
        {
            Invoke("TurnOffRagdoll", turnOffDelay);
            turningOff = true;
            frames = 0;
        }

        //if (gameController.GetComponent<GameController>().hitGround)
        //{
            //Invoke("TurnOffRagdoll", 3);
        //    //StartCoroutine(TurnOffRagdoll());
        //}

        //if (player.GetComponent<PlayerController>().heldObj == null)
        //{
        //    if (hips.GetComponent<Rigidbody>().velocity.magnitude < 0.2f)
        //    {
        //        Invoke("TurnOffRagdoll", 3);
        //        //StartCoroutine(TurnOffRagdoll());      
        //    }
        //}
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
        //Debug.Log(gameObject.transform.root.gameObject.name + gameObject.name);
        //Debug.Log(collision.gameObject.transform.root.gameObject.name + collision.gameObject.name);
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
            //Debug.Log(gameObject.transform.root.gameObject.name + gameObject.name);
            //Debug.Log(collision.gameObject.transform.root.gameObject.name + collision.gameObject.name);
            if (gameObject.tag == "canPickUpDeath")
            {
                if ((collision.gameObject.tag == "DeathSurface" || collision.gameObject.tag == "RespawnTube" || collision.gameObject.tag == "canPickUp" || collision.gameObject.tag == "Spikes") && gameController.GetComponent<GameController>().hitGround == false)
                {
                    gameController.GetComponent<GameController>().hitGround = true;
                }
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.transform.root.gameObject != gameObject.transform.root.gameObject)
        {
            if (gameObject.tag == "canPickUpDeath")
            {
                if (collision.gameObject.CompareTag("DeathSurface") || collision.gameObject.CompareTag("canPickUpDeath") || collision.gameObject.CompareTag("canPickUp"))
                {
                    if (!turningOff)
                    {
                        if (gameObject.GetComponent<Rigidbody>().velocity.magnitude < 0.01)
                        {
                            frames++;
                        }
                    }
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {

        if (gameObject.tag == "canPickUpDeath")
        {
            if (collision.gameObject.tag == "DeathSurface" || gameObject.tag == "canPickUpDeath" || gameObject.tag == "canPickUp")
            {
                frames = 0;
            }
        }
    }

    //public IEnumerator TurnOffRagdoll()
    //{

    //    yield return new WaitForSeconds(3f);
    //    hips.GetComponent<Rigidbody>().isKinematic = true;
    //    leftUpLeg.GetComponent<Rigidbody>().isKinematic = true;
    //    leftLeg.GetComponent<Rigidbody>().isKinematic = true;
    //    rightUpLeg.GetComponent<Rigidbody>().isKinematic = true;
    //    rightLeg.GetComponent<Rigidbody>().isKinematic = true;
    //    spine.GetComponent<Rigidbody>().isKinematic = true;
    //    leftArm.GetComponent<Rigidbody>().isKinematic = true;
    //    leftForeArm.GetComponent<Rigidbody>().isKinematic = true;
    //    leftHand.GetComponent<Rigidbody>().isKinematic = true;
    //    rightArm.GetComponent<Rigidbody>().isKinematic = true;
    //    rightForeArm.GetComponent<Rigidbody>().isKinematic = true;
    //    rightHand.GetComponent<Rigidbody>().isKinematic = true;
    //    head.GetComponent<Rigidbody>().isKinematic = true;
    //}

    public void TurnOffRagdoll()
    {
        hips.GetComponent<Rigidbody>().isKinematic = true;
        leftUpLeg.GetComponent<Rigidbody>().isKinematic = true;
        leftLeg.GetComponent<Rigidbody>().isKinematic = true;
        rightUpLeg.GetComponent<Rigidbody>().isKinematic = true;
        rightLeg.GetComponent<Rigidbody>().isKinematic = true;
        spine.GetComponent<Rigidbody>().isKinematic = true;
        leftArm.GetComponent<Rigidbody>().isKinematic = true;
        leftForeArm.GetComponent<Rigidbody>().isKinematic = true;
        leftHand.GetComponent<Rigidbody>().isKinematic = true;
        rightArm.GetComponent<Rigidbody>().isKinematic = true;
        rightForeArm.GetComponent<Rigidbody>().isKinematic = true;
        rightHand.GetComponent<Rigidbody>().isKinematic = true;
        head.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void TurnOnRagdoll()
    {
        turningOff = false;
        hips.GetComponent<Rigidbody>().isKinematic = false;
        leftUpLeg.GetComponent<Rigidbody>().isKinematic = false;
        leftLeg.GetComponent<Rigidbody>().isKinematic = false;
        rightUpLeg.GetComponent<Rigidbody>().isKinematic = false;
        rightLeg.GetComponent<Rigidbody>().isKinematic = false;
        spine.GetComponent<Rigidbody>().isKinematic = false;
        leftArm.GetComponent<Rigidbody>().isKinematic = false;
        leftForeArm.GetComponent<Rigidbody>().isKinematic = false;
        leftHand.GetComponent<Rigidbody>().isKinematic = false;
        rightArm.GetComponent<Rigidbody>().isKinematic = false;
        rightForeArm.GetComponent<Rigidbody>().isKinematic = false;
        rightHand.GetComponent<Rigidbody>().isKinematic = false;
        head.GetComponent<Rigidbody>().isKinematic = false;
    }
}
