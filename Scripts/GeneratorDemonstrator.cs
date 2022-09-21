using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorDemonstrator : MonoBehaviour
{
    public GameObject electricityArc;
    private GameObject electricityArc1;
    public GameObject arcStart;
    private int elecCounter = 0;
    private Vector3 midPoint;
    private GameObject pos;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (elecCounter == 2)
        {
            midPoint = (pos.transform.position + arcStart.transform.position) / 2;
            electricityArc1.transform.position = midPoint;
            electricityArc1.GetComponent<LineRenderer>().SetPosition(0, pos.transform.position);
            electricityArc1.GetComponent<LineRenderer>().SetPosition(1, arcStart.transform.position);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Hip_Root_JNT")
        {
            if (collision.gameObject.GetComponent<RagdollScript>().isElectrified)
            {
                if (elecCounter == 0)
                {
                    elecCounter = 1;
                }
            }
        }


        if (elecCounter == 1)
        {
            pos = collision.gameObject;
            midPoint = (pos.transform.position + arcStart.transform.position) / 2;
            electricityArc.transform.position = midPoint;
            //electricityArc.transform.position = pos.transform.position;
            electricityArc1 = Instantiate(electricityArc);
            electricityArc1.GetComponent<LineRenderer>().SetPosition(0, pos.transform.position);
            electricityArc1.GetComponent<LineRenderer>().SetPosition(1, arcStart.transform.position);
            elecCounter = 2;
        }
    }

    //private void OnCollisionEmpty(Collision collision)

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject == pos)
        {
            if (electricityArc1 != null)
            {
                Destroy(electricityArc1);
            }
            elecCounter = 0;
        }
    }
}
