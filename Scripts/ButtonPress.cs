using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    public GameObject doorGameObject;
    public GameObject buttonGameObject;
    public GameObject wireTexture;
    private Renderer wireRenderer;
    private IDoor door;
    private IDoor button;
    private int counter = 0;

    void Awake()
    {
        door = doorGameObject.GetComponent<IDoor>();
        button = buttonGameObject.GetComponent<IDoor>();
        counter = 0;
        wireRenderer = wireTexture.GetComponent<Renderer>();

    }

    void Update()
    {
        if (counter == 0)//PlayerIsOn == true
        {
            door.CloseDoor();
            button.CloseDoor();
            wireRenderer.material.SetFloat("_EmissionForHoles", -1);
        }
        else
        {
            door.OpenDoor();
            button.OpenDoor();
            wireRenderer.material.SetFloat("_EmissionForHoles", 1);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("canPickUp") || collision.gameObject.CompareTag("canPickUpDeath") || collision.gameObject.CompareTag("Player"))
        {
            counter++;

        }
    }

    //private void OnCollisionEmpty(Collision collision)

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("canPickUp") || collision.gameObject.CompareTag("canPickUpDeath") || collision.gameObject.CompareTag("Player"))
        {
            counter--;
        }
    }
}
