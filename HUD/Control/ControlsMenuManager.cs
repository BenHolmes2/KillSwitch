using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlsMenuManager : MonoBehaviour
{
    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }
}
