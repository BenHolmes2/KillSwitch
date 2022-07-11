using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    //[SerializeField] private Transform Player;
    //[SerializeField] private Transform respawnPoint;
    public GameController Controller;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Controller.StartCoroutine(Controller.respawnPlayer());
            Debug.Log("you should have died");
        }
    }
}


