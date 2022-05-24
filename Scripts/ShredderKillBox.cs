using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShredderKillBox : MonoBehaviour
{
    public GameController Controller;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Controller.StartCoroutine(Controller.respawnPlayer());

        }

        if (other.gameObject.tag == "canPickUp")
        {
            Destroy(other.gameObject.transform.root.gameObject);
        }
    }
}
