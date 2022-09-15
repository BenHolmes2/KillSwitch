using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PerformanceController : MonoBehaviour
{
    private int roomCount;
    public bool exitRoomPerf;
    private int bodyCountTotal;
    private GameController controller;
    private FPSCounter fCounter;
    private int frames;
    private int bodyCountTemp;
    int[] bodyCountRooms = new int[10];
    int[] fpsDrop = new int[10];
    private int totalDeaths;
    private string totalDeathsStr = "Total Deaths: ";
    private string timesFpsDropped = "Times FPS Dropped below 50: ";
    private string bodyStr = "";
    private string fpsStr = "";
    private string totalBodyStr = "Total Bodies Used: ";
    private int room;
    
    public float min = Mathf.Infinity;



    private void Start()
    {
        fCounter = gameObject.GetComponent<FPSCounter>();
        controller = gameObject.GetComponent<GameController>();
        exitRoomPerf = false;
        roomCount = 0;
        
    }


    void Update()
    {
        bodyCountTotal = controller.bodyCount;
        frames = fCounter.actualFPS;

      if (exitRoomPerf)
        {
            if (frames < 50)
            {
                fpsDrop[roomCount] = frames;
            }
            else
            {
                fpsDrop[roomCount] = frames;
                if (fpsDrop[roomCount]! < 50)
                {
                    min = fpsDrop[roomCount];
                }
            }
            bodyCountTemp = bodyCountTotal - bodyCountTemp;
            bodyCountRooms[roomCount] = bodyCountTemp;
            bodyCountTemp = bodyCountTotal;
            roomCount++;
            exitRoomPerf = false;
        }

    }

    private void OnDestroy()
    {
        totalDeaths = controller.deathByElectricityCount + controller.deathByShreddersCount + controller.deathBySpikesCount;
        room = 0;

        for (int rn = 1; rn < bodyCountRooms.Length; rn++)
        {
     
            bodyStr += "Room " + rn.ToString() + " Body Count:" + bodyCountRooms[room].ToString() + "\n";
            fpsStr += "Room " + rn.ToString() + " Lowest FPS: " + fpsDrop[room].ToString() + "\n";
            room++;
        }


        totalDeathsStr += totalDeaths.ToString() + "\n";
        totalBodyStr += bodyCountTotal.ToString() + "\n";



        File.AppendAllText("PerformanceData.txt", "------------------------------------------------------------------- \n");
        File.AppendAllText("PerformanceData.txt", bodyStr);
        File.AppendAllText("PerformanceData.txt", fpsStr);
        File.AppendAllText("PerformanceData.txt", totalDeathsStr);
        File.AppendAllText("PerformanceData.txt", totalBodyStr);
        File.AppendAllText("PerformanceData.txt", "------------------------------------------------------------------- \n");
    }
}
