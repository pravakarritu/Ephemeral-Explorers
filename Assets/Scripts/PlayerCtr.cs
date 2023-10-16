using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        // verticalInput = Input.GetAxis("Vertical");
        bool isJump = Input.GetKey(KeyCode.W);
        bool isJumpStart = Input.GetKeyDown(KeyCode.W);
        bool isJumpFin = Input.GetKeyUp(KeyCode.W);

        // transform.Translate(Vector3.right * horizontalInput * moveSpeed * Time.deltaTime);

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

        // if (verticalInput > 0 && jumpCount < 1) {
        // if (isJump && jumpTime == 0.0f) {
        if (isJumpStart && jumpCount < 1 && jumpTime < jumpTimeLimit) {
            jumpTime += Time.deltaTime;
            ySpeed = jumpForce * jumpCurve.Evaluate(jumpTime);
            // Debug.LogFormat("jumpCurve.Evaluate(jumpTime) = {0}", jumpCurve.Evaluate(jumpTime));
            ++jumpCount;
        }
        else if (!isJumpStart && isJump && jumpTime < jumpTimeLimit) {
            // rbody2D.AddForce(transform.up * jumpForce);
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
            // Debug.LogFormat("Ray collided");
            if (jumpFinish) {
                jumpTime = 0;
                jumpCount = 0;
                jumpFinish = false;
            }
            moveSpeed = defaultSpeed;
        }
        // else if (jumpTime > jumpTimeLimit) {
        //     Debug.LogFormat("jumpCurve.Evaluate(jumpTime) = {0}", jumpCurve.Evaluate(jumpTime));
        //     // xSpeed = 0.0f;
        //     ySpeed = -gravity;
        // }

        // Debug.LogFormat("(xSpeed) = {0}", xSpeed);
        // Debug.LogFormat("(ySpeed) = {0}", ySpeed);

        rbody2D.velocity = new Vector3(xSpeed, ySpeed);

        // Color c = new Color(1.0f, 0.0f, 0.0f);
        // Debug.DrawRay(transform.position, -2.2f * Vector3.up, c, 10, false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Goal") && keyGet) {
            SceneManager.LoadScene("LevelComplete");
        }
        else if (other.gameObject.CompareTag("Key")) {
            Debug.LogFormat("Get key");
            keyGet = true;
            Destroy(other.gameObject);
        }
        // else if (Physics2D.Raycast(transform.position, -Vector3.up, 2.2f)) {
        //     Debug.LogFormat("Ray collided");
        //     jumpCount = 0;
        //     jumpTime = 0;
        //     moveSpeed = defaultSpeed;
        // }
    }
}