using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShredderKillBox : MonoBehaviour
{
    public GameController Controller;
    public GameObject bloodEffect;
    private Transform pos;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            bloodEffect.transform.position = other.transform.position;
            Controller.StartCoroutine(Controller.respawnPlayer());
            Instantiate(bloodEffect);

        }

        if (other.gameObject.tag == "canPickUp")
        {
            bloodEffect.transform.position = other.transform.position;
            Destroy(other.gameObject.transform.root.gameObject);
            Instantiate(bloodEffect);
        }
    }
}
