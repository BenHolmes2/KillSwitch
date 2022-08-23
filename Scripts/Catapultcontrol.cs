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
        }
        else
        {            
            animCata.SetBool("On", false);
            animG1.SetBool("On", false);
            animG2.SetBool("On", false);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "QuickRigCharacter_Hips")
        {
            counter++;
        }
    }

    //private void OnCollisionEmpty(Collision collision)

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name == "QuickRigCharacter_Hips")
        {
            counter--;
        }
    }
}
