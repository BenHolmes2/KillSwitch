using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    public GameObject doorGameObjectR;
    public GameObject doorGameObjectL;
    public GameObject buttonGameObject;
    private IDoor doorR;
    private IDoor doorL;
    private IDoor button;

    void Awake()
    {
        doorR = doorGameObjectR.GetComponent<IDoor>();
        doorL = doorGameObjectL.GetComponent<IDoor>();
        button = buttonGameObject.GetComponent<IDoor>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))//PlayerIsOn == true
        {
            doorR.ToggleDoor();
            doorL.ToggleDoor();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if (collider.GetComponent<CharacterController>() != null)
        // {
            doorR.ToggleDoor();
            doorL.ToggleDoor();
            button.ToggleDoor();
        // }
    }

    //private void OnCollisionEmpty(Collision collision)

    private void OnCollisionExit(Collision collision)
    {
        // if (collider.GetComponent<CharacterController>() != null)
        // {
            doorR.ToggleDoor();
            doorL.ToggleDoor();
            button.ToggleDoor();
        // }
    }
}
