using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;

public class PlayerCtrElevator : MonoBehaviour
{
    public Camera cam;
    private Vector3 camDefaultPos;
    private float camDefaultSize, zoomingTime;
    private bool camZoomIn, camZoomStart;
    private float camZoomSize = 25.0f, zoomEndTime = 0.5f;

    public string curLevel = "Level1", nextLevel = "Level2";
    public AnimationCurve dashCurve;
    public AnimationCurve jumpCurve;

    private float horizontalInput, prevHorizontal, dashTime, jumpTime, jumpTimeLimit, elevatorUpTime, elevatorUpTimeLimit;
    public float moveSpeed = 10, jumpForce = 250, defaultSpeed, gravity;
    private int jumpCount;
    private bool jumpFinish, elevatorFinish;
    private bool keyGet = false;

    private Vector3 velocity = Vector3.zero;

    private Rigidbody2D rbody2D, elevatorRBody;
    float xSpeed, ySpeed;

    private GameObject playerAnim, elevator;
    private Animator anim;

    // Metric Manager 
    private MetricManager metricManager;

    private BoxManager boxManager;

    private int numberOfBoxMovements = 0;

    public int numberOfJumpsSuccess;


    // diminishSize
    private bool diminishPowerGet = false;
    private bool revocerSizePowerGet = false;
    private int keyCount = 0;

    private string currentScene;


    void Start()
    {
        camDefaultSize = cam.orthographicSize;
        camDefaultPos = cam.transform.position;
        camZoomIn = false;
        camZoomStart = false;

        rbody2D = GetComponent<Rigidbody2D>();
        defaultSpeed = moveSpeed;
        keyGet = false;
        jumpCount = 0;
        jumpTime = 0.0f;
        jumpTimeLimit = 0.3f;
        jumpFinish = true;
        elevatorUpTime = 0;
        elevatorUpTimeLimit = 2.0f;
        elevatorFinish = true;
        numberOfJumpsSuccess = 0;

        diminishPowerGet = false;
        revocerSizePowerGet = false;

        playerAnim = transform.GetChild(0).gameObject;
        anim = playerAnim.GetComponent<Animator>();

        // Metric Manager Initialization
        metricManager = FindObjectOfType<MetricManager>();
        boxManager = FindObjectOfType<BoxManager>();

        // Find Elevator object
        elevator = GameObject.FindWithTag("Elevator");
        elevatorRBody = elevator.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // If the camera is zoomed in, then the player can move
        if (camZoomIn && camZoomStart)
        {
            Vector3 destPos = new Vector3(transform.position.x, transform.position.y, -10.0f);
            cam.orthographicSize = Mathf.Lerp(camDefaultSize, camZoomSize, zoomingTime / zoomEndTime);
            // Transform camera position using smooth damp
            cam.transform.position = Vector3.SmoothDamp(cam.transform.position, destPos, ref velocity, 0.2f);
        }
    }

