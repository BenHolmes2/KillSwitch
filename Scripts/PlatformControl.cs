using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControl : MonoBehaviour
{


    private Animator platformAnim;


    public GameObject platform;


    private int counter = 0;

   void Awake()
    {
        platformAnim = platform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (counter == 0)
        {
            platformAnim.SetBool("On", false);
        }
        else
        {
            platformAnim.SetBool("On", true);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("haha");
        if (collision.gameObject.tag == "canPickUp" || collision.gameObject.tag == "canPickUpDeath")
        {
            counter = 1;
            Debug.Log("bruh");
        }


    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "canPickUp" || collision.gameObject.tag == "canPickUpDeath")
        {
            counter = 0;
            Debug.Log("bruh2");
        }


    }

}
