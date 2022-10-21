using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepRagdollOn : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<RagdollScriptAnimated>() != null)
        {
            if (other.gameObject.GetComponent<RagdollScriptAnimated>().turningOff == true)
            {
                //other.gameObject.GetComponent<RagdollScriptAnimated>().TurnOnRagdoll();
                other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.GetComponent<RagdollScriptAnimated>() != null)
    //    {
    //        if (other.gameObject.GetComponent<RagdollScriptAnimated>().turningOff == true)
    //        {
    //            //other.gameObject.GetComponent<RagdollScriptAnimated>().TurnOffRagdoll();
    //            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    //        }
    //    }
    //}
}
