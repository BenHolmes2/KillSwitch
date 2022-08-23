using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : MonoBehaviour
{
    public GameObject catapult;
    public GameObject gears;
    private int counter = 0;

    void Awake()
    {
        
        counter = 0;
    }

    void Update()
    {
        if (counter == 0)//PlayerIsOn == true
        {
            
        }
        else
        {
            
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
