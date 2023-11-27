using UnityEngine;

public class Portal : MonoBehaviour
{
    private GameObject player;
    private static bool isTeleporting = false;
    private static float teleportCooldown = 0.5f; // cooldown time in seconds

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isTeleporting)
        {
            teleportCooldown -= Time.deltaTime;
            if (teleportCooldown <= 0)
            {
                isTeleporting = false;
                teleportCooldown = 0.5f; // Reset cooldown
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player && !isTeleporting)
        {
            GameObject exitPortal = FindExitPortal(gameObject.tag);
            if (exitPortal != null)
            {
                TeleportPlayerTo(exitPortal);
                isTeleporting = true;
            }
        }
    }

    private GameObject FindExitPortal(string entryTag)
    {
        // Assuming the entry portals are tagged as "portal_X" and exit portals as "portal_Y"
        string exitTag = entryTag == "portal_1" ? "portal_2" : "portal_1";
        return GameObject.FindWithTag(exitTag);
    }

    private void TeleportPlayerTo(GameObject exitPortal)
    {
        // Teleport player to the exit portal with some offset, adjust as needed
        player.transform.position = exitPortal.transform.position + new Vector3(1, 0, 0);
    }
}




























// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// public class Portal : MonoBehaviour
// {
//     private string exit_tag = "";
//     private GameObject exit_portal, player;
//     // Start is called before the first frame update
//     void Start()
//     {
//         player = GameObject.Find("Player");
//     }
//     // Update is called once per frame
//     private void OnTriggerEnter2D(Collider2D collision)
//     {
//         if (collision.gameObject.CompareTag("Player"))
//         {
//             if (gameObject.tag == "portal_1")
//             {
//                 exit_tag = "portal_2";
//             }
//             else if (gameObject.tag == "portal_2")
//             {
//                 exit_tag = "portal_1";
//             }
//             else if (gameObject.tag == "portal_3")
//             {
//                 exit_tag = "portal_4";
//             }
//             else if (gameObject.tag == "portal_4")
//             {
//                 exit_tag = "portal_3";
//             }
//             else if (gameObject.tag == "portal_5")
//             {
//                 exit_tag = "portal_6";
//             }
//             else if (gameObject.tag == "portal_6")
//             {
//                 exit_tag = "portal_5";
//             }
//             exit_portal = GameObject.FindWithTag(exit_tag);
//             collision.gameObject.transform.position = new Vector3(exit_portal.transform.position.x - 5, exit_portal.transform.position.y, 0);
//         }
//     }
// }