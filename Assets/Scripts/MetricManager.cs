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

    public string GetResult( string currentLevel, int numberOfJumpsSuccess, int numberOfBoxMovements){

    float playTime = (float)stopWatch.Elapsed.TotalMinutes;

    // UnityEngine.Debug.Log($"Current level: {currentLevel}");

    float level1 = 0.0f;
    float level2 = 0.0f;
    float level3 = 0.0f;
    float level4 = 0.0f;
    float level5 = 0.0f;

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
        default:
            break;
    }


    // UnityEngine.Debug.Log($"Level 1: {level1}");
    // UnityEngine.Debug.Log($"Level 2: {level2}");
    // UnityEngine.Debug.Log($"Level 3: {level3}");
    // UnityEngine.Debug.Log($"Level 4: {level4}");
    // UnityEngine.Debug.Log($"Level 5: {level5}");

    UnityEngine.Debug.Log($"Number of Box Movements from Metrics: {numberOfBoxMovements}");

    string result = string.Format("https://docs.google.com/forms/u/5/d/e/1FAIpQLSdGjXOFO-4U50HRFrV0JlfMlTVqFL7456SmMlCHmBClU1rlzg/formResponse?entry.1928979965={0}&entry.1727223128={1}&entry.2075213155={2:0.00}&entry.1334567754={3:0.00}&entry.830705189={4:0.00}&entry.929535121={5:0.00}&entry.1300638476={6:0.00}", numberOfBoxMovements,numberOfJumpsSuccess, level1, level2,level3,level4,level5);
    return result;
        
    }
}
