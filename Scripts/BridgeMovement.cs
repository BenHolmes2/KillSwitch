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
            bridgeAnim.SetBool("On", true);
            gear1Anim.SetBool("On", true);
            gear2Anim.SetBool("On", true);
        }
        else
        {
            bridgeAnim.SetBool("On", false);
            gear1Anim.SetBool("On", false);
            gear2Anim.SetBool("On", false);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Hip_Root_JNT")
        {
            counter++;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name == "Hip_Root_JNT")
        {
            counter--;
        }
    }
}
