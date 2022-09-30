using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMovement : MonoBehaviour, IDoor
{

    private bool isOpen = false;
    private Animator animator;
    public GameObject Door_L;
    public GameObject Door_R;
    private Material Light_L;
    private Material Light_R;
    public bool dnew=false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (dnew) 
        {
            Light_L = Door_L.GetComponent<Material>();
            Light_R = Door_R.GetComponent<Material>();
            Light_L.SetColor("_Emission", Color.red);
            Light_R.SetColor("_Emission", Color.red); 
        }
    }

    public void OpenDoor()
    {
        if (dnew) 
        {
            Light_L.SetColor("_Emission", Color.green);
            Light_R.SetColor("_Emission", Color.green);
        }
        animator.SetBool("Open", true);
    }

    public void CloseDoor()
    {   
        animator.SetBool("Open", false);
        if (dnew)
        {
            Light_L.SetColor("_Emission", Color.red);
            Light_R.SetColor("_Emission", Color.red);
        }
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
