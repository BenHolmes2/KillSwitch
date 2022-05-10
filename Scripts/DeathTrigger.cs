using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private Transform respawnPoint;
    public GameController Controller;

    void OnCollisionEnter(Collision collision)
    {
        Controller.StartCoroutine(Controller.respawnPlayer());
    }
}


