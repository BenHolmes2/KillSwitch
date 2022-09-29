using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FanControl : MonoBehaviour
{
    private Animator fanAnim;
    private Animator buttonAnim;


    public VisualEffect windfx;

    public GameObject button;
    public GameObject fan;
    public Fan cFan;

    private int counter = 0;


   void Awake()
    {
        windfx = windfx.GetComponent<VisualEffect>();
        fanAnim = fan.GetComponent<Animator>();
        buttonAnim = button.GetComponent<Animator>();
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
            fan.GetComponent<CapsuleCollider>().enabled = false;
            buttonAnim.SetBool("On", false);
            windfx.enabled = false;
        }
        else
        {
            fanAnim.SetBool("On", true);
            fan.GetComponent<CapsuleCollider>().enabled = true;
            buttonAnim.SetBool("On", true);
            windfx.enabled = true;
        }
                      
    }

    private void OnTriggerStay(Collider collision)
    {
        Debug.Log("haha");
        //if (collision.gameObject.tag == "canPickUp" || collision.gameObject.tag == "canPickUpDeath")
        //{
            counter = 1;
            Debug.Log("bruh");
        //}

            
    }

    private void OnTriggerExit(Collider collision)
    {
        //if (collision.gameObject.tag == "canPickUp" || collision.gameObject.tag == "canPickUpDeath")
        //{
            counter = 0;
            Debug.Log("bruh2");
        //}


    }
}
