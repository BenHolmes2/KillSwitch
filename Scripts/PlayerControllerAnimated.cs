using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class PlayerControllerAnimated : MonoBehaviour
{
    public GameObject player;
    public Transform holdPos;
    public Transform holdPosObject;
    public float throwForce = 5f;
    public float throwForceObject = 15f;
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
    public GameObject menu;
    public PauseV2 menuScript;
    public InputSystemUIInputModule menuInput;

    public float mouseSensitivity = 100f;

    public Transform playerBody;
    public GameObject cameraObj;

    float xRotation = 0f;

    public float speed = 10.0f;
    public float speedTemp = 10.0f;
    public float jumpForce = 10.0f;
    public float gravity = 20.0f;
    private Vector3 movementInput = Vector3.zero;
    public Vector3 movementDir = Vector3.zero;
    private Vector3 airMovementDir = Vector3.zero;

    public GameControllerAnimated gameController;
    private GameObject tempObj;

    private RaycastHit pickUpHit;
    private RaycastHit cursorHit;

    public AudioSource StepSource;

    public AudioSource deathSource;
    private AudioClip[] deathSounds = new AudioClip[4];
    private int deathGruntInt;

    private double startTime;
    private double tempTime;
    public float jumpGracePeriod = 0.2f;
    public float rampUpModifier = 1.1f;
    public int rampUpOffset = 6;
    public GameObject animatorObj;
    private Animator bodyController;

    private float mouseInputX;
    private float mouseInputY;
    private PlayerInput playerInput;
    private bool finishedDropping = true;
    private bool noDoubleJump = true;



    void Start()
    {
        bodyController = animatorObj.GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        tempObj = GameObject.Find("GameController");
        gameController = tempObj.GetComponent<GameControllerAnimated>();
        menu = GameObject.Find("PauseMenuV2");
        menuScript = menu.GetComponent<PauseV2>();
        Cursor.lockState = CursorLockMode.Locked;
        menu = menu.gameObject.transform.Find("EventSystem").gameObject;
        menuInput = menu.GetComponent<InputSystemUIInputModule>();
        GetComponent<PlayerInput>().uiInputModule = menuInput;
        playerInput = gameObject.GetComponent<PlayerInput>();


        StepSource = this.gameObject.GetComponent<AudioSource>();

        deathSounds[0] = Resources.Load("DeathGrunt1") as AudioClip;
        deathSounds[1] = Resources.Load("DeathGrunt2") as AudioClip;
        deathSounds[2] = Resources.Load("DeathGrunt3") as AudioClip;
        deathSounds[3] = Resources.Load("DeathWilhelm") as AudioClip;

        speedTemp = speed;
        speedTemp -= rampUpOffset;
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(controller.velocity);
        //Debug.Log(controller.isGrounded);

        PlayerLook();
        PlayerMove();

        //Hoping this will fix weird edge case where if the ragdoll is destoryed while being held, the next time the player respawns they fall through the floor
        if (heldObj == null && finishedDropping)
        {
            ToggleCollisions(false);
        }


        if (Physics.Raycast(cameraObj.transform.position, cameraObj.transform.TransformDirection(Vector3.forward), out cursorHit, pickUpRange))
        {
            //Debug.DrawLine(cameraObj.transform.position, cursorHit.point, Color.white, 5f);

            if ((cursorHit.transform.gameObject.CompareTag("canPickUpObject") || cursorHit.transform.gameObject.CompareTag("canPickUpDeath") || cursorHit.transform.gameObject.CompareTag("canPickUp")) && heldObj == null)
            {
                //Debug.Log(cursorHit.transform.gameObject.name);
                gameController.reticleCanvas.SetActive(false);
                gameController.pickUpCanvas.SetActive(true);

                if (playerInput.currentControlScheme == "Controller")
                {
                    gameController.pickUpText.text = "X";
                }
                else
                {
                    gameController.pickUpText.text = "E";
                }
            }
            else
            {
                gameController.reticleCanvas.SetActive(true);
                gameController.pickUpCanvas.SetActive(false);
            }
        }
        else
        {
            gameController.reticleCanvas.SetActive(true);
            gameController.pickUpCanvas.SetActive(false);
        }



        if (heldObj != null)
        {
            MoveObject();
            //if (Input.GetKeyDown(KeyCode.Mouse0) && canThrow == true)
            //{
            //    ThrowObject();
            //}
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("canPickUpObject"))
    //    {
    //        other.gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 10);
    //        Debug.Log("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
    //    }
    //}

    private void OnRespawn()
    {
        gameController.startRespawn = true;
    }

    private void OnPause()
    {
        if (!menuScript.startPause)
        {
            menuScript.startPause = true;

        }
        else
        {
            menuScript.startPause = false;
        }
    }

    private void OnBack()
    {
        if (menuScript.paused)
        {
            menuScript.Back();
        }
    }

    private void OnPickUp()
    {

            if (heldObj == null)
            {
                if (Physics.Raycast(cameraObj.transform.position, cameraObj.transform.TransformDirection(Vector3.forward), out pickUpHit, pickUpRange))
                {
                    //Debug.DrawLine(cameraObj.transform.position, pickUpHit.point, Color.white, 5f);
                    //Debug.Log(pickUpHit.transform.gameObject.tag);
                    //Debug.Log(pickUpHit.transform.gameObject.name);
                    if (pickUpHit.transform.gameObject.CompareTag("canPickUpDeath") || pickUpHit.transform.gameObject.CompareTag("canPickUp"))
                    {
                        PickUpBody(pickUpHit.transform.gameObject);
                    }
                    //currently not needed, reimplement if we want to be able to pick up objects again
                    if (pickUpHit.transform.gameObject.CompareTag("canPickUpObject"))
                    {
                        PickUpObject(pickUpHit.transform.gameObject);
                    }
                }
            }
            else
            {
                DropObject();
            }
    }

    private void PickUpBody(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>())
        {
            heldObj = pickUpObj;
            heldObj = heldObj.transform.root.gameObject;
            //finding all of the colliders so we can ignore collsions from them
            //move this to a function
            hips = heldObj.transform.Find("parent").gameObject;
            hips = hips.transform.Find("mixamorig:Hips1").gameObject;
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


            heldObj = hips;
            heldObjRb = heldObj.GetComponent<Rigidbody>();
            //heldObjRb.transform.position = holdPos.transform.position;
            //Debug.Log("----------------------------------------");
            //Debug.Log(heldObj);
            //Debug.Log(heldObjRb);
            //Debug.Log("----------------------------------------");
            heldObjRb.transform.position = holdPos.transform.position;
            heldObjRb.transform.rotation = holdPos.transform.rotation;

            //heldObj.gameObject.transform.root.gameObject.layer = 6;

            heldObj.GetComponent<RagdollScriptAnimated>().TurnOnRagdoll();
            ToggleLayer(6);
            ToggleCollisions(true);
        }
    }

    private void PlayerMove()
    {
        //movementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        //Debug.Log(Input.GetAxisRaw("Horizontal"));
        //Debug.Log(Input.GetAxisRaw("Vertical"));

        //this applies a ramp up to the speed as the player holds down a move button
        //if ((Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0) && speedTemp <= speed)
        //{
        //    speedTemp *= rampUpModifier;
        //}
        //else if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0) 
        //{
        //    speedTemp = speed;
        //    speedTemp -= rampUpOffset; 
        //}

        if ((movementInput.z != 0 || movementInput.x != 0) && speedTemp <= speed)
        {
            speedTemp *= rampUpModifier;
        }
        else if (movementInput.z == 0 && movementInput.x == 0)
        {
            speedTemp = speed;
            speedTemp -= rampUpOffset;
        }


        if (movementInput.magnitude > 1.0f)
        {
            movementInput = movementInput.normalized;
        }

        // When player is in the air
        if (!controller.isGrounded)
        {
            airMovementDir = new Vector3(movementInput.x, movementInput.y, movementInput.z);

            startTime = Time.timeAsDouble;
            //if (startTime < tempTime)
            //{
            //    //if (Input.GetButton("Jump"))
            //    //{
            //    //    bodyController.SetBool("Jumping", true);
            //    //    movementDir.y = jumpForce;
            //    //    //set start time to temp time here
            //    //    tempTime = startTime;
            //    //}
            //}

            airMovementDir.x *= speedTemp;
            airMovementDir.z *= speedTemp;

            movementDir.x = airMovementDir.x;
            movementDir.z = airMovementDir.z;

        }


        if (controller.isGrounded)
        {
            movementDir = new Vector3(movementInput.x, movementInput.y, movementInput.z);

            //if (Input.GetButton("Jump"))
            //{
            //    bodyController.SetBool("Jumping", true);
            //    movementDir.y = jumpForce;
            //}

            movementDir.x *= speedTemp;
            movementDir.z *= speedTemp;
            startTime = Time.timeAsDouble;
            tempTime = startTime + jumpGracePeriod;
        }

        movementDir = transform.TransformDirection(movementDir);
        controller.Move(movementDir * Time.deltaTime);


        if (controller.velocity.magnitude > 2f && StepSource.isPlaying == false && controller.isGrounded)
        {
            StepSource.Play();

            //float time = Time.deltaTime;
        }

        if (controller.velocity.magnitude > 1f && controller.isGrounded)
        {
            bodyController.SetBool("Walking", true);
        }
        else if (controller.velocity.magnitude < 1f && !controller.isGrounded)
        {
            bodyController.SetBool("Walking", false);

        }

        if (controller.isGrounded)
        {
            bodyController.SetBool("Jumping", false);
        }
        //else
        //{

        //}

        movementDir.y -= gravity * Time.deltaTime;
        movementDir.x -= gravity * Time.deltaTime;
        movementDir.z -= gravity * Time.deltaTime;
        //Debug.Log(movementDir.y + "?????????????");
        //Debug.Log(movementDir);

    }

    private void OnMove(InputValue value)
    {
        movementInput = new Vector3(value.Get<Vector2>().x, movementInput.y, value.Get<Vector2>().y);
        //Debug.Log(value.Get<Vector2>());
    }

    private void PlayerLook()
    {
        //mouseInputX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        //mouseInputY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseInputY;
        xRotation = Mathf.Clamp(xRotation, -83f, 90f);


        cameraObj.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseInputX);
    }

    private void OnLook(InputValue value)
    {
        mouseInputX = value.Get<Vector2>().x * mouseSensitivity * Time.deltaTime;
        mouseInputY = value.Get<Vector2>().y * mouseSensitivity * Time.deltaTime;
    }

    private void OnJump()
    {
        //Debug.Log("SPACE PRESSED");
        if (!controller.isGrounded)
        {

            if (startTime < tempTime)
            {
                bodyController.SetBool("Jumping", true);
                movementDir.y = jumpForce;
                //set start time to temp time here
                tempTime = startTime;
            }
        }
        else 
        {
            bodyController.SetBool("Jumping", true);
            //Debug.Log("WHYYYYYYYYYYY");
            movementDir.y = jumpForce;
        }
        movementDir = transform.TransformDirection(movementDir);
        controller.Move(movementDir * Time.deltaTime);
    }

    private void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>())
        {
            heldObj = pickUpObj;
            //Debug.Log(heldObj);
            heldObjRb = heldObj.GetComponent<Rigidbody>();
            heldObjRb.isKinematic = false;
            ToggleLayer(6);
            ToggleCollisions(true);
            heldObjRb.transform.position = holdPosObject.transform.position;

        }

    }
    public void DropObject()
    {
        if (heldObj != null)
        {
            if (heldObj.GetComponent<RagdollScriptAnimated>() == true) //make this a better check
            {

                //Physics.IgnoreLayerCollision(6, 0, false);
                //Invoke("Physics.IgnoreLayerCollision(6, 3, false)", 0.5f);
                //Invoke("ToggleLayer(0)", 0.5f);

                StartCoroutine(ToggleCollisionsDrop(false));
                StartCoroutine(ToggleLayerDrop(0));
                finishedDropping = false;


                heldObj.GetComponent<RagdollScriptAnimated>().TurnOnRagdoll();
                heldObj = null;
            }
            else
            {
                ToggleLayer(0);
                ToggleCollisions(false);
                heldObj = null;
            }
        }
    }
    private void MoveObject()
    {
        if (heldObj.GetComponent<RagdollScriptAnimated>() == true) //make this a better check
        {
            ToggleLayer(6);
            ToggleCollisions(true);
            heldObj.GetComponent<RagdollScriptAnimated>().TurnOnRagdoll();
            heldObjRb.transform.position = holdPos.transform.position;
            heldObjRb.transform.rotation = holdPos.transform.rotation;
            heldObj.GetComponent<RagdollScriptAnimated>().ClampVelocity();
        }
        else
        {
            heldObjRb.transform.position = holdPosObject.transform.position;

            if (heldObjRb.velocity.magnitude > 5)
            {
                heldObjRb.GetComponent<Rigidbody>().velocity = new Vector3(0, -5, 0);
            }
           // heldObjRb.transform.rotation = holdPos.transform.rotation;
        }

        //heldObj.transform.position = holdPos.transform.position;
        //heldObj.transform.rotation = holdPos.transform.rotation;


    }

    private void ThrowObject()
    {
        
        if (heldObj.GetComponent<RagdollScriptAnimated>() == true) //make this a better check
        {
            heldObj.GetComponent<RagdollScriptAnimated>().TurnOnRagdoll();
            StartCoroutine(ToggleCollisionsDrop(false));
            StartCoroutine(ToggleLayerDrop(0));
            finishedDropping = false;
            //ToggleLayer(0);
            //ToggleCollisions(false);
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
        else if (heldObj.CompareTag("canPickUpObject"))
        {
            ToggleLayer(0);
            ToggleCollisions(false);
            heldObj.GetComponent<Rigidbody>().velocity = (cameraObj.transform.forward * throwForceObject);
            heldObj = null;
        }
    }

    private void OnThrow()
    {
        if (heldObj != null)
        {
            if (canThrow == true)
            {
                ThrowObject();
            }
        }
    }

    //The two functions below change the collision layer on the ragdoll and and disable collisions between layers.
    public void ToggleCollisions(bool toggle)
    {
        //this ignores collisions between the body and the envrionment but not the gearbox
        Physics.IgnoreLayerCollision(6, 0, toggle);
        Physics.IgnoreLayerCollision(6, 3, toggle);
    }

    private void ToggleLayer(int toggle)
    {
        if (heldObj.GetComponent<RagdollScriptAnimated>() == true) //make this a better check
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
        }
        else
        {
            heldObj.layer = toggle; 
        }

    }

    //The two functions below change the collision layer on the ragdoll and and re-enable collisions between layers.
    //These functions are set as IEnumerators so they can be called on a delay, this is to stop the body from colliding
    //with the player when being dropped.
    private IEnumerator ToggleLayerDrop(int toggle)
    {
        if (heldObj.GetComponent<RagdollScriptAnimated>() == true) //make this a better check
        {
            yield return new WaitForSeconds(0.5f);
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
        }
        else
        {
            heldObj.layer = toggle;
        }

    }

    public IEnumerator ToggleCollisionsDrop(bool toggle)
    {
        Physics.IgnoreLayerCollision(6, 0, toggle);
        yield return new WaitForSeconds(0.5f);
        Physics.IgnoreLayerCollision(6, 3, toggle);
        finishedDropping = true;
    }

    public void PlayDeathSound()
    {
        deathGruntInt = Random.Range(0, 4); //this randomly pick what death grunt to play
        if (deathGruntInt == 3) //i dont want the wilhelm scream to play as often as the others and this keeps it rare
        {
            if (deathGruntInt == 3)
            {
                if (deathGruntInt == 3)
                {
                    if (deathGruntInt == 3)
                    {
                        deathGruntInt = Random.Range(0, 4);
                    }
                }
            }
        }
        StepSource.PlayOneShot(deathSounds[deathGruntInt]);
    }


    private void OnDebug()
    {
        menuScript.DebugMenu();
    }
}
