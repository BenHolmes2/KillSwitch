using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMovement : MonoBehaviour, IDoor
{

    private bool isOpen = false;
    private Animator animator;
    private Material l;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        //l.SetColor("_Emission", Color.red);
    }

    public void OpenDoor()
    {
        animator.SetBool("Open", true);
    }

    public void CloseDoor()
    {
        animator.SetBool("Open", false);
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }
}
