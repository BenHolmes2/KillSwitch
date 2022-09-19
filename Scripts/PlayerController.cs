using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public Transform holdPos;
    public float throwForce = 5f;
    public float pickUpRange = 20f;

    public GameObject heldObj;
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
    //public GameObject reticleCanvas;
    //public GameObject pickUpCanvas;


    private Rigidbody heldObjRb;
    private bool canThrow = true;
    public CharacterController controller;

    public float mouseSensitivity = 100f;

    public Transform playerBody;
    public GameObject cameraObj;

    float xRotation = 0f;

    public float speed = 10.0f;
    public float jumpForce = 10.0f;
    public float gravity = 20.0f;
    private Vector3 movementInput = Vector3.zero;
    public Vector3 movementDir = Vector3.zero;
    private Vector3 airMovementDir = Vector3.zero;

    public GameController gameController;
    private GameObject tempObj;

    private RaycastHit pickUpHit;
    private RaycastHit cursorHit;

    public AudioClip Footsteps;
    public AudioSource StepSource;

    public AudioSource deathSource;
    private AudioClip[] deathSounds = new AudioClip[4];
    private int deathGruntInt;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        tempObj = GameObject.Find("GameController");
        gameController = tempObj.GetComponent<GameController>();
        Cursor.lockState = CursorLockMode.Locked;


        StepSource = this.gameObject.AddComponent<AudioSource>();
        StepSource.loop = true;
        StepSource.playOnAwake = true;
        if (Footsteps != null)
            StepSource.clip = Footsteps;
        StepSource.volume = 1f;

        deathSounds[0] = Resources.Load("DeathGrunt1") as AudioClip;
        deathSounds[1] = Resources.Load("DeathGrunt2") as AudioClip;
        deathSounds[2] = Resources.Load("DeathGrunt3") as AudioClip;
        deathSounds[3] = Resources.Load("DeathWilhelm") as AudioClip;
    }

    // Update is called once per frame
    void Update()
    {

        PlayerLook();
        PlayerMove();

        if (Physics.Raycast(cameraObj.transform.position, cameraObj.transform.TransformDirection(Vector3.forward), out cursorHit, pickUpRange))
        {
            if (cursorHit.transform.gameObject.tag == "canPickUp" || cursorHit.transform.gameObject.tag == "canPickUpDeath")
            {
                //Debug.Log(cursorHit.transform.gameObject.name);
                gameController.reticleCanvas.SetActive(false);
                gameController.pickUpCanvas.SetActive(true);
            }
            else
            {
                gameController.reticleCanvas.SetActive(true);
                gameController.pickUpCanvas.SetActive(false);
            }
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObj == null)
            {
                if (Physics.Raycast(cameraObj.transform.position, cameraObj.transform.TransformDirection(Vector3.forward), out pickUpHit, pickUpRange))
                {
                    //Debug.DrawLine(cameraObj.transform.position, pickUpHit.point, Color.white, 5f);
                    //Debug.Log(pickUpHit.transform.gameObject.tag);
                    //Debug.Log(pickUpHit.transform.gameObject.name);
                    if (pickUpHit.transform.gameObject.tag == "canPickUp" || pickUpHit.transform.gameObject.tag == "canPickUpDeath")
                    {
                        PickUpBody(pickUpHit.transform.gameObject);
                    }
                    //currently not needed, reimplement if we want to be able to pick up objects again
                    //if (hit.transform.gameObject.tag == "canPickUpObject")
                    //{
                    //    PickUpObject(hit.transform.gameObject);
                    //}
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
            //finding all of the colliders so we can ignore collsions from them
            //move this to a function
            hips = heldObj.transform.Find("Hip_Root_JNT").gameObject;
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
            heldObj = hips;
            heldObjRb = heldObj.GetComponent<Rigidbody>();
            //heldObjRb.transform.position = holdPos.transform.position;
            Debug.Log("----------------------------------------");
            Debug.Log(heldObj);
            Debug.Log(heldObjRb);
            Debug.Log("----------------------------------------");
            heldObjRb.transform.position = holdPos.transform.position;

            //heldObj.gameObject.transform.root.gameObject.layer = 6;

            heldObj.GetComponent<RagdollScript>().TurnOnRagdoll();

            ToggleCollisions(6);
        }
    }

    void PlayerMove()
    {
        movementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));

        if (movementInput.magnitude > 1.0f)
        {
            movementInput = movementInput.normalized;
        }

        // When player is in the air
        //to implement jump grace period, check ground check in update and then create a small timer that allows the player to still jump
        if (!controller.isGrounded)
        {
            airMovementDir = new Vector3(movementInput.x, 0.0f, movementInput.z);

            airMovementDir.x *= speed;
            airMovementDir.z *= speed;

            movementDir.x = airMovementDir.x;
            movementDir.z = airMovementDir.z;

        }


        if (controller.isGrounded)
        {
            movementDir = new Vector3(movementInput.x, 0.0f, movementInput.z);

            if (Input.GetButton("Jump"))
            {
                movementDir.y = jumpForce;
            }

            movementDir.x *= speed;
            movementDir.z *= speed;

        }

        movementDir = transform.TransformDirection(movementDir);
        controller.Move(movementDir * Time.deltaTime);
        //Debug.Log(movementDir);
        //Debug.Log(controller.velocity.magnitude);
        //Debug.Log(controller.velocity);

        //float stepsOffset = 0.5f;
        //float time = Time.deltaTime;
        //if ((movementDir.x > 2.0f || movementDir.x < -2.0f || movementDir.z > 2.0f || movementDir.z < -2.0f) && )


        if (controller.velocity.magnitude > 2f && StepSource.isPlaying == false && controller.isGrounded)
        {
            StepSource.PlayOneShot(Footsteps);
            //float time = Time.deltaTime;
        }
        //else
        //{

        //}

        movementDir.y -= gravity * Time.deltaTime;
        movementDir.x -= gravity * Time.deltaTime;
        movementDir.z -= gravity * Time.deltaTime;
        //Debug.Log(movementDir);

    }

    void PlayerLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        cameraObj.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
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
    public void DropObject()
    {
        if (heldObj != null)
        {
            //heldObj.gameObject.layer.Equals(0);
            heldObj.GetComponent<RagdollScript>().TurnOnRagdoll();
            heldObj = null;
            ToggleCollisions(0);
        }
    }
    void MoveObject()
    {
        heldObj.GetComponent<RagdollScript>().TurnOnRagdoll();
        //heldObj.transform.position = holdPos.transform.position;
        //heldObj.transform.rotation = holdPos.transform.rotation;
        heldObjRb.transform.position = holdPos.transform.position;
        heldObjRb.transform.rotation = holdPos.transform.rotation;

    }

    void ThrowObject()
    {
        //heldObj.gameObject.layer.Equals(0);
        heldObj.GetComponent<RagdollScript>().TurnOnRagdoll();
  
        ToggleCollisions(0);
        heldObjRb.transform.rotation = cameraObj.transform.rotation;
        //heldObjRb.velocity = (cameraObj.transform.forward * throwForce);
        hips.GetComponent<Rigidbody>().velocity = (cameraObj.transform.forward * throwForce);
        leftUpLeg.GetComponent<Rigidbody>().velocity = (cameraObj.transform.forward * throwForce);
        leftLeg.GetComponent<Rigidbody>().velocity = (cameraObj.transform.forward * throwForce);
        rightUpLeg.GetComponent<Rigidbody>().velocity = (cameraObj.transform.forward * throwForce);
        rightLeg.GetComponent<Rigidbody>().velocity = (cameraObj.transform.forward * throwForce);
        spine.GetComponent<Rigidbody>().velocity = (cameraObj.transform.forward * throwForce);
        leftArm.GetComponent<Rigidbody>().velocity = (cameraObj.transform.forward * throwForce);
        leftForeArm.GetComponent<Rigidbody>().velocity = (cameraObj.transform.forward * throwForce);
        leftHand.GetComponent<Rigidbody>().velocity = (cameraObj.transform.forward * throwForce);
        rightArm.GetComponent<Rigidbody>().velocity = (cameraObj.transform.forward * throwForce);
        rightForeArm.GetComponent<Rigidbody>().velocity = (cameraObj.transform.forward * throwForce);
        hips.GetComponent<Rigidbody>().velocity = (cameraObj.transform.forward * throwForce);
        rightHand.GetComponent<Rigidbody>().velocity = (cameraObj.transform.forward * throwForce);
        head.GetComponent<Rigidbody>().velocity = (cameraObj.transform.forward * throwForce);
        heldObj = null;
    }

    void ToggleCollisions(int toggle)
    {
        hips.layer = toggle;
        leftUpLeg.layer = toggle;
        leftLeg.layer = toggle;
        rightUpLeg.layer = toggle;
        rightLeg.layer = toggle;
        spine.layer = toggle;
        leftArm.layer = toggle;
        leftForeArm.layer = toggle;
        leftHand.layer = toggle;
        rightArm.layer = toggle;
        rightForeArm.layer = toggle;
        hips.layer = toggle;
        rightHand.layer = toggle;
        head.layer = toggle;

        //this ignores collisions between the body and the envrionment but not the gearbox
        //Physics.IgnoreLayerCollision(6, 0, toggle);

        //these ignore collisions stop the ragdoll from clipping with the player and itself when being held, but casue issues
        //Physics.IgnoreCollision(controller, hips.GetComponent<Collider>(), toggle);
        //Physics.IgnoreCollision(controller, leftUpLeg.GetComponent<Collider>(), toggle);
        //Physics.IgnoreCollision(controller, leftLeg.GetComponent<Collider>(), toggle);
        //Physics.IgnoreCollision(controller, rightUpLeg.GetComponent<Collider>(), toggle);
        //Physics.IgnoreCollision(controller, rightLeg.GetComponent<Collider>(), toggle);
        //Physics.IgnoreCollision(controller, spine.GetComponent<Collider>(), toggle);
        //Physics.IgnoreCollision(controller, leftArm.GetComponent<Collider>(), toggle);
        //Physics.IgnoreCollision(controller, leftForeArm.GetComponent<Collider>(), toggle);
        //Physics.IgnoreCollision(controller, leftHand.GetComponent<Collider>(), toggle);
        //Physics.IgnoreCollision(controller, rightArm.GetComponent<Collider>(), toggle);
        //Physics.IgnoreCollision(controller, rightForeArm.GetComponent<Collider>(), toggle);
        //Physics.IgnoreCollision(controller, rightHand.GetComponent<Collider>(), toggle);
        //Physics.IgnoreCollision(controller, head.GetComponent<Collider>(), toggle);


        //this does the same as above but can casue issues with gearboxes
        //hips.GetComponent<Collider>().enabled = toggle;
        //leftUpLeg.GetComponent<Collider>().enabled = toggle;
        //leftLeg.GetComponent<Collider>().enabled = toggle;
        //rightUpLeg.GetComponent<Collider>().enabled = toggle;
        //rightLeg.GetComponent<Collider>().enabled = toggle;
        //spine.GetComponent<Collider>().enabled = toggle;
        //leftArm.GetComponent<Collider>().enabled = toggle;
        //leftForeArm.GetComponent<Collider>().enabled = toggle;
        //leftHand.GetComponent<Collider>().enabled = toggle;
        //rightArm.GetComponent<Collider>().enabled = toggle;
        //rightForeArm.GetComponent<Collider>().enabled = toggle;
        //rightHand.GetComponent<Collider>().enabled = toggle;
        //head.GetComponent<Collider>().enabled = toggle;

    }

    public void PlayDeathSound()
    {
        deathGruntInt = Random.Range(0, 3); //this randomly pick what death grunt to play
        if (deathGruntInt == 4) //i dont want the wilhelm scream to play as often as the others and this keeps it rare
        {
            deathGruntInt = Random.Range(0, 3);
        }
        StepSource.PlayOneShot(deathSounds[deathGruntInt]);
    }
}
