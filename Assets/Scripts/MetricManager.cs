using System;
using System.Diagnostics;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MetricManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Stopwatch stopWatch = new Stopwatch();

    void Start()
    {
        stopWatch.Start();
    }

    public void EndRun(){
        stopWatch.Stop();
    }

    public string GetResult(){

    float playTime = (float)stopWatch.Elapsed.TotalMinutes;
    string result = string.Format("https://docs.google.com/forms/d/e/1FAIpQLScywbIqPgUsz8mMFOxUDuxT7EttYwewOaHSmpXCcKalJDcZWA/formResponse", playTime);
    return result;
        
    }
}
