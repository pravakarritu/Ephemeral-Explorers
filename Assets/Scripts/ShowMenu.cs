using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMenu : MonoBehaviour
{
    private GameObject ExitButton, TutorialButton, PlayerControlsButton, TipsPanel, retry, next;
    private bool isClicked = false;

    void Start()
    {
        retry = GameObject.Find("/Canvas/Retry");
        next = GameObject.Find("/Canvas/Next");
        ExitButton = GameObject.Find("/Canvas/Exit");
        PlayerControlsButton = GameObject.Find("/Canvas/PlayerControls");
        TipsPanel = GameObject.Find("/Canvas/TipsPanel");
        ExitButton.SetActive(false);
        if (retry) retry.SetActive(false);
        if (next) next.SetActive(false);
        if (PlayerControlsButton) PlayerControlsButton.SetActive(false);
        if (TipsPanel) TipsPanel.SetActive(false);
    }

    public void MenuSet()
    {
        if (isClicked)
        {
            ExitButton.SetActive(false);
            if (retry) retry.SetActive(false);
            if (next) next.SetActive(false);
            // TutorialButton.SetActive(false);
            if (PlayerControlsButton) PlayerControlsButton.SetActive(false);
            if (TipsPanel)TipsPanel.SetActive(false);
            isClicked = false;
        }
        else
        {
            ExitButton.SetActive(true);
            if (retry) retry.SetActive(true);
            if (next) next.SetActive(true);
            // TutorialButton.SetActive(true);
            if (PlayerControlsButton) PlayerControlsButton.SetActive(true);
            if (TipsPanel)TipsPanel.SetActive(true);
            isClicked = true;
        }
    }
}
