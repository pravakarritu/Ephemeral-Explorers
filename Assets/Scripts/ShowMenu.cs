using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMenu : MonoBehaviour
{
    private GameObject ExitButton, TutorialButton;
    private bool isClicked = false;
    
    void Start()
    {
        ExitButton = GameObject.Find("/Canvas/Exit");
        TutorialButton = GameObject.Find("/Canvas/Tutorial");
        ExitButton.SetActive(false);
        TutorialButton.SetActive(false);
    }

    public void MenuSet()
    {
        if (isClicked) {
            ExitButton.SetActive(false);
            TutorialButton.SetActive(false);
            isClicked = false;
        }
        else {
            ExitButton.SetActive(true);
            TutorialButton.SetActive(true);
            isClicked = true;
        }
    }
}
