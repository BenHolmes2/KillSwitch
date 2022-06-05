using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
	public GameObject SceneManager;
	private void OnTriggerEnter(Collider other)
    {
         SceneManager.LoadScene("Menu");
    }
}