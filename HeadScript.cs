using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadScript : MonoBehaviour
{
    private GameObject gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController");

        if (gameObject.tag == "canPickUpDeath")
        {

        }

    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.tag == "canPickUpDeath")
        {
            if (collision.gameObject.tag == "Head")
            {
                gameController.GetComponent<GameController1>().hitGround = true;
            }
        }

    }
}
