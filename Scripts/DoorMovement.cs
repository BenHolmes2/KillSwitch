using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoorMovement : MonoBehaviour, IDoor
{

    private bool isOpen = false;
    private Animator animator;
    //public GameObject Door_L;
    //public GameObject Door_R;
    //private Renderer Light_L;
    //private Renderer Light_R;
    public bool dnew = false;
    //[ColorUsage(true, true)]
    //private Color red = Color.red;
    //[ColorUsage(true, true)]
    //private Color green = Color.green;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (dnew)
        {
            //Light_L = Door_L.GetComponent<Renderer>();
            //Light_R = Door_R.GetComponent<Renderer>();
            //Light_L.material.EnableKeyword("_EmissionColor");
            //Light_R.material.EnableKeyword("_EmissionColor");
            //Light_L.material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
            //Light_R.material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
            //Light_L.material.SetColor("_EmissionColor", red);
            //Light_R.material.SetColor("_EmissionColor", red);
            //RendererExtensions.UpdateGIMaterials(Light_L);
            //RendererExtensions.UpdateGIMaterials(Light_R);
            //DynamicGI.SetEmissive(Light_L, Color.red * 100000);
            //DynamicGI.SetEmissive(Light_R, Color.red * 100000);
            //DynamicGI.UpdateEnvironment();
        }
    }

    public void OpenDoor()
    {
        if (dnew)
        {
            //Light_L.material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
            //Light_R.material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
            //Light_L.material.SetColor("_EmissionColor", green);
            //Light_R.material.SetColor("_EmissionColor", green);
            //RendererExtensions.UpdateGIMaterials(Light_L);
            //RendererExtensions.UpdateGIMaterials(Light_R);
            //DynamicGI.SetEmissive(Light_L, Color.green * 100000);
            //DynamicGI.SetEmissive(Light_R, Color.green * 100000);
            //DynamicGI.UpdateEnvironment();
        }
        animator.SetBool("Open", true);
    }

    public void CloseDoor()
    {
        animator.SetBool("Open", false);
        if (dnew)
        {
            //Light_L.material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
            //Light_R.material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
            //Light_L.material.SetColor("_EmissionColor", red);
            //Light_R.material.SetColor("_EmissionColor", red);
            //RendererExtensions.UpdateGIMaterials(Light_L);
            //RendererExtensions.UpdateGIMaterials(Light_R);
            //DynamicGI.SetEmissive(Light_L, Color.red * 100000);
            //DynamicGI.SetEmissive(Light_R, Color.red * 100000);
            //DynamicGI.UpdateEnvironment();
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
