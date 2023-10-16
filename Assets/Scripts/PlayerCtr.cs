using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class PlayerCtr : MonoBehaviour
{
    public AnimationCurve dashCurve;
    public AnimationCurve jumpCurve;

    private float horizontalInput, prevHorizontal, dashTime, jumpTime, jumpTimeLimit;
    public float moveSpeed = 10, jumpForce = 250, defaultSpeed, gravity;
    private int jumpCount;
    private bool jumpFinish;
    private bool keyGet = false;

    private Rigidbody2D rbody2D;
    float xSpeed, ySpeed;

    void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
        defaultSpeed = moveSpeed;
        keyGet = false;
        jumpCount = 0;
        jumpTime = 0.0f;
        jumpTimeLimit = 0.3f;
        jumpFinish = false;
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        bool isJump = Input.GetKey(KeyCode.W);
        bool isJumpStart = Input.GetKeyDown(KeyCode.W);
        bool isJumpFin = Input.GetKeyUp(KeyCode.W);

        xSpeed = horizontalInput * moveSpeed;

        if (horizontalInput != 0) {
            dashTime += Time.deltaTime;
        }
        else if (horizontalInput == 0) {
            dashTime = 0.0f;
        }
        else if (horizontalInput > 0 && prevHorizontal < 0) {
            dashTime = 0.0f;
        }
        else if (horizontalInput < 0 && prevHorizontal > 0) {
            dashTime = 0.0f;
        }
        prevHorizontal = horizontalInput;

        xSpeed *= dashCurve.Evaluate(dashTime);

        if (isJumpStart && jumpCount < 1 && jumpTime < jumpTimeLimit) {
            jumpTime += Time.deltaTime;
            ySpeed = jumpForce * jumpCurve.Evaluate(jumpTime);
            ++jumpCount;
        }
        else if (!isJumpStart && isJump && jumpTime < jumpTimeLimit) {
            jumpTime += Time.deltaTime;
            ySpeed = jumpForce * jumpCurve.Evaluate(jumpTime);
        }
        else {
            ySpeed = -gravity;
        }
        if (!jumpFinish) {
            jumpFinish = isJumpFin;
        }

        int layer_mask = LayerMask.GetMask(new string[]{"Default"});
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, -(Vector2)Vector3.up, 2.1f, layer_mask);
        if (hit.collider) {
            if (jumpFinish) {
                jumpTime = 0;
                jumpCount = 0;
                jumpFinish = false;
            }
            moveSpeed = defaultSpeed;
        }

        rbody2D.velocity = new Vector3(xSpeed, ySpeed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Goal") && keyGet) {
            SceneManager.LoadScene("LevelComplete");
        }
        else if (other.gameObject.CompareTag("Key")) {
            keyGet = true;
            Destroy(other.gameObject);
        }
    }
}