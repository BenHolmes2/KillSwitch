using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public bool isStart;
	void OnMouseUp()
	{
		if (isStart)
		{
			SceneManager.LoadScene("Playtest");
		}

	}
}
