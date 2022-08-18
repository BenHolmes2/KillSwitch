using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameData : MonoBehaviour
{
    public double startTime;
    public double runningTime;
    public double tempTime;
    //public float[] roomTimes;
    public bool exitRoom;
    private int roomCount;
    private int bodyCountTotal;
    private int bodyCountTemp;
    private GameController controller;
    int[] bodyCountRooms = new int[6];
    double[] roomTimes = new double[6];

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(Time.time);
        startTime = Time.timeAsDouble;
        tempTime = startTime;
        exitRoom = false;
        roomCount = 0;
        runningTime = 0;
        controller = gameObject.GetComponent<GameController>();

    }

    // Update is called once per frame
    void Update()
    {

        bodyCountTotal = controller.bodyCount;

        if (exitRoom)
        {
            runningTime += tempTime;
            tempTime = Time.timeAsDouble - runningTime;
            bodyCountTemp = bodyCountTotal - bodyCountTemp;
            roomTimes[roomCount] = tempTime; //subtract start time? is start time getting the right time since its getting the time at the start of each frame
            bodyCountRooms[roomCount] = bodyCountTemp;
            //Debug.Log("---------------------------------------------------------------------------------");
            //Debug.Log(roomTimes[roomCount]);
            //Debug.Log(bodyCountRooms[roomCount]);
            //Debug.Log("---------------------------------------------------------------------------------");
            bodyCountTemp = bodyCountTotal;
            roomCount++;
            exitRoom = false;
        }

    }

    private void OnDestroy()
    {
        //add total time
        //add deaths per room
        string timeStr = "";
        string bodyStr = "";
        string totalBodyStr = "Total Bodies Used: ";
        string electricityDeathStr = "Total Deaths from Electricity: ";
        string spikesDeathStr = "Total Deaths from Spikes: ";
        string shreddersDeathStr = "Total Deaths from Shredders: ";
        string totalDeathStr = "Total Deaths: ";
        string totalRespawnStr = "Total Times Respawned: ";
        int totalDeaths = controller.deathByElectricityCount + controller.deathByShreddersCount + controller.deathBySpikesCount;
        int totalRespawn = bodyCountTotal - totalDeaths;
        int room = 0;

        for (int roomNum = 1; roomNum < roomTimes.Length + 1; roomNum++)
        {
            timeStr += "Room " + roomNum.ToString() + " Time:" + ((int)roomTimes[room]).ToString() + " (sec) \n";
            //Debug.Log(temp);
            bodyStr += "Room " + roomNum.ToString() + " Body Count:" + bodyCountRooms[room].ToString() + "\n";
            //Debug.Log(temp1);
            room++;
        }
        electricityDeathStr += controller.deathByElectricityCount.ToString() + "\n";
        spikesDeathStr += controller.deathBySpikesCount.ToString() + "\n";
        shreddersDeathStr += controller.deathByShreddersCount.ToString() + "\n";
        totalDeathStr += totalDeaths.ToString() + "\n";
        totalRespawnStr += totalRespawn.ToString() + "\n";
        totalBodyStr += bodyCountTotal.ToString() + "\n";

        File.AppendAllText("GameData.txt", "---------------------------------------------------------------------------------\n");
        File.AppendAllText("GameData.txt", timeStr);
        File.AppendAllText("GameData.txt", bodyStr);
        File.AppendAllText("GameData.txt", totalBodyStr);
        File.AppendAllText("GameData.txt", totalRespawnStr);
        File.AppendAllText("GameData.txt", totalDeathStr);
        File.AppendAllText("GameData.txt", shreddersDeathStr);
        File.AppendAllText("GameData.txt", electricityDeathStr);
        File.AppendAllText("GameData.txt", spikesDeathStr);
        File.AppendAllText("GameData.txt", "---------------------------------------------------------------------------------\n");

    }
}
