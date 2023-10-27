using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveTutorial : MonoBehaviour
{
    public Animator animtut;
    private GameObject player;
    private PlayerCtr playerCtr;
    // Start is called before the first frame update
    void Start()
    {
        animtut = GetComponent<Animator>();
        // Find player object
        player = GameObject.FindWithTag("Player");
        // Get PlayerCtr script
        playerCtr = player.GetComponent<PlayerCtr>();
    }

    void ChangeAnimation()
    {
        animtut.SetInteger("Change", animtut.GetInteger("Change") + 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (animtut.GetInteger("Change") == 0 && Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeAnimation();
        }
        if (animtut.GetInteger("Change") == 1 && Input.GetKeyDown(KeyCode.Space))
        {
            ChangeAnimation();
        }
        if (animtut.GetInteger("Change") == 2 && Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeAnimation();
        }
        if (animtut.GetInteger("Change") == 3 && playerCtr.GetKeyGet())
        {
            ChangeAnimation();
        }
        if (animtut.GetInteger("Change") == 4 && Input.GetKeyDown(KeyCode.Space))
        {
            ChangeAnimation();
        }
        if (animtut.GetInteger("Change") == 6 && Input.GetKeyDown(KeyCode.RightShift))
        {
            ChangeAnimation();
        }
        if (animtut.GetInteger("Change") == 5 && Input.GetKeyDown(KeyCode.RightShift))
        {
            ChangeAnimation();
        }
        if (animtut.GetInteger("Change") == 7 && Input.GetKeyDown(KeyCode.Space))
        {
            ChangeAnimation();
        }
    }
}
