using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class sphere_movement : MonoBehaviour
{
    Rigidbody2D body;
    float horizontal;
    float vertical;
    public Rigidbody2D rb;
    public float jumpAmount = 10;
    public float jumpHeight;
    public float jumpStrength = 0.1f;
    public GameObject character;
    public float runSpeed = 20.0f;
    private GameObject player;
    PlayerCtr playerCtr;
    public Vector2 position;
    private Vector3 originalPos;

    void Start()
    {
        rb = character.GetComponent<Rigidbody2D>();
        originalPos = character.transform.position;
        player = GameObject.FindWithTag("Player");
        playerCtr = player.GetComponent<PlayerCtr>();
        position = GameObject.Find("Player").transform.position;
        // Debug.Log(position);

    }

    void Update()
    {
        // if(character.transform.position.y < originalPos.y + 12)
        // {
        //     GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
        // }
        // else if (character.transform.position.y > originalPos.y + 12)
        // {
        //     GetComponent<Rigidbody2D>().AddForce(Vector2.down * jumpStrength * 1.15f, ForceMode2D.Impulse);
        // }
        if(GameObject.Find("enemy").transform.position.y < 5)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
        }
        else if (GameObject.Find("enemy").transform.position.y > 5)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.down * jumpStrength, ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.down * 40, ForceMode2D.Impulse);
        if (col.gameObject.CompareTag("Player"))
        {
            GameObject.Find("Player").transform.position = position;
        }
    }

}

