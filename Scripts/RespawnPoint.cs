using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public GameObject player;
    public Transform respawnPoint;

    [SerializeField]
    private GameObject deadBody;
    private Rigidbody rb;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            
            z//having trouble getting the player to spawn in a new postion each time the key is pressed, currently it only works sometimes
            //player.transform.position = respawnPoint.transform.position;
            //player.GetComponent<PlayerMovement>().enabled = false;
            //player.GetComponent<Transform>().position = respawnPoint.transform.position;
            StartCoroutine(Example());
            //Instantiate(deadBody);
            //player.GetComponent<PlayerMovement>().enabled = true;
            

            //StartCoroutine(ExampleCoroutine());
            //new WaitForSeconds(6);

            
            // rb = GetComponent<Rigidbody>();

            // rb.AddForce(0, 0, 1f, ForceMode.Impulse);
        }
    }

    //IEnumerator ExampleCoroutine()
    //{
    //    //Print the time of when the function is first called.
    //    Debug.Log("Started Coroutine at timestamp : " + Time.time);

    //    //yield on a new YieldInstruction that waits for 5 seconds.
    //    yield return new WaitForSeconds(5);

    //    //After we have waited 5 seconds print the time again.
    //    Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    //}





    IEnumerator Example()
    {
        Debug.Log("Hello");
        //wait 3 seconds
        deadBody.transform.position = player.transform.position;

        player.GetComponent<PlayerMoveSlide>().enabled = false;
        player.GetComponent<Transform>().position = respawnPoint.transform.position;
        yield return new WaitForSeconds(0.2f);
        player.GetComponent<PlayerMoveSlide>().enabled = true;
        Instantiate(deadBody);
        

        Debug.Log("Goodbye");
    }
}

