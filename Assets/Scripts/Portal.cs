using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Portal : MonoBehaviour
{
    private string exit_tag = "";
    private GameObject exit_portal, player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.tag == "portal_1")
            {
                exit_tag = "portal_2";
            }
            else if (gameObject.tag == "portal_2")
            {
                exit_tag = "portal_1";
            }
            else if (gameObject.tag == "portal_3")
            {
                exit_tag = "portal_4";
            }
            else if (gameObject.tag == "portal_4")
            {
                exit_tag = "portal_3";
            }
            else if (gameObject.tag == "portal_5")
            {
                exit_tag = "portal_6";
            }
            else if (gameObject.tag == "portal_6")
            {
                exit_tag = "portal_5";
            }
            exit_portal = GameObject.FindWithTag(exit_tag);
            collision.gameObject.transform.position = new Vector3(exit_portal.transform.position.x + 2, exit_portal.transform.position.y, 0);
        }
    }
}