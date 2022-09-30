using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMovement : MonoBehaviour, IDoor
{

    private bool isOpen = false;
    private Animator animator;
    public GameObject Door_L;
    public GameObject Door_R;
    private Renderer Light_L;
    private Renderer Light_R;
    public bool dnew=false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (dnew) 
        {
            Light_L = Door_L.GetComponent<Renderer>();
            Light_R = Door_R.GetComponent<Renderer>();
            Light_L.material.EnableKeyword("_EMISSION");
            Light_R.material.EnableKeyword("_EMISSION");
            Light_L.material.SetColor("_EmissionColor", Color.red);
            Light_R.material.SetColor("_EmissionColor", Color.red); 
        }
    }

    public void OpenDoor()
    {
        if (dnew) 
        {
            Light_L.material.EnableKeyword("_EMISSION");
            Light_R.material.EnableKeyword("_EMISSION");
            Light_L.material.SetColor("_EmissionColor", Color.green);
            Light_R.material.SetColor("_EmissionColor", Color.green);
        }
        animator.SetBool("Open", true);
    }

    public void CloseDoor()
    {   
        animator.SetBool("Open", false);
        if (dnew)
        {
            Light_L.material.EnableKeyword("_EMISSION");
            Light_R.material.EnableKeyword("_EMISSION");
            Light_L.material.SetColor("_EmissionColor", Color.red);
            Light_R.material.SetColor("_EmissionColor", Color.red);
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
