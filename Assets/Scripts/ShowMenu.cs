using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMenu : MonoBehaviour
{
    private GameObject ExitButton, TutorialButton, PlayerControlsButton, TipsPanel;
    private bool isClicked = false;

    void Start()
    {
        ExitButton = GameObject.Find("/Canvas/Exit");
        PlayerControlsButton = GameObject.Find("/Canvas/PlayerControls");
        TipsPanel = GameObject.Find("/Canvas/TipsPanel");
        ExitButton.SetActive(false);
        if (PlayerControlsButton) PlayerControlsButton.SetActive(false);
        if (TipsPanel) TipsPanel.SetActive(false);
    }

    public void MenuSet()
    {
        if (isClicked)
        {
            ExitButton.SetActive(false);
            // TutorialButton.SetActive(false);
            if (PlayerControlsButton) PlayerControlsButton.SetActive(false);
            if (TipsPanel)TipsPanel.SetActive(false);
            isClicked = false;
        }
        else
        {
            ExitButton.SetActive(true);
            // TutorialButton.SetActive(true);
            if (PlayerControlsButton) PlayerControlsButton.SetActive(true);
            if (TipsPanel)TipsPanel.SetActive(true);
            isClicked = true;
        }
    }
}
