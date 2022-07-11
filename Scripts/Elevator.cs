using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Elevator : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
    {
         SceneManager.LoadScene("Menu");
    }
}