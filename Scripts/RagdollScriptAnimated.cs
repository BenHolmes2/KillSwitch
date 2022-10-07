using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class RagdollScriptAnimated : MonoBehaviour
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
    private GameObject characterMesh;
    private Renderer characterRenderer;
    public bool turningOff;
    private float turnOffDelay;
    private int frames = 0;
    private float electricityEffectModifier;
    private float negative = -1f;

    private double runningTime;
    private double destroyTime;



    //// Start is called before the first frame update
    void Start()
    {
        //Debug.Log("????");
        gameController = GameObject.Find("GameController");
        turnOffDelay = gameController.GetComponent<GameControllerAnimated>().ragdollTurnOffDelay;
        player = gameController.GetComponent<GameControllerAnimated>().spawnedPlayer;
        ragdoll = gameObject;
        currObj = ragdoll;
        currObj = currObj.transform.root.gameObject;
        currObj = currObj.transform.Find("parent").gameObject;
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
        hips = currObj.transform.Find("mixamorig:Hips1").gameObject;
        leftUpLeg = hips.transform.Find("mixamorig:LeftUpLeg").gameObject;
        leftLeg = leftUpLeg.transform.Find("mixamorig:LeftLeg").gameObject;
        rightUpLeg = hips.transform.Find("mixamorig:RightUpLeg").gameObject;
        rightLeg = rightUpLeg.transform.Find("mixamorig:RightLeg").gameObject;
        spine = hips.transform.Find("mixamorig:Spine").gameObject;
        spine = spine.transform.Find("mixamorig:Spine1").gameObject;

        spineTemp = spine.transform.Find("mixamorig:Spine2").gameObject;

        leftArm = spineTemp.transform.Find("mixamorig:LeftShoulder").gameObject;
        leftArm = leftArm.transform.Find("mixamorig:LeftArm").gameObject;

        leftForeArm = leftArm.transform.Find("mixamorig:LeftForeArm").gameObject;
        leftHand = leftForeArm.transform.Find("mixamorig:LeftHand").gameObject;

        rightArm = spineTemp.transform.Find("mixamorig:RightShoulder").gameObject;
        rightArm = rightArm.transform.Find("mixamorig:RightArm").gameObject;

        rightForeArm = rightArm.transform.Find("mixamorig:RightForeArm").gameObject;
        rightHand = rightForeArm.transform.Find("mixamorig:RightHand").gameObject;
        head = spineTemp.transform.Find("mixamorig:Neck").gameObject;
        head = head.transform.Find("mixamorig:Head").gameObject;
    }

    //// Update is called once per frame
    void Update()
    {
        runningTime = Time.timeAsDouble;
        Debug.Log(destroyTime);
        if (runningTime > destroyTime && destroyTime != 0)
        {
            Destroy(gameObject.transform.root.gameObject);
        }

        if (!gameObject.GetComponent<Rigidbody>().isKinematic)
        {
            runningTime = 0;
            destroyTime = 0;
        }

        //if (gameObject.GetComponent<Rigidbody>().velocity.magnitude > 15)
        //{
        //    gameObject.GetComponent<Rigidbody>().velocity = 15;
        //}


        //if (player.GetComponent<PlayerControllerAnimated>().heldObj != null)
        //{
        //    frames = 0;
        //}

        if (head != null && characterRenderer == null)
        {
            if (head.GetComponent<RagdollScriptAnimated>().isElectrified)
            {
                characterMesh = head.transform.root.gameObject;
                characterMesh = characterMesh.transform.Find("parent").gameObject;
                characterMesh = characterMesh.transform.Find("KS_CHaracter_Rig_GRP").gameObject;
                characterMesh = characterMesh.transform.Find("Character_Mesh").gameObject;
                characterMesh = characterMesh.transform.Find("CHarcter_Skin_Geo").gameObject;

                characterRenderer = characterMesh.GetComponent<Renderer>();
                
                characterRenderer.material.SetFloat("_EmissionForHoles", 0f);
            }
        }

        if (characterRenderer != null)
        {
            electricityEffectModifier = Mathf.PingPong(Time.time, 0.8f);
            electricityEffectModifier *= negative;
            characterRenderer.material.SetFloat("_EmissionForHoles", electricityEffectModifier);

        }

        if (gameObject.GetComponent<Rigidbody>().velocity.magnitude == 0 && !gameController.GetComponent<GameControllerAnimated>().isRespawn && turningOff == false)
        {
            TurnOffRagdoll();
            destroyTime = Time.timeAsDouble + 180;
        }

        //if (frames > 240)
        //{
        //    TurnOffRagdoll();
        //    //Invoke("TurnOffRagdoll", turnOffDelay);
        //    turningOff = true;
        //    frames = 0;
        //}

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

            hips.GetComponent<RagdollScriptAnimated>().isElectrified = true;
            leftUpLeg.GetComponent<RagdollScriptAnimated>().isElectrified = true;
            leftLeg.GetComponent<RagdollScriptAnimated>().isElectrified = true;
            rightUpLeg.GetComponent<RagdollScriptAnimated>().isElectrified = true;
            rightLeg.GetComponent<RagdollScriptAnimated>().isElectrified = true;
            spine.GetComponent<RagdollScriptAnimated>().isElectrified = true;
            leftArm.GetComponent<RagdollScriptAnimated>().isElectrified = true;
            leftForeArm.GetComponent<RagdollScriptAnimated>().isElectrified = true;
            leftHand.GetComponent<RagdollScriptAnimated>().isElectrified = true;
            rightArm.GetComponent<RagdollScriptAnimated>().isElectrified = true;
            rightForeArm.GetComponent<RagdollScriptAnimated>().isElectrified = true;
            rightHand.GetComponent<RagdollScriptAnimated>().isElectrified = true;
            head.GetComponent<RagdollScriptAnimated>().isElectrified = true;
        }

        if (other.gameObject.CompareTag("GearBox"))
        {
            if (!gameController.GetComponent<GameControllerAnimated>().isRespawn)
            {
                //TurnOffRagdoll();
                if (other.gameObject.GetComponent<Rigidbody>() != null)
                {
                    other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!gameController.GetComponent<GameControllerAnimated>().isRespawn)
        {
            //TurnOnRagdoll();
            if (other.gameObject.GetComponent<Rigidbody>() != null)
            {
                other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
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

        if (collision.gameObject.transform.root.gameObject != gameObject.transform.root.gameObject)
        {
            //Debug.Log(gameObject.transform.root.gameObject.name + gameObject.name);
            //Debug.Log(collision.gameObject.transform.root.gameObject.name + collision.gameObject.name);
            if (gameObject.tag == "canPickUpDeath")
            {
                if ((collision.gameObject.CompareTag("DeathSurface") || collision.gameObject.CompareTag("RespawnTube") || collision.gameObject.CompareTag("canPickUp") || collision.gameObject.CompareTag("Spikes") || collision.gameObject.CompareTag("GearBox")) && gameController.GetComponent<GameControllerAnimated>().hitGround == false)
                {
                    gameController.GetComponent<GameControllerAnimated>().hitGround = true;
                }
            }
        }
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.transform.root.gameObject != gameObject.transform.root.gameObject)
    //    {
    //        if (gameObject.tag == "canPickUpDeath")
    //        {
    //            if (collision.gameObject.CompareTag("DeathSurface") || collision.gameObject.CompareTag("canPickUpDeath") || collision.gameObject.CompareTag("canPickUp"))
    //            {
    //                if (!turningOff && !gameController.GetComponent<GameControllerAnimated>().isRespawn)
    //                {
    //                    if (gameObject.GetComponent<Rigidbody>().velocity.magnitude < 0.1)
    //                    {
    //                        frames++;
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.transform.root.gameObject != gameObject.transform.root.gameObject)
    //    {

    //        if (gameObject.tag == "canPickUpDeath")
    //        {
    //            if (collision.gameObject.tag == "DeathSurface" || gameObject.tag == "canPickUpDeath" || gameObject.tag == "canPickUp")
    //            {
    //                frames = 0;
    //            }
    //        }
    //    }
    //}

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
        turningOff = true;
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

    public void ClampVelocity()
    {
        int clamp = -5;
        hips.GetComponent<Rigidbody>().velocity = new Vector3(0, clamp, 0);
        leftUpLeg.GetComponent<Rigidbody>().velocity = new Vector3(0, clamp, 0);
        leftLeg.GetComponent<Rigidbody>().velocity = new Vector3(0, clamp, 0);
        rightUpLeg.GetComponent<Rigidbody>().velocity = new Vector3(0, clamp, 0);
        rightLeg.GetComponent<Rigidbody>().velocity = new Vector3(0, clamp, 0);
        spine.GetComponent<Rigidbody>().velocity = new Vector3(0, clamp, 0);
        leftArm.GetComponent<Rigidbody>().velocity = new Vector3(0, clamp, 0);
        leftForeArm.GetComponent<Rigidbody>().velocity = new Vector3(0, clamp, 0);
        leftHand.GetComponent<Rigidbody>().velocity = new Vector3(0, clamp, 0);
        rightArm.GetComponent<Rigidbody>().velocity = new Vector3(0, clamp, 0);
        rightForeArm.GetComponent<Rigidbody>().velocity = new Vector3(0, clamp, 0);
        rightHand.GetComponent<Rigidbody>().velocity = new Vector3(0, clamp, 0);
        head.GetComponent<Rigidbody>().velocity = new Vector3(0, clamp, 0);

        //hips.GetComponent<Rigidbody>().velocity = clamp;
        //leftUpLeg.GetComponent<Rigidbody>().velocity.y
        //leftLeg.GetComponent<Rigidbody>().velocity.y
        //rightUpLeg.GetComponent<Rigidbody>().velocity.y
        //rightLeg.GetComponent<Rigidbody>().velocity.y
        //spine.GetComponent<Rigidbody>().velocity.y
        //leftArm.GetComponent<Rigidbody>().velocity.y
        //leftForeArm.GetComponent<Rigidbody>().velocity.y
        //leftHand.GetComponent<Rigidbody>().velocity.y
        //rightArm.GetComponent<Rigidbody>().velocity.y
        //rightForeArm.GetComponent<Rigidbody>().velocity.y
        //rightHand.GetComponent<Rigidbody>().velocity.y
        //head.GetComponent<Rigidbody>().velocity.y

    }
}
