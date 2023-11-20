using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public GameObject[] platformSetA;
    public GameObject[] platformSetB;

    private bool isVisibleA = true;
    private bool isVisibleB = false;
    private bool pressV = false;

    void Start()
    {

        // When the game loads, one set of platforms appear and one set of platforms are not visible
        foreach (GameObject platform in platformSetA)
        {
            platform.SetActive(isVisibleA);
        }

        foreach (GameObject platform in platformSetB)
        {
            platform.SetActive(isVisibleB);
        }
    }

    void LateUpdate()
    {
        // When the player presses the V key, the platforms switch visibility
        if (pressV)
        {
            print("Late: isVisibleA: " + isVisibleA);
            print("Late: isVisibleB: " + isVisibleB);
            foreach (GameObject platform in platformSetA)
            {
                platform.SetActive(isVisibleA);
            }

            foreach (GameObject platform in platformSetB)
            {
                platform.SetActive(isVisibleB);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            print("before sVisibleA: " + isVisibleA);
            print("before isVisibleB: " + isVisibleB);
            isVisibleA = !isVisibleA;
            isVisibleB = !isVisibleB;
            print("isVisibleA: " + isVisibleA);
            print("isVisibleB: " + isVisibleB);

            pressV = true;

            // foreach (GameObject platform in platformSetA)
            // {
            //     platform.SetActive(isVisibleA);
            // }

            // foreach (GameObject platform in platformSetB)
            // {
            //     platform.SetActive(isVisibleB);
            // }
        }
        else {
            pressV = false;
        }
    }
}