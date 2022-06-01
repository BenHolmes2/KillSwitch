using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRespawnPoint : MonoBehaviour
{
    public GameObject spawnPos;
    public GameObject player;
    public float cameraFollowSpeed = 100; //maybe for really long rooms have a seperate collider for just this
    public GameObject GameController;
    private int i = 0;
    public int jumpForce;

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && i == 0)
        {
            GameController.GetComponent<GameController>().followSpeed = cameraFollowSpeed; //changes the speed of the camera transition when the player respawns
            GameController.GetComponent<GameController>().respawnPoint = spawnPos;
            player = GameController.GetComponent<GameController>().spawnedPlayer;
            player.GetComponent<PlayerMoveSlide>().jumpForce = jumpForce;

            i++; //iterator to make the spawn change only work once
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // look at pick up body when you want to implement this
        //This is broken, cant acces top parent game object of the collider picked up
        //if (other.gameObject.tag == "Ragdoll"  || other.gameObject.tag == "Item"  )
        //{
        //    Destroy(other.transform.parent.parent.gameObject);
        //}
    }
}
