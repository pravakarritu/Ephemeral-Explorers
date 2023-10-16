using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMenu : MonoBehaviour
{
    private GameObject ExitButton, TutorialButton, PlayerControlsButton;
    private bool isClicked = false;
    
    void Start()
    {
        ExitButton = GameObject.Find("/Canvas/Exit");
        // TutorialButton = GameObject.Find("/Canvas/Tutorial");
        PlayerControlsButton = GameObject.Find("/Canvas/PlayerControls");
        ExitButton.SetActive(false);
        // TutorialButton.SetActive(false);
        PlayerControlsButton.SetActive(false);
    }

    public void MenuSet()
    {
        if (isClicked) {
            ExitButton.SetActive(false);
            // TutorialButton.SetActive(false);
            PlayerControlsButton.SetActive(false);
            isClicked = false;
        }
        else {
            ExitButton.SetActive(true);
            // TutorialButton.SetActive(true);
            PlayerControlsButton.SetActive(true);
            isClicked = true;
        }
    }
}
