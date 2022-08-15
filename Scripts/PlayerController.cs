using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public Transform holdPos;
    public float throwForce = 500f;
    public float pickUpRange = 20f;

    public GameObject heldObj;
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
    private Vector3 movementDir = Vector3.zero;
    private Vector3 airMovementDir = Vector3.zero;

    public GameController gameController;
    private GameObject tempObj;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        tempObj = GameObject.Find("GameController");
        gameController = tempObj.GetComponent<GameController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerLook();
        PlayerMove();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObj == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(cameraObj.transform.position, cameraObj.transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    Debug.DrawLine(cameraObj.transform.position, hit.point, Color.white, 5f);
                    Debug.Log(hit.transform.gameObject.tag);
                    Debug.Log(hit.transform.gameObject.name);
                    if (hit.transform.gameObject.tag == "canPickUp" || hit.transform.gameObject.tag == "canPickUpDeath")
                    {
                        PickUpBody(hit.transform.gameObject);
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
            heldObj = heldObj.transform.Find("riggedd body 04").gameObject;
            heldObj = heldObj.transform.Find("QuickRigCharacter_Reference").gameObject;
            heldObj = heldObj.transform.Find("QuickRigCharacter_Hips").gameObject;

            //finding all of the colliders so we can ignore collsions from them
            //move this to a function
            //dont think these do anything
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
            heldObjRb = heldObj.GetComponent<Rigidbody>();
            heldObjRb.transform.position = holdPos.transform.position;

            ToggleCollisions(true);
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

        movementDir.y -= gravity * Time.deltaTime;
        movementDir.x -= gravity * Time.deltaTime;
        movementDir.z -= gravity * Time.deltaTime;
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
    void DropObject()
    {
        ToggleCollisions(false);
        heldObj = null;
    }
    void MoveObject()
    {
        heldObj.transform.position = holdPos.transform.position;
    }

    void ThrowObject()
    {
        ToggleCollisions(false);
        //Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        //heldObj.transform.parent = null;
        heldObj = null;
        //heldObjRb.AddForce(transform.right * throwForce);
        heldObjRb.velocity = (transform.forward * throwForce);
    }

    void ToggleCollisions(bool toggle)
    {
        Physics.IgnoreCollision(controller, hips.GetComponent<Collider>(), toggle);
        Physics.IgnoreCollision(controller, leftUpLeg.GetComponent<Collider>(), toggle);
        Physics.IgnoreCollision(controller, leftLeg.GetComponent<Collider>(), toggle);
        Physics.IgnoreCollision(controller, rightUpLeg.GetComponent<Collider>(), toggle);
        Physics.IgnoreCollision(controller, rightLeg.GetComponent<Collider>(), toggle);
        Physics.IgnoreCollision(controller, spine.GetComponent<Collider>(), toggle);
        Physics.IgnoreCollision(controller, leftArm.GetComponent<Collider>(), toggle);
        Physics.IgnoreCollision(controller, leftForeArm.GetComponent<Collider>(), toggle);
        Physics.IgnoreCollision(controller, leftHand.GetComponent<Collider>(), toggle);
        Physics.IgnoreCollision(controller, rightArm.GetComponent<Collider>(), toggle);
        Physics.IgnoreCollision(controller, rightForeArm.GetComponent<Collider>(), toggle);
        Physics.IgnoreCollision(controller, rightHand.GetComponent<Collider>(), toggle);
        Physics.IgnoreCollision(controller, head.GetComponent<Collider>(), toggle);
    }
}