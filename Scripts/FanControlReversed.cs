using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FanControlReversed : MonoBehaviour
{
    private Animator fanAnim;
    private Animator buttonAnim;


    public VisualEffect windFX;
    public ParticleSystem trailsFX;
    public ParticleSystem dotsFX;

    public GameObject button;
    public GameObject fan;
    public CapsuleCollider fanCollider;
    public Fan cFan;

    private int counter = 0;


   void Awake()
    {
        windFX = windFX.GetComponent<VisualEffect>();
        trailsFX = trailsFX.GetComponent<ParticleSystem>();
        dotsFX = dotsFX.GetComponent<ParticleSystem>();
        fanAnim = fan.GetComponent<Animator>();
        buttonAnim = button.GetComponent<Animator>();
        counter = 0;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        fanCollider = fan.GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (counter == 0)
        {
            fanAnim.SetBool("On", true);
            //fan.GetComponent<CapsuleCollider>().enabled = true;
            fanCollider.enabled = true;
            buttonAnim.SetBool("On", false);
            windFX.enabled = true;
            trailsFX.Play();
            dotsFX.Play();
        }
        else
        {
            fanAnim.SetBool("On", false);
            //fan.GetComponent<CapsuleCollider>().enabled = false;
            fanCollider.enabled = false;
            buttonAnim.SetBool("On", true);
            windFX.enabled = false;
            trailsFX.Stop();
            dotsFX.Stop();
        }
                      
    }

    private void OnTriggerStay(Collider collision)
    {
        //Debug.Log("haha");
        //if (collision.gameObject.tag == "canPickUp" || collision.gameObject.tag == "canPickUpDeath")
        //{
            counter = 1;
            //Debug.Log("bruh");
        //}

            
    }

    private void OnTriggerExit(Collider collision)
    {
        //if (collision.gameObject.tag == "canPickUp" || collision.gameObject.tag == "canPickUpDeath")
        //{
            counter = 0;
            //Debug.Log("bruh2");
        //}


    }
}
