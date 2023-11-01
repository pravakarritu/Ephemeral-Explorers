using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public GameObject[] platformSetA;
    public GameObject[] platformSetB;

    private bool isVisibleA = true;
    private bool isVisibleB = false;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            isVisibleA = !isVisibleA;
            isVisibleB = !isVisibleB;

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
}