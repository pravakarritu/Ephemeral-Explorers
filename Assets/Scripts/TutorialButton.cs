using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tutorial;
    private bool tutorialActive = false;
    public GameObject levels;
    private bool levelsActive = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void toggleTutorial()
    {
        if (tutorialActive)
        {
            tutorial.SetActive(false);
            tutorialActive = false;
        }
        else
        {
            tutorial.SetActive(true);
            tutorialActive = true;
        }
    }

    public void toggleLevels()
    {
        if (levelsActive)
        {
            levels.SetActive(false);
            levelsActive = false;
        }
        else
        {
            levels.SetActive(true);
            levelsActive = true;
        }
    }
}
