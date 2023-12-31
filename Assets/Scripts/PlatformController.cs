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
            isVisibleA = !isVisibleA;
            isVisibleB = !isVisibleB;

            pressV = true;
        }
        else {
            pressV = false;
        }
    }
}