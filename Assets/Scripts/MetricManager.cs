using System;
using System.Diagnostics;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MetricManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Stopwatch stopWatch = new Stopwatch();

    void Start()
    {
        stopWatch.Start();
    }

    public void StartRun(){
        stopWatch.Start();
    }

    public void EndRun(){
        stopWatch.Stop();
    }

    public string GetResult( string currentLevel, int numberOfJumpsSuccess, int numberOfBoxMovements, bool diminishPowerGet){

    float playTime = (float)stopWatch.Elapsed.TotalMinutes;

    // UnityEngine.Debug.Log($"Current level: {currentLevel}");

    float level1 = 0.0f;
    float level2 = 0.0f;
    float level3 = 0.0f;
    float level4 = 0.0f;
    float level5 = 0.0f;
    float level6 = 0.0f;
    float level7 = 0.0f;

    switch (currentLevel)
    {
        case "Level1":
            level1 = playTime;
            break;
        case "Level2":
            level2 = playTime;
            break;
        case "Level3":
            level3 = playTime;
            break;
        case "Level4":
            level4 = playTime;
            break;
        case "Level5":
            level5 = playTime;
            break;
        case "Level6":
            level6 = playTime;
            break;
        case "Level7":
            level7 = playTime;
            break;
        default:
            break;
    }


    UnityEngine.Debug.Log($"Number of Box Movements from Metrics: {numberOfBoxMovements}");

    string result = string.Format("https://docs.google.com/forms/u/5/d/e/1FAIpQLSdGjXOFO-4U50HRFrV0JlfMlTVqFL7456SmMlCHmBClU1rlzg/formResponse?entry.962410089={0}&entry.1928979965={1}&entry.1727223128={2}&entry.2075213155={3:0.00}&entry.1334567754={4:0.00}&entry.830705189={5:0.00}&entry.929535121={6:0.00}&entry.1300638476={7:0.00}&entry.428638582={8:0.00}&entry.1744762258={9:0.00}", diminishPowerGet, numberOfBoxMovements,numberOfJumpsSuccess, level1,level2,level3,level4,level5,level6,level7);
    return result;
        
    }
}
