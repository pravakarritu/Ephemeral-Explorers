using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoxManager : MonoBehaviour
{
    public int boxNum = 4, boxRow = 2, boxCol = 2, emptyBoxIndex = 3;
    public GameObject[] box, frame;
    private int[][] boxMap; // index: position index , value: index of box located at the position
    private GameObject player;

    private int activeBoxIndex, playerMapIndex, activeMapIndex;

    // private Color32 normalColor = new Color32(0, 0, 0, 0);
    private Color32 normalColor = new Color32(255, 255, 255, 255);
    public Color32 activeColor;

    public TextMeshProUGUI player_movement_control, screen_control, screen_select;

    void Start()
    {
        Array.Resize(ref boxMap, boxRow);
        for (int i=0; i < boxRow; ++i) {
            Array.Resize(ref boxMap[i], boxCol);
            for (int j=0; j < boxCol; ++j) {
                int index = i * boxCol + j;
                boxMap[i][j] = index;
            }
        }
        player = GameObject.FindWithTag("Player");
        activeBoxIndex = playerMapIndex = 0;
        for (int i = 0; i < 4; ++i) {
            GameObject childObj = frame[activeBoxIndex].transform.GetChild(i).gameObject;
            childObj.GetComponent<Renderer>().material.color = activeColor;
        }
    }

    void Update()
    {
        // Select Box to move
        bool changeBox = Input.GetKeyDown(KeyCode.Tab);
        if (changeBox)
        {
            for (int i = 0; i < 4; ++i) {
                GameObject childObj = frame[activeBoxIndex].transform.GetChild(i).gameObject;
                childObj.GetComponent<Renderer>().material.color = normalColor;
            }
            ++activeMapIndex;
            activeMapIndex %= boxNum;
            int mapRow = activeMapIndex / boxCol;
            int mapCol = activeMapIndex - boxCol*mapRow;
            while (boxMap[mapRow][mapCol] == emptyBoxIndex) {
                ++activeMapIndex;
                activeMapIndex %= boxNum;
                mapRow = activeMapIndex / boxCol;
                mapCol = activeMapIndex - boxCol*mapRow;
            }
            activeBoxIndex = boxMap[mapRow][mapCol];
            for (int i = 0; i < 4; ++i) {
                GameObject childObj = frame[activeBoxIndex].transform.GetChild(i).gameObject;
                childObj.GetComponent<Renderer>().material.color = activeColor;
            }
        }

        bool rotate = Input.GetKeyDown(KeyCode.RightShift);
        if (rotate) {
            for (int i=0; i < box[activeBoxIndex].transform.childCount; ++i) {
                GameObject childObj = box[activeBoxIndex].transform.GetChild(i).gameObject;
                if (childObj.name == "GameWorld") {
                    childObj.transform.Rotate(Vector3.forward * -90);
                }
            }
        }

        // Box movement
        bool left = Input.GetKeyDown(KeyCode.A);
        bool right = Input.GetKeyDown(KeyCode.D);
        bool up = Input.GetKeyDown(KeyCode.W);
        bool down = Input.GetKeyDown(KeyCode.S);

        int row = activeMapIndex / boxCol;
        int col = activeMapIndex - boxCol*row;
        if (left && col != 0) {
            int leftBoxID = boxMap[row][col-1];
            if (leftBoxID == emptyBoxIndex) {
                Vector3 tmp = box[activeBoxIndex].transform.position;
                box[activeBoxIndex].transform.position = box[emptyBoxIndex].transform.position;
                box[emptyBoxIndex].transform.position = tmp;

                boxMap[row][col-1] = activeBoxIndex;
                boxMap[row][col] = emptyBoxIndex;
                if (activeMapIndex == playerMapIndex) {
                    playerMapIndex -= 1;
                }
                activeMapIndex -= 1;
            }
        }
        else if (right && col != boxCol-1) {
            int rightBoxID = boxMap[row][col+1];
            if (rightBoxID == emptyBoxIndex) {
                Vector3 tmp = box[activeBoxIndex].transform.position;
                box[activeBoxIndex].transform.position = box[emptyBoxIndex].transform.position;
                box[emptyBoxIndex].transform.position = tmp;

                boxMap[row][col+1] = activeBoxIndex;
                boxMap[row][col] = emptyBoxIndex;
                if (activeMapIndex == playerMapIndex) {
                    playerMapIndex += 1;
                }
                activeMapIndex += 1;
            }
        }
        else if (up && row != 0) {
            int upBoxID = boxMap[row-1][col];
            if (upBoxID == emptyBoxIndex) {
                Vector3 tmp = box[activeBoxIndex].transform.position;
                box[activeBoxIndex].transform.position = box[emptyBoxIndex].transform.position;
                box[emptyBoxIndex].transform.position = tmp;

                boxMap[row-1][col] = activeBoxIndex;
                boxMap[row][col] = emptyBoxIndex;
                if (activeMapIndex == playerMapIndex) {
                    playerMapIndex -= boxCol;
                }
                activeMapIndex -= boxCol;
            }
        }
        else if (down && row != boxRow-1) {
            int downBoxID = boxMap[row+1][col];
            if (downBoxID == emptyBoxIndex) {
                Vector3 tmp = box[activeBoxIndex].transform.position;
                box[activeBoxIndex].transform.position = box[emptyBoxIndex].transform.position;
                box[emptyBoxIndex].transform.position = tmp;

                boxMap[row+1][col] = activeBoxIndex;
                boxMap[row][col] = emptyBoxIndex;
                if (activeMapIndex == playerMapIndex) {
                    playerMapIndex += boxCol;
                }
                activeMapIndex += boxCol;
            }
        }

        // Player Movement
        float playerX = player.transform.position.x;
        float playerY = player.transform.position.y;
        float playerW = player.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        float playerH = player.GetComponent<SpriteRenderer>().bounds.size.y / 2;

        GameObject parentBox = player.transform.parent.gameObject;
        float parentX = parentBox.transform.position.x;
        float parentY = parentBox.transform.position.y;
        float parentW = parentBox.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        float parentH = parentBox.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        
        parentBox = player.transform.parent.gameObject;
        row = playerMapIndex / boxCol;
        col = playerMapIndex - boxCol*row;
        if (playerX + playerW < parentX - parentW && col != 0) {
            int leftBoxID = boxMap[row][col-1];
            if (leftBoxID != emptyBoxIndex) {
                player.transform.parent = box[leftBoxID].transform;
                playerMapIndex -= 1;
            }
        }
        else if (playerX - playerW > parentX + parentW && col != boxCol-1) {
            int rightBoxID = boxMap[row][col+1];
            if (rightBoxID != emptyBoxIndex) {
                player.transform.parent = box[rightBoxID].transform;
                playerMapIndex += 1;
            }
        }
        else if (playerY - playerH > parentY + parentH && row != 0) {
            int upBoxID = boxMap[row-1][col];
            if (upBoxID != emptyBoxIndex) {
                player.transform.parent = box[upBoxID].transform;
                playerMapIndex -= boxCol;
            }
        }
        else if (playerY + playerH < parentY - parentH && row != boxRow-1) {
            int downBoxID = boxMap[row+1][col];
            if (downBoxID != emptyBoxIndex) {
                player.transform.parent = box[downBoxID].transform;
                playerMapIndex += boxCol;
            }
        }

        if (playerX + playerW > parentX + parentW) {
            if (col == boxCol-1 || boxMap[row][col+1] == emptyBoxIndex) {
                player.transform.position -= new Vector3(0.8f, 0.0f, 0.0f);
            }
        }
        else if (playerX - playerW < parentX - parentW) {
            if (col == 0 || boxMap[row][col-1] == emptyBoxIndex) {
                player.transform.position += new Vector3(0.8f, 0.0f, 0.0f);
            }
        }
        else if (playerY + playerH > parentY + parentH) {
            if (row == 0 || boxMap[row-1][col] == emptyBoxIndex) {
                player.transform.position -= new Vector3(0.0f, 0.8f, 0.0f);
            }
        }
        else if (playerY + playerH < parentY - parentH) {
            if (row == boxRow-1 || boxMap[row+1][col] == emptyBoxIndex) {
                player.transform.position += new Vector3(0.0f, 0.8f, 0.0f);
            }
        }
    }
}
