using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveSlide : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpForce = 10.0f;
    public float gravity = 20.0f;
    private Vector3 movementInput = Vector3.zero;
    private Vector3 movementDir = Vector3.zero;
    private Vector3 airMovementDir = Vector3.zero;
    //what is this for? is it needed?
    private Vector3 LatestRecordedMovementDir = Vector3.zero;

    //what are these for? are they needed?
    //private float velocityX = 0f;
    //private float velocityZ = 0f;

    CharacterController controller;
    public GameController1 gameController;
    private GameObject tempObj;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        tempObj = GameObject.Find("GameController");
        gameController = tempObj.GetComponent<GameController1>();
    }


    void Update()
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

            //these blocks are breaking the movement, what do they do/ are meant to do?
            //if (movementInput.magnitude != 0f)
            //{
            //    LatestRecordedMovementDir.x = airMovementDir.x;
            //    LatestRecordedMovementDir.z = airMovementDir.z;
            //}

            //if (movementInput.magnitude == 0f)
            //{
            //    airMovementDir.x = LatestRecordedMovementDir.x;
            //    airMovementDir.z = LatestRecordedMovementDir.z;
            //}

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

            //these blocks are breaking the movement, what do they do/are meant to do?
            //if (movementInput.magnitude != 0f)
            //{
            //    LatestRecordedMovementDir.x = movementDir.x;
            //    LatestRecordedMovementDir.z = movementDir.z;
            //}
            //if (movementInput.magnitude == 0f)
            //{

            //    movementDir.x = LatestRecordedMovementDir.x;
            //    movementDir.z = LatestRecordedMovementDir.z;
            //}
        }

        movementDir = transform.TransformDirection(movementDir);
        controller.Move(movementDir * Time.deltaTime);

        movementDir.y -= gravity * Time.deltaTime;
        movementDir.x -= gravity * Time.deltaTime;
        movementDir.z -= gravity * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LightningPoles")
        {
            gameController.StartCoroutine(gameController.respawnPlayer());
        }
    }
}