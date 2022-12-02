using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FanControlTubeFan : MonoBehaviour
{
    private Animator buttonAnim;


    public VisualEffect windFX1;
    public GameObject trailsFX1;
    public ParticleSystem dotsFX1;
    public GameObject fan1;
    private Animator fanAnim1;
    private CapsuleCollider fanCollider1;

    public VisualEffect windFX2;
    public GameObject trailsFX2;
    public ParticleSystem dotsFX2;
    public GameObject fan2;
    private Animator fanAnim2;
    private CapsuleCollider fanCollider2;

    public VisualEffect windFX3;
    public GameObject trailsFX3;
    public ParticleSystem dotsFX3;
    public GameObject fan3;
    private Animator fanAnim3;
    private CapsuleCollider fanCollider3;

    public VisualEffect windFX4;
    public GameObject trailsFX4;
    public ParticleSystem dotsFX4;
    public GameObject fan4;
    private Animator fanAnim4;
    private CapsuleCollider fanCollider4;

    public VisualEffect windFX5;
    public GameObject trailsFX5;
    public ParticleSystem dotsFX5;
    public GameObject fan5;
    private Animator fanAnim5;
    private CapsuleCollider fanCollider5;


    public GameObject button;

    //public GameObject wireTexture;
    //private Renderer wireRenderer;

    private int counter = 0;


   void Awake()
    {
        windFX1 = windFX1.GetComponent<VisualEffect>();
        //trailsFX = trailsFX.GetComponent<ParticleSystem>();
        dotsFX1 = dotsFX1.GetComponent<ParticleSystem>();
        fanAnim1 = fan1.GetComponent<Animator>();

        windFX2 = windFX2.GetComponent<VisualEffect>();
        //trailsFX = trailsFX.GetComponent<ParticleSystem>();
        dotsFX2 = dotsFX2.GetComponent<ParticleSystem>();
        fanAnim2 = fan2.GetComponent<Animator>();

        windFX3 = windFX3.GetComponent<VisualEffect>();
        //trailsFX = trailsFX.GetComponent<ParticleSystem>();
        dotsFX3 = dotsFX3.GetComponent<ParticleSystem>();
        fanAnim3 = fan3.GetComponent<Animator>();

        windFX4 = windFX4.GetComponent<VisualEffect>();
        //trailsFX = trailsFX.GetComponent<ParticleSystem>();
        dotsFX4 = dotsFX4.GetComponent<ParticleSystem>();
        fanAnim4 = fan4.GetComponent<Animator>();

        windFX5 = windFX5.GetComponent<VisualEffect>();
        //trailsFX = trailsFX.GetComponent<ParticleSystem>();
        dotsFX5 = dotsFX5.GetComponent<ParticleSystem>();
        fanAnim5 = fan5.GetComponent<Animator>();


        buttonAnim = button.GetComponent<Animator>();
        counter = 0;
        //wireRenderer = wireTexture.GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        fanCollider1 = fan1.GetComponent<CapsuleCollider>();
        fanCollider2 = fan2.GetComponent<CapsuleCollider>();
        fanCollider3 = fan3.GetComponent<CapsuleCollider>();
        fanCollider4 = fan4.GetComponent<CapsuleCollider>();
        fanCollider5 = fan5.GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (counter == 0)
        {
            fanAnim1.SetBool("On", false);
            fanCollider1.enabled = false;
            buttonAnim.SetBool("On", false);
            windFX1.enabled = false;
            trailsFX1.SetActive(false);
            dotsFX1.Stop();

            fanAnim2.SetBool("On", false);
            fanCollider2.enabled = false;
            windFX2.enabled = false;
            trailsFX2.SetActive(false);
            dotsFX2.Stop();

            fanAnim3.SetBool("On", false);
            fanCollider3.enabled = false;
            windFX3.enabled = false;
            trailsFX3.SetActive(false);
            dotsFX3.Stop();

            fanAnim4.SetBool("On", false);
            fanCollider4.enabled = false;
            windFX4.enabled = false;
            trailsFX4.SetActive(false);
            dotsFX4.Stop();

            fanAnim5.SetBool("On", false);
            fanCollider5.enabled = false;
            windFX5.enabled = false;
            trailsFX5.SetActive(false);
            dotsFX5.Stop();

            //wireRenderer.material.SetFloat("_EmissionForHoles", -1);
        }
        else
        {
            fanAnim1.SetBool("On", true);
            fanCollider1.enabled = true;
            buttonAnim.SetBool("On", true);
            windFX1.enabled = true;
            trailsFX1.SetActive(true);
            dotsFX1.Play();

            fanAnim2.SetBool("On", true);
            fanCollider2.enabled = true;
            windFX2.enabled = true;
            trailsFX2.SetActive(true);
            dotsFX2.Play();

            fanAnim3.SetBool("On", true);
            fanCollider3.enabled = true;
            windFX3.enabled = true;
            trailsFX3.SetActive(true);
            dotsFX3.Play();

            fanAnim4.SetBool("On", true);
            fanCollider4.enabled = true;
            windFX4.enabled = true;
            trailsFX4.SetActive(true);
            dotsFX4.Play();

            fanAnim5.SetBool("On", true);
            fanCollider5.enabled = true;
            windFX5.enabled = true;
            trailsFX5.SetActive(true);
            dotsFX5.Play();


            //wireRenderer.material.SetFloat("_EmissionForHoles", 1);
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
