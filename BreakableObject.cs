using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public AudioClip brokenSpeaker;
    public AudioSource speaker;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("canPickUpObject") || other.gameObject.CompareTag("canPickUpDeath") || other.gameObject.CompareTag("canPickUp"))
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            gameObject.tag = "canPickUpObject";


            if (speaker != null)
            {
                if (brokenSpeaker != null)
                {
                    speaker.Stop();
                    speaker.PlayOneShot(brokenSpeaker);
                }
            }
        }
    }
}
