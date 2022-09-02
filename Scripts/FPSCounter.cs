using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    public TextMeshProUGUI FPSText;

    private float pollingTime = 1f;
    private float time;
    private int frameCount;
    public int actualFPS;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        frameCount++;
        
        if(time >= pollingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            FPSText.text = frameRate.ToString() + " FPS";
            actualFPS = frameRate;


            time -= pollingTime;
            frameCount = 0;

        }

        
    }
}
