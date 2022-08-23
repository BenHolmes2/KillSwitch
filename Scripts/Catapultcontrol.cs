using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapultcontrol : MonoBehaviour
{
    private Animator anim;
    public GameObject catapult;
    private int counter = 0;

    void Awake()
    {
        anim = catapult.GetComponent<Animator>();
        counter = 0;
    }

    void Update()
    {
        if (counter == 0)//PlayerIsOn == true
        {
            
                anim.speed = 1;
            
        }
        else
        {
            
                anim.speed = 0;
            
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        counter++;
    }

    //private void OnCollisionEmpty(Collision collision)

    private void OnTriggerExit(Collider collision)
    {
        counter--;
    }
}
