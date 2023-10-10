using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private float horizontalInput;
    private float forwardInput;
    public float speed = 20.0f;
    public GameObject character;
    public Rigidbody2D rb;
    public float jumpAmount = 10;
        public float jumpHeight;
    public float jumpStrength = 10f;

    void Start()
    {
        rb = character.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float movespeed = 1.0f;
        transform.position = new Vector2(transform.position.x + movespeed * Time.deltaTime, transform.position.y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
       // Vector2 door_position = GameObject.Find("Door").transform.position;
        GameObject.Find("Door").transform.position = new Vector2(8,1);

    }
}