    void Update()
    {
        // Check if space bar is pressed
        bool zoomInOut = Input.GetKeyDown(KeyCode.Z);
        if (zoomInOut)
        {
            // Zoom state is opposite of the current state
            camZoomIn = !camZoomIn;
            zoomingTime = 0.0f;
            camZoomStart = true;
        }
        // If the camera is zoomed in, then the player can move
        if (camZoomIn && camZoomStart)
        {
            // Smoothly zoom in the camera
            zoomingTime += Time.deltaTime;
            anim.enabled = true;

            // Get player movement input
            horizontalInput = Input.GetAxis("Horizontal");
            bool isJump = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space);
            bool isJumpStart = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space);
            bool isJumpFin = Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Space);

            // Gradually increase/decrease the horizontal speed of the player
            xSpeed = horizontalInput * moveSpeed;
            if (horizontalInput > 0)
            {
                dashTime += Time.deltaTime;
                playerAnim.transform.localScale = new Vector3(1, 1, 1);
                anim.SetBool("horizontal", true);
            }
            else if (horizontalInput < 0)
            {
                dashTime += Time.deltaTime;
                playerAnim.transform.localScale = new Vector3(-1, 1, 1);
                anim.SetBool("horizontal", true);
            }
            else if (horizontalInput == 0)
            {
                dashTime = 0.0f;
                anim.SetBool("horizontal", false);
            }
            else if (horizontalInput > 0 && prevHorizontal < 0)
            {
                dashTime = 0.0f;
            }
            else if (horizontalInput < 0 && prevHorizontal > 0)
            {
                dashTime = 0.0f;
            }
            prevHorizontal = horizontalInput;
            xSpeed *= dashCurve.Evaluate(dashTime);

            // Allow the player to jump while the jump time is less than the limit
            if (isJumpStart && jumpCount < 1 && !jumpFinish)
            {
                jumpTime += Time.deltaTime;
                ySpeed = jumpForce * jumpCurve.Evaluate(jumpTime);
                ++jumpCount;

                // Analytics for number of Jumps
                ++numberOfJumpsSuccess;
            }
            else if (!isJumpStart && isJump && jumpTime < jumpTimeLimit && !jumpFinish)
            {
                jumpTime += Time.deltaTime;
                ySpeed = jumpForce * jumpCurve.Evaluate(jumpTime);
            }
            else
            {
                ySpeed = -gravity;
            }
            if (!jumpFinish)
            {
                jumpFinish = isJumpFin;
            }

            int layer_mask = LayerMask.GetMask(new string[] { "Default" });
            float w = GetComponent<SpriteRenderer>().bounds.size.x / 2;
            Vector2 vec1 = new Vector2(transform.position.x - w, transform.position.y);
            Vector2 vec2 = new Vector2(transform.position.x + w, transform.position.y);
            RaycastHit2D hit1 = Physics2D.Raycast(vec1, -(Vector2)Vector3.up, 3.5f, layer_mask);
            RaycastHit2D hit2 = Physics2D.Raycast(vec2, -(Vector2)Vector3.up, 3.5f, layer_mask);
            // Debug.DrawRay(vec1, -(Vector2)Vector3.up * 3.5f, Color.red, 100.0f,false);
            // Debug.DrawRay((Vector2)transform.position, -(Vector2)Vector3.up * 3.5f, Color.red, 100.0f,false);
            // Debug.DrawRay((Vector2)transform.position, -(Vector2)Vector3.up * 3.5f, Color.red, 100.0f,false);
            if (hit1.collider)
            {
                if (jumpFinish)
                {
                    jumpTime = 0;
                    jumpCount = 0;
                    jumpFinish = false;
                }
                moveSpeed = defaultSpeed;
                anim.SetBool("jump", false);

                if (hit1.collider.CompareTag("Elevator") && elevatorUpTime < elevatorUpTimeLimit && elevatorFinish) {
                    // elevator.transform.position += 50 * transform.up * Time.deltaTime;
                    elevatorUpTime += Time.deltaTime;
                    elevatorRBody.velocity = 10 * transform.up;
                }
                else if (elevatorUpTime > 0) {
                    elevatorUpTime = elevatorUpTime - Time.deltaTime;
                    elevatorFinish = false;
                    elevatorRBody.velocity = -3 * transform.up;
                }
                else
                {
                    elevatorUpTime = 0;
                    elevatorFinish = true;
                    elevatorRBody.velocity = new Vector3(0.0f, -3.0f);
                }
            }
            else if (hit2.collider)
            {
                if (jumpFinish)
                {
                    jumpTime = 0;
                    jumpCount = 0;
                    jumpFinish = false;
                }
                moveSpeed = defaultSpeed;
                anim.SetBool("jump", false);

                if (hit2.collider.CompareTag("Elevator") && elevatorUpTime < elevatorUpTimeLimit && elevatorFinish) {
                    // elevator.transform.position += 50 * transform.up * Time.deltaTime;
                    elevatorUpTime += Time.deltaTime;
                    elevatorRBody.velocity = 10 * transform.up;
                }
                else if (elevatorUpTime > 0) {
                    elevatorUpTime = elevatorUpTime - Time.deltaTime;
                    elevatorFinish = false;
                    elevatorRBody.velocity = -3 * transform.up;
                }
                else
                {
                    elevatorUpTime = 0;
                    elevatorFinish = true;
                    elevatorRBody.velocity = new Vector3(0.0f, -3.0f);
                }
            }
            else
            {
                anim.SetBool("jump", true);
                elevatorRBody.velocity = new Vector3(0.0f, -3.0f);
            }

            rbody2D.velocity = new Vector3(xSpeed, ySpeed);
        }
        else if (camZoomStart)
        {
            zoomingTime += Time.deltaTime;
            Vector3 srcPos = new Vector3(transform.position.x, transform.position.y, -10.0f);
            cam.orthographicSize = Mathf.Lerp(camZoomSize, camDefaultSize, zoomingTime / zoomEndTime);
            cam.transform.position = Vector3.Lerp(srcPos, camDefaultPos, zoomingTime / zoomEndTime);
            rbody2D.velocity = new Vector3(0.0f, 0.0f);
            anim.enabled = false;
        }

        curLevel = SceneManager.GetActiveScene().name;
        if (curLevel == "Level1")
        {
            nextLevel = "Level2";
        }
        else if (curLevel == "Level2")
        {
            nextLevel = "Level3";
        }
        else if (curLevel == "Level3")
        {
            nextLevel = "Level4";
        }
        else if (curLevel == "Level4")
        {
            nextLevel = "Level5";
        }
    }

    public bool IsZoomIn()
    {
        return camZoomIn;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Goal") && keyGet)
        {
            // Send Analytics when each level in the game ends

            numberOfBoxMovements = boxManager.sendNumberOfBoxMovements();
            metricManager.EndRun();
            string result = metricManager.GetResult(curLevel, numberOfJumpsSuccess, numberOfBoxMovements, diminishPowerGet);
            StartCoroutine(GetRequest(result));

            SceneTransition st = GetComponent<SceneTransition>();
            metricManager.StartRun();

            st.SetLevels(curLevel, nextLevel);
            st.LoadScene();
        }
        else if (other.gameObject.CompareTag("Key"))
        {
            keyGet = true;
            Destroy(other.gameObject);
            // currentScene = SceneManager.GetActiveScene().name;
            // if ((currentScene == "Level3") || (currentScene == "Level4") || (currentScene == "Level6"))
            // {
            //     if (keyCount == 0)
            //     {
            //         keyCount += 1;
            //         Destroy(other.gameObject);
            //     }
            //     else
            //     {
            //         keyGet = true;
            //         Destroy(other.gameObject);
            //     }
            // }
            // else if (currentScene == "Level6")
            // {
            //     if (keyCount == 0)
            //     {
            //         keyCount = 1;
            //         Destroy(other.gameObject);
            //     }
            //     else if (keyCount == 1)
            //     {
            //         keyCount = 2;
            //         Destroy(other.gameObject);
            //     }
            //     else if (keyCount == 2)
            //     {
            //         keyGet = true;
            //         Destroy(other.gameObject);
            //     }

            // }
            // else
            // {
            //     keyGet = true;
            //     Destroy(other.gameObject);
            // }
        }
        else if (other.gameObject.CompareTag("DiminishPower"))
        {
            diminishPowerGet = true;

            Debug.Log("getPower");
            transform.localScale = transform.localScale * 0.5f; // Reduce the player's size by half
            Destroy(other.gameObject); // Destroy the power object
        }
        else if (other.gameObject.CompareTag("BackSizePower"))
        {
            revocerSizePowerGet = true;

            transform.localScale = transform.localScale * 2.0f; // Reduce the player's size by half
            Destroy(other.gameObject); // Destroy the power object
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Collide");
            // rbody2D.velocity = new Vector3(-100, 0);
            transform.position -= new Vector3(1.0f, 0.0f, 0.0f);
        }
    }


    // diminish size

    // private void HandleDiminishPower(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("DiminishPower"))
    //     {
    //         diminishPowerGet = true;

    //         Debug.Log("getPower");
    //         transform.localScale = transform.localScale * 0.5f; // Reduce the player's size by half
    //         Destroy(other.gameObject); // Destroy the power object
    //     }
    // }



    // Get keyGet value
    public bool GetKeyGet()
    {
        return keyGet;
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
