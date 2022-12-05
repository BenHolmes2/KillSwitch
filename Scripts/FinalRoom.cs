using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class FinalRoom : MonoBehaviour
{
    private int bodiesUsed;
    public int bodiesCheck = 0;
    public GameObject ragdoll;
    public Transform spawnPos;
    public float spawnTime = 1.5f;
    public float spawnTimeModifyier = 0.01f;
    private double startTime;
    private int fadeStart;
    private double tempTime;
    private double tempTime1;
    private bool check = false;
    public GameObject blackOutSquare;
    public float j;
    public bool addExtraBodies = false;
    public TMP_Text counterText;
    public Animator Credits;
    public GameObject reticule;
    public GameObject creditsCanvas;
    private int frames = 0;
    private bool faded = false;




    // Start is called before the first frame update
    void Start()
    {
        counterText.text = "0";
        //blackOutSquare.GetComponent<Image>().color = new Color(0, 0, 0, 255);
        startTime = Time.timeAsDouble;
        if (PlayerPrefs.GetInt("BodiesUsed") != 0)
        {
            bodiesUsed = PlayerPrefs.GetInt("BodiesUsed");
            if (addExtraBodies)
            {
                bodiesUsed += 20;
            }
        }
        else
        {
            bodiesUsed = 50;
        }

        //StartCoroutine(FadeBlackOutSqaure(false, 0.2f));
    }

    // Update is called once per frame
    void Update()
    {
        if (frames < 120)
        {
            frames++;
        }

        if (frames >= 120 && !faded)
        {
            Debug.Log("?>?>?");
            StartCoroutine(FadeBlackOutSqaure(false, 0.2f));
            faded = true;
        }

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
                    if (spawnTime > 0.5)
                    {
                        spawnTime = spawnTime - spawnTimeModifyier;

                    }

                    InantiateRagdolls();
                    startTime = tempTime + spawnTime;
                    bodiesCheck++;
                }
            }
            if (bodiesCheck + 10 > bodiesUsed)
            {
                StartCoroutine(FadeBlackOutSqaure());

            }
            if (blackOutSquare.GetComponent<Image>().color.a >= 1)
            {
                creditsCanvas.SetActive(true);
                Credits.SetBool("PlayCredits", true);
                reticule.SetActive(false);
                //SceneManager.LoadScene("MainMenuV2");

            }
            if (Credits.GetBool("isFinished") == true)
            {
                SceneManager.LoadScene("MainMenuV2");
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
        counterText.text = bodiesCheck.ToString();
    }

    public IEnumerator FadeBlackOutSqaure(bool fadeToBlack = true, float fadeSpeed = 0.15f)
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
        else
        {
            while (blackOutSquare.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                j = blackOutSquare.GetComponent<Image>().color.a;
                yield return null;
            }
        }
    }
}
