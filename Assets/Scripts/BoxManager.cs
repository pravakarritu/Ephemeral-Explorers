using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEngine.SceneManagement;

public class BoxManager : MonoBehaviour
{
    public int boxNum = 4, boxRow = 2, boxCol = 2, emptyBoxIndex = 3;
    public GameObject[] box, frame;
    private int[][] boxMap; // index: position index , value: index of box located at the position

    private GameObject player;
    PlayerCtr playerCtr;

    private int playerMapIndex, emptyMapIndex;

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
        playerCtr = player.GetComponent<PlayerCtr>();
        playerMapIndex = 0;
        emptyMapIndex = emptyBoxIndex;
    }

    void Update()
    {
        if (!playerCtr.IsZoomIn()) {
            bool rotate = Input.GetKeyDown(KeyCode.RightShift);
            if (rotate) {
                int r_row = playerMapIndex / boxCol;
                int r_col = playerMapIndex - boxCol*r_row;
                box[boxMap[r_row][r_col]].transform.Rotate(Vector3.forward * -90);
                player.transform.Rotate(Vector3.forward * 90);
            }

            // Box movement
            bool left = Input.GetKeyDown(KeyCode.LeftArrow);
            bool right = Input.GetKeyDown(KeyCode.RightArrow);
            bool up = Input.GetKeyDown(KeyCode.UpArrow);
            bool down = Input.GetKeyDown(KeyCode.DownArrow);

            int row = emptyMapIndex / boxCol;
            int col = emptyMapIndex - boxCol * row;
            if (left && col != boxCol-1) {
                int rightBoxID = boxMap[row][col+1];
                Vector3 tmp = box[rightBoxID].transform.position;
                box[rightBoxID].transform.position = box[emptyBoxIndex].transform.position;
                box[emptyBoxIndex].transform.position = tmp;

                boxMap[row][col+1] = emptyBoxIndex;
                boxMap[row][col] = rightBoxID;
                if (emptyMapIndex+1 == playerMapIndex) {
                    playerMapIndex -= 1;
                }
                emptyMapIndex += 1;
            }
            else if (right && col != 0) {
                int leftBoxID = boxMap[row][col-1];
                Vector3 tmp = box[leftBoxID].transform.position;
                box[leftBoxID].transform.position = box[emptyBoxIndex].transform.position;
                box[emptyBoxIndex].transform.position = tmp;

                boxMap[row][col-1] = emptyBoxIndex;
                boxMap[row][col] = leftBoxID;
                if (emptyMapIndex-1 == playerMapIndex) {
                    playerMapIndex += 1;
                }
                emptyMapIndex -= 1;
            }
            else if (down && row != 0) {
                int upBoxID = boxMap[row-1][col];
                Vector3 tmp = box[upBoxID].transform.position;
                box[upBoxID].transform.position = box[emptyBoxIndex].transform.position;
                box[emptyBoxIndex].transform.position = tmp;

                boxMap[row-1][col] = emptyBoxIndex;
                boxMap[row][col] = upBoxID;
                if (emptyMapIndex-boxCol == playerMapIndex) {
                    playerMapIndex += boxCol;
                }
                emptyMapIndex -= boxCol;
            }
            else if (up && row != boxRow-1) {
                int downBoxID = boxMap[row+1][col];
                Vector3 tmp = box[downBoxID].transform.position;
                box[downBoxID].transform.position = box[emptyBoxIndex].transform.position;
                box[emptyBoxIndex].transform.position = tmp;

                boxMap[row+1][col] = emptyBoxIndex;
                boxMap[row][col] = downBoxID;
                if (emptyMapIndex+boxCol == playerMapIndex) {
                    playerMapIndex -= boxCol;
                }
                emptyMapIndex += boxCol;
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
        int p_row = playerMapIndex / boxCol;
        int p_col = playerMapIndex - boxCol*p_row;
        if (playerX + playerW < parentX - parentW && p_col != 0) {
            int leftBoxID = boxMap[p_row][p_col-1];
            if (leftBoxID != emptyBoxIndex) {
                player.transform.parent = box[leftBoxID].transform;
                playerMapIndex -= 1;
            }
        }
        else if (playerX - playerW > parentX + parentW && p_col != boxCol-1) {
            int rightBoxID = boxMap[p_row][p_col+1];
            if (rightBoxID != emptyBoxIndex) {
                player.transform.parent = box[rightBoxID].transform;
                playerMapIndex += 1;
            }
        }
        else if (playerY - playerH > parentY + parentH && p_row != 0) {
            int upBoxID = boxMap[p_row-1][p_col];
            if (upBoxID != emptyBoxIndex) {
                player.transform.parent = box[upBoxID].transform;
                playerMapIndex -= boxCol;
            }
        }
        else if (playerY + playerH < parentY - parentH && p_row != boxRow-1) {
            int downBoxID = boxMap[p_row+1][p_col];
            if (downBoxID != emptyBoxIndex) {
                player.transform.parent = box[downBoxID].transform;
                playerMapIndex += boxCol;
            }
        }

        if (playerX + playerW > parentX + parentW) {
            if (p_col == boxCol-1 || boxMap[p_row][p_col+1] == emptyBoxIndex) {
                SceneTransition st = GetComponent<SceneTransition>();
                st.SetLevels(playerCtr.curLevel, playerCtr.nextLevel);
                st.LoadScene();
            }
        }
        else if (playerX - playerW < parentX - parentW) {
            if (p_col == 0 || boxMap[p_row][p_col-1] == emptyBoxIndex) {
                SceneTransition st = GetComponent<SceneTransition>();
                st.SetLevels(playerCtr.curLevel, playerCtr.nextLevel);
                st.LoadScene();
            }
        }
        else if (playerY + playerH > parentY + parentH) {
            if (p_row == 0 || boxMap[p_row-1][p_col] == emptyBoxIndex) {
                SceneTransition st = GetComponent<SceneTransition>();
                st.SetLevels(playerCtr.curLevel, playerCtr.nextLevel);
                st.LoadScene();
            }
        }
        else if (playerY - playerH < parentY - parentH) {
            if (p_row == boxRow-1 || boxMap[p_row+1][p_col] == emptyBoxIndex) {
                SceneTransition st = GetComponent<SceneTransition>();
                st.SetLevels(playerCtr.curLevel, playerCtr.nextLevel);
                st.LoadScene();
            }
        }
    }
}
