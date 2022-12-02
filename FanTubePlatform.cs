using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanTubePlatform : MonoBehaviour
{
    public Animator platform;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.name == "mixamorig:Hips1")
        {
            if (collision.gameObject.GetComponent<RagdollScriptAnimated>().isElectrified)
            {
                platform.SetBool("On", true);
            }
        }

    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name == "mixamorig:Hips1")
        {
            if (collision.gameObject.GetComponent<RagdollScriptAnimated>().isElectrified)
            {
                platform.SetBool("On", false);
            }
        }
    }
}