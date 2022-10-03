using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeMovement : MonoBehaviour
{
    private Animator bridgeAnim;
    private Animator gear1Anim;
    private Animator gear2Anim;

    public GameObject bridge;
    public GameObject gear1;
    public GameObject gear2;
    public GameObject deathCollider;

    private int counter = 0;



    void Awake()
    {
        bridgeAnim = bridge.GetComponent<Animator>();
        gear1Anim = gear1.GetComponent<Animator>();
        gear2Anim = gear2.GetComponent<Animator>();
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (counter == 0 )
        {
            gear1Anim.SetBool("On", true);
            gear2Anim.SetBool("On", true);
            bridgeAnim.SetBool("On", true);
            deathCollider.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            gear1Anim.SetBool("On", false);
            gear2Anim.SetBool("On", false);
            bridgeAnim.SetBool("On", false);
            deathCollider.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "canPickUp" || collision.gameObject.tag == "canPickUpDeath")
        {
            counter = 1;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "canPickUp" || collision.gameObject.tag == "canPickUpDeath")
        {
            counter = 0;
        }
    }
}
