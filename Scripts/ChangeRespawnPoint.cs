using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRespawnPoint : MonoBehaviour
{
    public GameObject spawnPos;
    public float cameraFollowSpeed = 100; //maybe for really long rooms have a seperate collider for just this
    public GameObject GameController;
    private int i = 0;

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && i == 0)
        {
            GameController.GetComponent<GameController>().followSpeed = cameraFollowSpeed; //changes the speed of the camera transition when the player respawns
            GameController.GetComponent<GameController>().respawnPoint = spawnPos;
            i++; //iterator to make the spawn change only work once
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //This is broken, cant acces top parent game object of the collider picked up
        //if (other.gameObject.tag == "Ragdoll"  || other.gameObject.tag == "Item"  )
        //{
        //    Destroy(other.transform.parent.parent.gameObject);
        //}
    }
}
