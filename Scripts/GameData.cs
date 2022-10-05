using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameData : MonoBehaviour
{
    private double startTime;
    private double runningTime;
    private double tempTime;
    private double endTime;
    //public float[] roomTimes;
    public bool exitRoom;
    private int roomCount;
    private int bodyCountTotal;
    private int bodyCountTemp;
    private GameControllerAnimated controller;
    int[] bodyCountRooms = new int[11]; //change these based on number of rooms in the build
    double[] roomTimes = new double[11];
    private string timeStr = "";
    private string timeTotalStr = "Total Time Take: ";
    private string bodyStr = "";
    private string totalBodyStr = "Total Bodies Used: ";
    private string electricityDeathStr = "Total Deaths from Electricity: ";
    private string spikesDeathStr = "Total Deaths from Spikes: ";
    private string shreddersDeathStr = "Total Deaths from Shredders: ";
    private string buzzSawDeathStr = "Total Deaths from BuzzSaws: ";
    private string gearBoxDeathStr = "Total Deaths from GearBoxes: ";
    private string fallingDeathStr = "Total Deaths from Falling: ";
    private string totalDeathStr = "Total Deaths: ";
    private string totalRespawnStr = "Total Times Respawned: ";
    private int totalRespawn;
    private int totalDeaths;
    private int room;

    void Start()
    {
        //Debug.Log(Time.time);
        startTime = Time.timeAsDouble;
        tempTime = startTime;
        exitRoom = false;
        roomCount = 0;
        runningTime = 0;
        controller = gameObject.GetComponent<GameControllerAnimated>();

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
            roomTimes[roomCount] = tempTime; 
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
        endTime = Time.timeAsDouble - startTime;
        totalDeaths = controller.deathByElectricityCount + controller.deathByShreddersCount + controller.deathBySpikesCount;
        totalRespawn = bodyCountTotal - totalDeaths;
        room = 0;

        for (int roomNum = 1; roomNum < roomTimes.Length + 1; roomNum++)
        {
            timeStr += "Room " + roomNum.ToString() + " Time:" + ((int)roomTimes[room]).ToString() + " (sec) \n";
            //Debug.Log(temp);
            bodyStr += "Room " + roomNum.ToString() + " Body Count:" + bodyCountRooms[room].ToString() + "\n";
            //Debug.Log(temp1);
            room++;
        }

        timeTotalStr += endTime.ToString() + "\n";
        electricityDeathStr += controller.deathByElectricityCount.ToString() + "\n";
        spikesDeathStr += controller.deathBySpikesCount.ToString() + "\n";
        shreddersDeathStr += controller.deathByShreddersCount.ToString() + "\n";
        buzzSawDeathStr += controller.deathByBuzzSawCount.ToString() + "\n";
        gearBoxDeathStr += controller.deathByGearBoxCount.ToString() + "\n";
        fallingDeathStr += controller.deathByFallingCount.ToString() + "\n";
        totalDeathStr += totalDeaths.ToString() + "\n";
        totalRespawnStr += totalRespawn.ToString() + "\n";
        totalBodyStr += bodyCountTotal.ToString() + "\n";

        File.AppendAllText("GameData.txt", "---------------------------------------------------------------------------------\n");
        File.AppendAllText("GameData.txt", timeStr);
        File.AppendAllText("GameData.txt", timeTotalStr);
        File.AppendAllText("GameData.txt", bodyStr);
        File.AppendAllText("GameData.txt", totalBodyStr);
        File.AppendAllText("GameData.txt", totalRespawnStr);
        File.AppendAllText("GameData.txt", totalDeathStr);
        File.AppendAllText("GameData.txt", shreddersDeathStr);
        File.AppendAllText("GameData.txt", electricityDeathStr);
        File.AppendAllText("GameData.txt", spikesDeathStr);
        File.AppendAllText("GameData.txt", buzzSawDeathStr);
        File.AppendAllText("GameData.txt", gearBoxDeathStr);
        File.AppendAllText("GameData.txt", fallingDeathStr);
        File.AppendAllText("GameData.txt", "---------------------------------------------------------------------------------\n");

        PlayerPrefs.SetInt("BodiesUsed", bodyCountTotal);
    }
}
