using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    public GameController Controller;
    public GameObject bloodEffect;
    private Transform pos;

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Shredders"))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                bloodEffect.transform.position = other.transform.position;
                Controller.StartCoroutine(Controller.respawnPlayer());
                Controller.deathByShreddersCount++;
                //Controller.respawnPlayer();
                //add this back if we want blood
                //Instantiate(bloodEffect);

            }

            if (other.gameObject.CompareTag("canPickUp") || other.gameObject.CompareTag("canPickUpDeath"))
            {
                if (!Controller.isRespawn)
                {
                    bloodEffect.transform.position = other.transform.position;
                    Destroy(other.gameObject.transform.root.gameObject);
                    //add this back if we want blood
                    //Instantiate(bloodEffect);
                }
            }
        }

        if (gameObject.CompareTag("Spikes"))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Controller.StartCoroutine(Controller.respawnPlayer());
                Controller.deathBySpikesCount++;
                //Controller.respawnPlayer();
                Debug.Log("you should have died");
            }
        }

        if (gameObject.CompareTag("LightningPoles"))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                //Controller.respawnPlayer();
                Controller.StartCoroutine(Controller.respawnPlayer());
                Controller.deathByElectricityCount++;
            }
        }


        if (gameObject.CompareTag("GearBox"))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                //Controller.respawnPlayer();
                Controller.StartCoroutine(Controller.respawnPlayer());
                Controller.deathByGearBoxCount++;
            }
        }

        if (gameObject.CompareTag("DeathFloor"))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                //Controller.respawnPlayer();
                Controller.StartCoroutine(Controller.respawnPlayer());
                Controller.deathByFallingCount++;
            }
        }

        if (gameObject.CompareTag("BuzzSaw"))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (!Controller.isRespawn)
                {
                    Controller.StartCoroutine(Controller.respawnPlayer());
                    Controller.deathByBuzzSawCount++;
                }
            }

            //turn this back on if we want the buzzsaw to destroy bodies with a blood effect
            //if (other.gameObject.CompareTag("canPickUp") || other.gameObject.CompareTag("canPickUpDeath"))
            //{
            //    if (!Controller.isRespawn)
            //    {
            //        bloodEffect.transform.position = other.transform.position;
            //        Destroy(other.gameObject.transform.root.gameObject);
            //        Instantiate(bloodEffect);
            //    }
            //}
        }
    }
}
