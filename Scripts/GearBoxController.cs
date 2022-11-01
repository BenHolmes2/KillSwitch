using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearBoxController : MonoBehaviour
{
    private Animator bridgeAnim;
    private Animator gear1Anim;
    private Animator gear2Anim;

    public GameObject bridge;
    public GameObject gear1;
    public GameObject gear2;
    public GameObject deathColliderObj;
    public GameObject spinColliderObj;
    private BoxCollider deathCollider;
    //private BoxCollider spinCollider;
    private double currentTime;
    private double destroyTime;
    public double destroyTimeModifier;
    private bool destroyed = false;
    private bool timeSet = false;
    private int counter = 0;



    void Awake()
    {
        bridgeAnim = bridge.GetComponent<Animator>();
        gear1Anim = gear1.GetComponent<Animator>();
        gear2Anim = gear2.GetComponent<Animator>();
        counter = 0;
    }

    private void Start()
    {
        deathCollider =  deathColliderObj.GetComponent<BoxCollider>();
        //spinCollider = spinColliderObj.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (counter == 0 )
        {
            gear1Anim.SetBool("On", true);
            gear2Anim.SetBool("On", true);
            bridgeAnim.SetBool("On", true);
            deathCollider.enabled = true;
            destroyTime = 0;
            currentTime = 0;
            timeSet = false;
            destroyed = false;
            //spinCollider.enabled = true;

        }
        else
        {
            gear1Anim.SetBool("On", false);
            gear2Anim.SetBool("On", false);
            bridgeAnim.SetBool("On", false);
            deathCollider.enabled = false;
            //spinCollider.enabled = false;
        }

        if (destroyTime != 0)
        {
            currentTime = Time.timeAsDouble;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("canPickUp") || other.gameObject.CompareTag("canPickUpDeath"))
    //    {
    //        if (!timeSet)
    //        {
    //            destroyTime = Time.timeAsDouble + destroyTimeModifier;
    //            timeSet = true;
    //            destroyed = false;
    //        }
    //    }
    //}


    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("canPickUp") || collision.gameObject.CompareTag("canPickUpDeath"))
        {
            if (!destroyed)
            {
                counter = 1;
            }
            if (!timeSet)
            {
                destroyTime = Time.timeAsDouble + destroyTimeModifier;
                timeSet = true;
            }

            if (currentTime > destroyTime && destroyTime != 0)
            {
                Destroy(collision.gameObject.transform.root.root.gameObject);
                destroyed = true;
                timeSet = false;
                counter = 0;
                destroyTime = 0;
                currentTime = 0;
            }


        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("canPickUp") || collision.gameObject.CompareTag("canPickUpDeath"))
        {
            counter = 0;
        }
    }
}
