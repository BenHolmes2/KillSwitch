using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grate : MonoBehaviour
{
    public GameObject grate;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "mixamorig:Hips1")
        {
            if (other.gameObject.GetComponent<RagdollScriptAnimated>().isElectrified)
            {
                grate.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
}
