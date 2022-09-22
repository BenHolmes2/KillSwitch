using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanControl2 : MonoBehaviour
{
    private Animator fanAnim;

    public GameObject fan;
    public Fan cFan;

    private int counter = 0;


    void Awake()
    {
        cFan.fanForceBody = 0;
        fanAnim = fan.GetComponent<Animator>();
        counter = 0;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (counter == 0)
        {
            fanAnim.SetBool("On", false);
            cFan.fanForceBody = 0;
        }
        else
        {
            fanAnim.SetBool("On", true);
            cFan.fanForceBody = 20;
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
