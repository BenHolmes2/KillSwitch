using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridge : MonoBehaviour
{
    public Animator bridgeAnimator;
    private bool bridgeSpinning = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    private void OnTriggerEnter(Collider other)
    {
        bridgeSpinning = bridgeAnimator.GetBool("On");
        if (bridgeSpinning)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<CharacterController>().Move(new Vector3(1f, 0f, 0f));
                Debug.Log("Bridge moved ya");
            }
        }
    }


}
