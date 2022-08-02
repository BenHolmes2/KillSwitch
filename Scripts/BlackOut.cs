using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackOut : MonoBehaviour
{
    public GameObject blackOutSquare;
    public float fadeAmount;
    public int fadeSpeed = 500;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(FadeBlackOutSqaure(true, fadeSpeed));
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(FadeBlackOutSqaure(false, fadeSpeed));
        }
    }

    public IEnumerator FadeBlackOutSqaure(bool fadeToBlack = true, int fadeSpeed = 500)
    {
        Color objectColor = blackOutSquare.GetComponent<Image>().color;
        float fadeAmount;

        if (fadeToBlack)
        {
            while (blackOutSquare.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
                Debug.Log(fadeAmount);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b ,fadeAmount);
                Debug.Log(objectColor);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
        else
        {
            while (blackOutSquare.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                Debug.Log(fadeAmount);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                Debug.Log(objectColor);
                yield return null;
            }
        }
    }
}
