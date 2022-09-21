using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalRoom : MonoBehaviour
{
    public int bodiesUsed;
    public int bodiesCheck = 0;
    public GameObject ragdoll;
    public Transform spawnPos;
    public float spawnTime = 1.5f;
    public float spawnTimeModifyier = 0.01f;
    private double startTime;
    private double tempTime;
    private double tempTime1;
    private bool check = false;
    private bool check1 = false;



    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.timeAsDouble;

    }

    // Update is called once per frame
    void Update()
    {
        if (check)
        {
            if (bodiesCheck < bodiesUsed)
            {
                tempTime = Time.timeAsDouble;

                if (tempTime > startTime)
                {
                    if (spawnTimeModifyier < 0.2)
                    {
                        spawnTimeModifyier *= 1.1f;
                    }
                    if (spawnTime > 0.35)
                    {
                        spawnTime = spawnTime - spawnTimeModifyier;

                    }

                    InantiateRagdolls();
                    startTime = tempTime + spawnTime;
                    bodiesCheck++;
                }
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Player"))
        //{
        //    SpawnRagdolls();
        //}
        if (!check)
        {
            check = true;
            check1 = true;
            startTime = Time.timeAsDouble + spawnTime;
        }
    }


    private void SpawnRagdolls()
    {
        for (int i = 0; i < bodiesUsed; i++)
        {
            Invoke("InantiateRagdolls", spawnTime);
            //spawnTime = spawnTime;
        }
    }

    private void InantiateRagdolls()
    {
        ragdoll.transform.position = spawnPos.position;
        ragdoll.transform.rotation = spawnPos.rotation;
        Instantiate(ragdoll);
    }
}
