using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCtr : MonoBehaviour
{
    private float horizontalInput, verticalInput, prevHorizontal;
    private bool jump;
    public float moveSpeed = 10, jumpForce = 250, defaultSpeed;
    private int jumpCount = 0;
    private bool keyGet = false;

    private Rigidbody2D rbody2D;

    void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
        defaultSpeed = moveSpeed;
        keyGet = false;
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        // verticalInput = Input.GetAxis("Vertical");
        jump = Input.GetKeyDown(KeyCode.W);

        // if (jumpCount > 0 && moveSpeed > 0)
        // {
        //     moveSpeed -= 5 * Time.deltaTime;
        //     // horizontalInput += prevHorizontal;
        // }
        transform.Translate(Vector3.right * horizontalInput * moveSpeed * Time.deltaTime);

        // if (verticalInput > 0 && jumpCount < 1) {
        if (jump && jumpCount < 1)
        {
            // prevHorizontal = horizontalInput;
            rbody2D.AddForce(transform.up * jumpForce);
            // transform.Translate(Vector3.up * jumpSpeed * Time.deltaTime);
            // transform.Translate(Vector3.up * verticalInput * jumpSpeed * Time.deltaTime);
            ++jumpCount;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Debug.LogFormat("jumpCount: = {0}", jumpCount);
        jumpCount = 0;
        moveSpeed = defaultSpeed;
        if (other.gameObject.CompareTag("Goal") && keyGet)
        {
            SceneManager.LoadScene("StartMenu");
        }
        else if (other.gameObject.CompareTag("Key"))
        {
            Debug.LogFormat("Get key");
            keyGet = true;
            Destroy(other.gameObject);
        }
    }
}