using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Elevator : MonoBehaviour
{
    public GameObject blackOutSquare;
    public float j;
    private bool check = false;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(FadeBlackOutSqaure());
        check = true;
    }

    private void Update()
    {
        if (blackOutSquare.GetComponent<Image>().color.a >= 1 && check)
        {
            SceneManager.LoadScene("FinalRoom");
        }
    }

    public IEnumerator FadeBlackOutSqaure(bool fadeToBlack = true, float fadeSpeed = 0.5f)
    {
        Color objectColor = blackOutSquare.GetComponent<Image>().color;
        float fadeAmount;

        if (fadeToBlack)
        {
            j = blackOutSquare.GetComponent<Image>().color.a;
            while (blackOutSquare.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                j = blackOutSquare.GetComponent<Image>().color.a;
                yield return null;
            }
        }
        //else
        //{
        //    while (blackOutSquare.GetComponent<Image>().color.a > 0)
        //    {
        //        fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
        //        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
        //        blackOutSquare.GetComponent<Image>().color = objectColor;
        //        j = blackOutSquare.GetComponent<Image>().color.a;
        //        yield return null;
        //    }
        //}
    }

}