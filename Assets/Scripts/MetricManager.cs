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

    public string GetResult( string currentLevel){

    float playTime = (float)stopWatch.Elapsed.TotalMinutes;

    UnityEngine.Debug.Log($"Current level: {currentLevel}");

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


    UnityEngine.Debug.Log($"Level 1: {level1}");
    UnityEngine.Debug.Log($"Level 2: {level2}");
    UnityEngine.Debug.Log($"Level 3: {level3}");
    UnityEngine.Debug.Log($"Level 4: {level4}");
    UnityEngine.Debug.Log($"Level 5: {level5}");

    string result = string.Format("https://docs.google.com/forms/d/e/1FAIpQLSdGjXOFO-4U50HRFrV0JlfMlTVqFL7456SmMlCHmBClU1rlzg/formResponse?entry.2075213155={0:0.00}&entry.1334567754={1:0.00}&entry.830705189={2:0.00}&entry.929535121={3:0.00}&entry.1300638476={4:0.00}", level1, level2,level3,level4,level5);
    return result;
        
    }
}
