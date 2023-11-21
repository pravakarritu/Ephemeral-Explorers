using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMenu : MonoBehaviour
{
    private GameObject ExitButton, TutorialButton, PlayerControlsButton;
    private bool isClicked = true;

    void Start()
    {
        ExitButton = GameObject.Find("/Canvas/Exit");
        PlayerControlsButton = GameObject.Find("/Canvas/PlayerControls");
        ExitButton.SetActive(true);
        PlayerControlsButton.SetActive(true);
    }

    public void MenuSet()
    {
        if (isClicked)
        {
            ExitButton.SetActive(false);
            // TutorialButton.SetActive(false);
            PlayerControlsButton.SetActive(false);
            isClicked = false;
        }
        else
        {
            ExitButton.SetActive(true);
            // TutorialButton.SetActive(true);
            PlayerControlsButton.SetActive(true);
            isClicked = true;
        }
    }
}
