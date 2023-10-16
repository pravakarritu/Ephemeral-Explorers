using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomSceneManager : MonoBehaviour
{
    private GameObject moveSceneWorld, player;
    private int playerIndex, activeSceneIndex, activeBoxIndex, playerBoxIndex;
    private bool changeBox, leftArrow, rightArrow, upArrow, downArrow;
    private float moveHorizontal, moveVertiacal;

    private GameObject[] scenes = new GameObject[4];
    private GameObject[] cameraFrame = new GameObject[4];
    private Vector3[] boxPos = new Vector3[4];
    private int[] boxScene = new int[4];

    public Color32 normalColor = new Color32(190, 190, 190, 255);
    public Color32 activeColor = new Color32(100, 100, 100, 255);

    private Camera[] sceneCam = new Camera[4];
    public TextMeshProUGUI player_movement_control;
    public TextMeshProUGUI screen_control;
    public TextMeshProUGUI screen_select;




    void Start()
    {
        //
        cameraFrame[0] = GameObject.Find("/Scene1/GameWorld/CameraFrame");
        cameraFrame[1] = GameObject.Find("/Scene2/GameWorld/CameraFrame");
        cameraFrame[2] = GameObject.Find("/Scene3/GameWorld/CameraFrame");
        cameraFrame[3] = GameObject.Find("/Scene4");

        boxPos[0] = cameraFrame[0].transform.position;
        boxPos[1] = cameraFrame[1].transform.position;
        boxPos[2] = cameraFrame[2].transform.position;
        boxPos[3] = cameraFrame[3].transform.position;//GameObject.Find("/Scene4").transform.position;
        //

        boxScene[0] = 0;
        boxScene[1] = 1;
        boxScene[2] = 2;
        boxScene[3] = 3;

        player = GameObject.FindWithTag("Player");
        playerIndex = activeSceneIndex = activeBoxIndex = playerBoxIndex = 0;
        // scenes[activeSceneIndex].GetComponent<Renderer>().material.color = activeColor;
        for (int i = 0; i < 4; ++i) {
            GameObject childObj = cameraFrame[activeSceneIndex].transform.GetChild(i).gameObject;
            childObj.GetComponent<Renderer>().material.color = activeColor;
        }

        sceneCam[0] = GameObject.Find("/Scene1/GameWorld/CameraFrame/Scene1Camera").GetComponent<Camera>();
        sceneCam[1] = GameObject.Find("/Scene2/GameWorld/CameraFrame/Scene2Camera").GetComponent<Camera>();
        sceneCam[2] = GameObject.Find("/Scene3/GameWorld/CameraFrame/Scene3Camera").GetComponent<Camera>();
        sceneCam[3] = GameObject.Find("/Scene4/Scene4Camera").GetComponent<Camera>();
    }

    void Update()
    {
        changeBox = Input.GetKeyDown(KeyCode.Space);
        if (changeBox)
        {
            Debug.LogFormat("After : activeSceneIndex = {0}", activeSceneIndex);

            for (int i = 0; i < 4; ++i) {
                GameObject childObj = cameraFrame[activeSceneIndex].transform.GetChild(i).gameObject;
                childObj.GetComponent<Renderer>().material.color = normalColor;
            }
            ++activeSceneIndex;
            activeSceneIndex %= 3;
            for (int i = 0; i < 4; ++i) {
                GameObject childObj = cameraFrame[activeSceneIndex].transform.GetChild(i).gameObject;
                childObj.GetComponent<Renderer>().material.color = activeColor;
            }
            
            for (int i = 0; i < 4; ++i)
            {
                if (boxScene[i] == activeSceneIndex)
                {
                    activeBoxIndex = i;
                }
            }
            Debug.LogFormat("Before : activeSceneIndex = {0}", activeSceneIndex);
        }

        leftArrow = Input.GetKeyDown(KeyCode.LeftArrow);
        rightArrow = Input.GetKeyDown(KeyCode.RightArrow);
        upArrow = Input.GetKeyDown(KeyCode.UpArrow);
        downArrow = Input.GetKeyDown(KeyCode.DownArrow);

        if (leftArrow && activeBoxIndex == 1 && boxScene[0] == 3)
        {
            Rect tmp = sceneCam[activeSceneIndex].rect;
            sceneCam[activeSceneIndex].rect = sceneCam[boxScene[0]].rect;
            sceneCam[boxScene[0]].rect = tmp;
            boxScene[0] = activeSceneIndex;
            boxScene[activeBoxIndex] = 3;
            if (activeBoxIndex == playerBoxIndex)
            {
                playerBoxIndex = 0;
            }
            activeBoxIndex = 0;
        }
        else if (leftArrow && activeBoxIndex == 3 && boxScene[2] == 3)
        {
            Rect tmp = sceneCam[activeSceneIndex].rect;
            sceneCam[activeSceneIndex].rect = sceneCam[boxScene[2]].rect;
            sceneCam[boxScene[2]].rect = tmp;
            boxScene[2] = activeSceneIndex;
            boxScene[activeBoxIndex] = 3;
            if (activeBoxIndex == playerBoxIndex)
            {
                playerBoxIndex = 2;
            }
            activeBoxIndex = 2;
        }
        else if (rightArrow && activeBoxIndex == 0 && boxScene[1] == 3)
        {
            Rect tmp = sceneCam[activeSceneIndex].rect;
            sceneCam[activeSceneIndex].rect = sceneCam[boxScene[1]].rect;
            sceneCam[boxScene[1]].rect = tmp;
            boxScene[1] = activeSceneIndex;
            boxScene[activeBoxIndex] = 3;
            if (activeBoxIndex == playerBoxIndex)
            {
                playerBoxIndex = 1;
            }
            activeBoxIndex = 1;
        }
        else if (rightArrow && activeBoxIndex == 2 && boxScene[3] == 3)
        {
            Rect tmp = sceneCam[activeSceneIndex].rect;
            sceneCam[activeSceneIndex].rect = sceneCam[boxScene[3]].rect;
            sceneCam[boxScene[3]].rect = tmp;
            boxScene[3] = activeSceneIndex;
            boxScene[activeBoxIndex] = 3;
            if (activeBoxIndex == playerBoxIndex)
            {
                playerBoxIndex = 3;
            }
            activeBoxIndex = 3;
        }
        else if (upArrow && activeBoxIndex == 2 && boxScene[0] == 3)
        {
            Rect tmp = sceneCam[activeSceneIndex].rect;
            sceneCam[activeSceneIndex].rect = sceneCam[boxScene[0]].rect;
            sceneCam[boxScene[0]].rect = tmp;
            boxScene[0] = activeSceneIndex;
            boxScene[activeBoxIndex] = 3;
            if (activeBoxIndex == playerBoxIndex)
            {
                playerBoxIndex = 0;
            }
            activeBoxIndex = 0;
        }
        else if (upArrow && activeBoxIndex == 3 && boxScene[1] == 3)
        {
            Rect tmp = sceneCam[activeSceneIndex].rect;
            sceneCam[activeSceneIndex].rect = sceneCam[boxScene[1]].rect;
            sceneCam[boxScene[1]].rect = tmp;
            boxScene[1] = activeSceneIndex;
            boxScene[activeBoxIndex] = 3;
            if (activeBoxIndex == playerBoxIndex)
            {
                playerBoxIndex = 1;
            }
            activeBoxIndex = 1;
        }
        else if (downArrow && activeBoxIndex == 0 && boxScene[2] == 3)
        {
            Rect tmp = sceneCam[activeSceneIndex].rect;
            sceneCam[activeSceneIndex].rect = sceneCam[boxScene[2]].rect;
            sceneCam[boxScene[2]].rect = tmp;
            boxScene[2] = activeSceneIndex;
            boxScene[activeBoxIndex] = 3;
            if (activeBoxIndex == playerBoxIndex)
            {
                playerBoxIndex = 2;
            }
            activeBoxIndex = 2;
        }
        else if (downArrow && activeBoxIndex == 1 && boxScene[3] == 3)
        {
            Rect tmp = sceneCam[activeSceneIndex].rect;
            sceneCam[activeSceneIndex].rect = sceneCam[boxScene[3]].rect;
            sceneCam[boxScene[3]].rect = tmp;
            boxScene[3] = activeSceneIndex;
            boxScene[activeBoxIndex] = 3;
            if (activeBoxIndex == playerBoxIndex)
            {
                playerBoxIndex = 3;
            }
            activeBoxIndex = 3;
        }


        /// Player warp
        BoxCollider2D playerCollider, curSceneCollider, nextSceneCollider;

        playerCollider = player.GetComponent<BoxCollider2D>();
        float playerX = player.transform.position.x;
        float playerW = playerCollider.bounds.size.x / 2;
        Vector3 curRelativePos = cameraFrame[playerIndex].transform.position - player.transform.position;

        GameObject curChildObj = cameraFrame[playerIndex].transform.GetChild(0).gameObject;
        curSceneCollider = curChildObj.GetComponent<BoxCollider2D>();
        float sceneX = cameraFrame[playerIndex].transform.position.x;
        float sceneW = curSceneCollider.bounds.size.x / 2;

        if (playerX - playerW > sceneX + sceneW)
        {
            if (playerBoxIndex == 0 && boxScene[1] != 3)
            {
                player.transform.parent = cameraFrame[boxScene[1]].transform;
                GameObject nextChildObj = cameraFrame[boxScene[1]].transform.GetChild(0).gameObject;
                nextSceneCollider = nextChildObj.GetComponent<BoxCollider2D>();
                float nextSceneX = cameraFrame[boxScene[1]].transform.position.x;
                float nextSceneY = cameraFrame[boxScene[1]].transform.position.y;

                float nextSceneW = nextSceneCollider.bounds.size.x / 2;
                float nextPlayerX = nextSceneX - nextSceneW;
                float nextPlayerY = nextSceneY - curRelativePos.y;
                player.transform.position = new Vector3(nextPlayerX, nextPlayerY, player.transform.position.z);

                playerIndex = boxScene[1];
                playerBoxIndex = 1;
            }
            else if (playerBoxIndex == 2 && boxScene[3] != 3)
            {
                player.transform.parent = cameraFrame[boxScene[3]].transform;
                GameObject nextChildObj = cameraFrame[boxScene[3]].transform.GetChild(0).gameObject;
                nextSceneCollider = nextChildObj.GetComponent<BoxCollider2D>();
                float nextSceneX = cameraFrame[boxScene[3]].transform.position.x;
                float nextSceneY = cameraFrame[boxScene[3]].transform.position.y;

                float nextSceneW = nextSceneCollider.bounds.size.x / 2;
                float nextPlayerX = nextSceneX - nextSceneW;
                float nextPlayerY = nextSceneY - curRelativePos.y;
                player.transform.position = new Vector3(nextPlayerX, nextPlayerY, player.transform.position.z);

                playerIndex = boxScene[3];
                playerBoxIndex = 3;
            }
        }
        else if (playerX + playerW < sceneX - sceneW)
        {
            if (playerBoxIndex == 1 && boxScene[0] != 3)
            {
                player.transform.parent = cameraFrame[boxScene[0]].transform;
                GameObject nextChildObj = cameraFrame[boxScene[0]].transform.GetChild(0).gameObject;
                nextSceneCollider = nextChildObj.GetComponent<BoxCollider2D>();
                float nextSceneX = cameraFrame[boxScene[0]].transform.position.x;
                float nextSceneY = cameraFrame[boxScene[0]].transform.position.y;

                float nextSceneW = nextSceneCollider.bounds.size.x / 2;
                float nextPlayerX = nextSceneX + nextSceneW;
                float nextPlayerY = nextSceneY - curRelativePos.y;
                player.transform.position = new Vector3(nextPlayerX, nextPlayerY, player.transform.position.z);

                playerIndex = boxScene[0];
                playerBoxIndex = 0;
            }
            else if (playerBoxIndex == 3 && boxScene[2] != 3)
            {
                player.transform.parent = cameraFrame[boxScene[2]].transform;
                GameObject nextChildObj = cameraFrame[boxScene[2]].transform.GetChild(0).gameObject;
                nextSceneCollider = nextChildObj.GetComponent<BoxCollider2D>();
                float nextSceneX = cameraFrame[boxScene[2]].transform.position.x;
                float nextSceneY = cameraFrame[boxScene[2]].transform.position.y;

                float nextSceneW = nextSceneCollider.bounds.size.x / 2;
                float nextPlayerX = nextSceneX + nextSceneW;
                float nextPlayerY = nextSceneY - curRelativePos.y;
                player.transform.position = new Vector3(nextPlayerX, nextPlayerY, player.transform.position.z);

                playerIndex = boxScene[2];
                playerBoxIndex = 2;
            }
        }

        if (playerX + playerW > sceneX + sceneW)
        {
            if (playerBoxIndex == 1 || playerBoxIndex == 3 ||
                playerBoxIndex == 0 && boxScene[1] == 3 ||
                playerBoxIndex == 2 && boxScene[3] == 3)
            {
                player.transform.position -= new Vector3(0.5f, 0.0f, 0.0f);
            }
        }
        else if (playerX - playerW < sceneX - sceneW)
        {
            if (playerBoxIndex == 0 || playerBoxIndex == 2 ||
                playerBoxIndex == 1 && boxScene[0] == 3 ||
                playerBoxIndex == 3 && boxScene[2] == 3)
            {
                player.transform.position += new Vector3(0.5f, 0.0f, 0.0f);
            }
        }
        //Scene activeScene = SceneManager.GetSceneByName("ActiveScene");
        //SceneManager.MoveGameObjectToScene(MoveSceneWorld, ActiveScene);
    }
}