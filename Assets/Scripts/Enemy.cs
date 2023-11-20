using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    Rigidbody2D body;
    float horizontal;
    float vertical;
    public Rigidbody2D rb;
    public float jumpAmount = 10;
    public float jumpHeight;
    public float jumpStrength = 0.1f;
    public GameObject character, box;
    public float runSpeed = 20.0f;
    private GameObject player;
    PlayerCtr playerCtr;
    public Vector2 position;
    private Vector3 originalDiff, boxPos;
    private bool up;

    void Start()
    {
        rb = character.GetComponent<Rigidbody2D>();
        boxPos = box.transform.position;
        originalDiff = character.transform.position - boxPos;
        player = GameObject.FindWithTag("Player");
        playerCtr = player.GetComponent<PlayerCtr>();
        position = GameObject.Find("Player").transform.position;
        // Debug.Log(position);
        up = true;
    }

    void Update()
    {
        boxPos = box.transform.position;
        float relativeY_fromBox = transform.position.y - boxPos.y;
        // Debug.LogFormat("{0}", relativeY_fromBox);
        if(up && relativeY_fromBox < -0.7)
        {
            // Debug.LogFormat("up");
            transform.position += new Vector3(0.0f, Time.deltaTime*5, 0.0f);
        }
        else if (relativeY_fromBox >= -0.7)
        {
            // Debug.LogFormat("up = false");
            up = false;
            transform.position -= new Vector3(0.0f, Time.deltaTime*5, 0.0f);
        }
        else if (!up && relativeY_fromBox > originalDiff.y) {
            // Debug.LogFormat("down");
            transform.position -= new Vector3(0.0f, Time.deltaTime*5, 0.0f);
        }
        else if (!up && relativeY_fromBox <= originalDiff.y) {
            // Debug.LogFormat("up = true");
            transform.position = boxPos + originalDiff;
            up = true;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            player.transform.position = position;
        }
    }

}

