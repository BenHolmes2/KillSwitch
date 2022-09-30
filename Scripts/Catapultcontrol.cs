using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapultcontrol : MonoBehaviour
{
    private Animator animCata;
    private Animator animG1;
    private Animator animG2;
    public GameObject catapult;
    public GameObject gear1;
    public GameObject gear2;
    private int counter = 0;

    void Awake()
    {
        animCata = catapult.GetComponent<Animator>();
        animG1 = gear1.GetComponent<Animator>();
        animG2 = gear2.GetComponent<Animator>();
        counter = 0;
    }

    void Update()
    {
        if (counter == 0)//PlayerIsOn == true
        {
            animCata.SetBool("On", true);
            animG1.SetBool("On", true);
            animG2.SetBool("On", true);
            //gameObject.GetComponent<DeathCollider>().enabled = true;
        }
        else
        {            
            animCata.SetBool("On", false);
            animG1.SetBool("On", false);
            animG2.SetBool("On", false);
            //gameObject.GetComponent<DeathCollider>().enabled = false;
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
