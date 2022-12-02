using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("canPickUp") || collision.gameObject.CompareTag("canPickUpDeath"))
        {
            collision.gameObject.transform.root.root.gameObject.transform.parent = gameObject.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("canPickUp") || collision.gameObject.CompareTag("canPickUpDeath"))
        {
            collision.gameObject.transform.root.root.gameObject.transform.parent = gameObject.transform;
        }
    }

}
