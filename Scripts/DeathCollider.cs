using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    public GameController1 Controller;
    public GameObject bloodEffect;
    private Transform pos;

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Shredders"))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                bloodEffect.transform.position = other.transform.position;
                //Controller.StartCoroutine(Controller.respawnPlayer());
                Controller.respawnPlayer();
                Instantiate(bloodEffect);

            }

            if (other.gameObject.CompareTag("canPickUp") || other.gameObject.CompareTag("canPickUpDeath"))
            {
                if (!Controller.isRespawn)
                {
                    bloodEffect.transform.position = other.transform.position;
                    Destroy(other.gameObject.transform.root.gameObject);
                    Instantiate(bloodEffect);
                }
            }
        }

        if (gameObject.CompareTag("Spikes"))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                //Controller.StartCoroutine(Controller.respawnPlayer());
                Controller.respawnPlayer();
                Debug.Log("you should have died");
            }
        }

        if (gameObject.CompareTag("LightningPoles"))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Controller.respawnPlayer();
            }
        }

    }
}
