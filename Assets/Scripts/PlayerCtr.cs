using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;

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

    private GameObject playerAnim;
    private Animator anim;

    // Metric Manager 
    private MetricManager metricManager;

    void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
        defaultSpeed = moveSpeed;
        keyGet = false;
        jumpCount = 0;
        jumpTime = 0.0f;
        jumpTimeLimit = 0.3f;
        jumpFinish = true;

        playerAnim = transform.GetChild(0).gameObject;
        anim = playerAnim.GetComponent<Animator>();

        // Metric Manager Initialization
        metricManager = FindObjectOfType<MetricManager>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        bool isJump = Input.GetKey(KeyCode.UpArrow);
        bool isJumpStart = Input.GetKeyDown(KeyCode.UpArrow);
        bool isJumpFin = Input.GetKeyUp(KeyCode.UpArrow);

        xSpeed = horizontalInput * moveSpeed;

        if (horizontalInput > 0) {
            dashTime += Time.deltaTime;
            playerAnim.transform.localScale = new Vector3(1, 1, 1);
            anim.SetBool("horizontal", true);
        }
        else if (horizontalInput < 0) {
            dashTime += Time.deltaTime;
            playerAnim.transform.localScale = new Vector3(-1, 1, 1);
            anim.SetBool("horizontal", true);
        }
        else if (horizontalInput == 0) {
            dashTime = 0.0f;
            anim.SetBool("horizontal", false);
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
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, -(Vector2)Vector3.up, 3.5f, layer_mask);
        // Debug.DrawRay((Vector2)transform.position, -(Vector2)Vector3.up * 3.5f, Color.red, 100.0f,false);
        if (hit.collider) {
            if (jumpFinish) {
                jumpTime = 0;
                jumpCount = 0;
                jumpFinish = false;
            }
            moveSpeed = defaultSpeed;
            anim.SetBool("jump", false);
        }
        else {
            anim.SetBool("jump", true);
        }

        rbody2D.velocity = new Vector3(xSpeed, ySpeed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Goal") && keyGet) {
            // Send Analytics when game ends
            metricManager.EndRun();
            string result = metricManager.GetResult();
            StartCoroutine(GetRequest(result));

            SceneManager.LoadScene("LevelComplete");
        }
        else if (other.gameObject.CompareTag("Key")) {
            keyGet = true;
            Destroy(other.gameObject);
        }
    }


    // Send the analytics to the google form
        IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }
}