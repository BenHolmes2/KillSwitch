using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FanControl : MonoBehaviour
{
    private Animator fanAnim;
    private Animator buttonAnim;


    public VisualEffect windFX;
    public GameObject trailsFX;
    public ParticleSystem dotsFX;

    public GameObject button;
    public GameObject fan;
    public CapsuleCollider fanCollider;
    public Fan cFan;

    public GameObject wireTexture;
    private Renderer wireRenderer;

    private int counter = 0;


   void Awake()
    {
        windFX = windFX.GetComponent<VisualEffect>();
        //trailsFX = trailsFX.GetComponent<ParticleSystem>();
        dotsFX = dotsFX.GetComponent<ParticleSystem>();
        fanAnim = fan.GetComponent<Animator>();
        buttonAnim = button.GetComponent<Animator>();
        counter = 0;
        wireRenderer = wireTexture.GetComponent<Renderer>();
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
            fanAnim.SetBool("On", false);
            //fan.GetComponent<CapsuleCollider>().enabled = false;
            fanCollider.enabled = false;
            buttonAnim.SetBool("On", false);
            windFX.enabled = false;
            trailsFX.SetActive(false);
            dotsFX.Stop();
            wireRenderer.material.SetFloat("_EmissionForHoles", -1);
        }
        else
        {
            fanAnim.SetBool("On", true);
            //fan.GetComponent<CapsuleCollider>().enabled = true;
            fanCollider.enabled = true;
            buttonAnim.SetBool("On", true);
            windFX.enabled = true;
            trailsFX.SetActive(true);
            dotsFX.Play();
            wireRenderer.material.SetFloat("_EmissionForHoles", 1);
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
