using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEngine.SceneManagement;

public class BoxManager : MonoBehaviour
{
    // Initialize the below values in unity editor
    public int boxNum, boxRow, boxCol, emptyBoxIndex;
    public GameObject[] box, frame;
    private int[][] boxMap; // index: position index , value: index of box located at the position

    private GameObject player;
    PlayerCtr playerCtr;

    private int playerMapIndex, emptyMapIndex;

    // private Color32 normalColor = new Color32(0, 0, 0, 0);
    private Color32 normalColor = new Color32(255, 255, 255, 255);
    public Color32 activeColor;

    public TextMeshProUGUI player_movement_control, screen_control, screen_select;


    // Number of Box Movements - Analytics
    public int numberOfBoxMovements = 0;

    void Start()
    {

        // numberOfBoxMovements = 0;
        // Initialize memory for boxMap row
        Array.Resize(ref boxMap, boxRow);
        for (int i = 0; i < boxRow; ++i)
        {
            // Initialize memory for boxMap col
            Array.Resize(ref boxMap[i], boxCol);
            for (int j = 0; j < boxCol; ++j)
            {
                // Assign unique IDs to boxMap positions
                int index = i * boxCol + j;
                boxMap[i][j] = index;
            }
        }
        // Find player object
        player = GameObject.FindWithTag("Player");
        // Get PlayerCtr script
        playerCtr = player.GetComponent<PlayerCtr>();
        // Player is always in first box
        playerMapIndex = 0;
        // Get the empty box value set in unity editor
        emptyMapIndex = emptyBoxIndex;
    }

    void Update()
    {
        // Box controls work only when we are zoomed out
        if (!playerCtr.IsZoomIn())
        {
            // Check if user presses right shift key
            bool rotate = Input.GetKeyDown(KeyCode.RightShift);
            // Rotate the box and player if user presses right shift key
            if (rotate)
            {
                // Get row and col of player
                int r_row = playerMapIndex / boxCol;
                int r_col = playerMapIndex - boxCol * r_row;
                // Rotate the box and player
                box[boxMap[r_row][r_col]].transform.Rotate(Vector3.forward * -90);
                player.transform.Rotate(Vector3.forward * 90);
            }

            // Get box movement input
            bool left = Input.GetKeyDown(KeyCode.LeftArrow);
            bool right = Input.GetKeyDown(KeyCode.RightArrow);
            bool up = Input.GetKeyDown(KeyCode.UpArrow);
            bool down = Input.GetKeyDown(KeyCode.DownArrow);

            // Get the row and col of empty box
            int row = emptyMapIndex / boxCol;
            int col = emptyMapIndex - boxCol * row;
            // If left key is pressed and empty box is not in the rightmost column
            if (left && col != boxCol - 1)
            {
                // Get the ID of box that is to the right of empty box
                int rightBoxID = boxMap[row][col + 1];
                // Get the position of right box
                Vector3 tmp = box[rightBoxID].transform.position;
                // Move the empty box to the right box position
                box[rightBoxID].transform.position = box[emptyBoxIndex].transform.position;
                // Move the right box to the empty box position
                box[emptyBoxIndex].transform.position = tmp;

                // Update the ID of empty box in boxMap
                boxMap[row][col + 1] = emptyBoxIndex;
                // Update the ID of right box in boxMap
                boxMap[row][col] = rightBoxID;
                // Check if the player was in the right box
                if (emptyMapIndex + 1 == playerMapIndex)
                {
                    // Update the playerMapIndex
                    playerMapIndex -= 1;
                }
                // Update the emptyMapIndex to the right box
                emptyMapIndex += 1;

                Debug.Log(++numberOfBoxMovements);
            }
            else if (right && col != 0)
            {
                int leftBoxID = boxMap[row][col - 1];
                Vector3 tmp = box[leftBoxID].transform.position;
                box[leftBoxID].transform.position = box[emptyBoxIndex].transform.position;
                box[emptyBoxIndex].transform.position = tmp;

                boxMap[row][col - 1] = emptyBoxIndex;
                boxMap[row][col] = leftBoxID;
                if (emptyMapIndex - 1 == playerMapIndex)
                {
                    playerMapIndex += 1;
                }
                emptyMapIndex -= 1;
                Debug.Log(++numberOfBoxMovements);
            }
            else if (down && row != 0)
            {
                int upBoxID = boxMap[row - 1][col];
                Vector3 tmp = box[upBoxID].transform.position;
                box[upBoxID].transform.position = box[emptyBoxIndex].transform.position;
                box[emptyBoxIndex].transform.position = tmp;

                boxMap[row - 1][col] = emptyBoxIndex;
                boxMap[row][col] = upBoxID;
                if (emptyMapIndex - boxCol == playerMapIndex)
                {
                    playerMapIndex += boxCol;
                }
                emptyMapIndex -= boxCol;
                Debug.Log(++numberOfBoxMovements);
            }
            else if (up && row != boxRow - 1)
            {
                int downBoxID = boxMap[row + 1][col];
                Vector3 tmp = box[downBoxID].transform.position;
                box[downBoxID].transform.position = box[emptyBoxIndex].transform.position;
                box[emptyBoxIndex].transform.position = tmp;

                boxMap[row + 1][col] = emptyBoxIndex;
                boxMap[row][col] = downBoxID;
                if (emptyMapIndex + boxCol == playerMapIndex)
                {
                    playerMapIndex -= boxCol;
                }
                emptyMapIndex += boxCol;
                Debug.Log(++numberOfBoxMovements);
            }
        }


        // Get the x and y position of player
        float playerX = player.transform.position.x;
        float playerY = player.transform.position.y;
        // Get the half width and half height of player
        float playerW = player.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        float playerH = player.GetComponent<SpriteRenderer>().bounds.size.y / 2;

        // Get the x and y position of parent box
        GameObject parentBox = player.transform.parent.gameObject;
        float parentX = parentBox.transform.position.x;
        float parentY = parentBox.transform.position.y;
        // Get the half width and half height of parent box
        float parentW = parentBox.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        float parentH = parentBox.GetComponent<SpriteRenderer>().bounds.size.y / 2;

        // Get the row and col of player
        int p_row = playerMapIndex / boxCol;
        int p_col = playerMapIndex - boxCol * p_row;
        // Check if the player is going from right box to left box
        if (playerX + playerW < parentX - parentW && p_col != 0)
        {
            // Get the ID of left box
            int leftBoxID = boxMap[p_row][p_col - 1];
            // Check if the left box is not empty box
            if (leftBoxID != emptyBoxIndex)
            {
                // Set the player as child of left box
                player.transform.parent = box[leftBoxID].transform;
                // Update the playerMapIndex
                playerMapIndex -= 1;
            }
        }
        // Check if the player is going from left box to right box
        else if (playerX - playerW > parentX + parentW && p_col != boxCol - 1)
        {
            // Get the ID of right box
            int rightBoxID = boxMap[p_row][p_col + 1];
            // Check if the right box is not empty box
            if (rightBoxID != emptyBoxIndex)
            {
                // Set the player as child of right box
                player.transform.parent = box[rightBoxID].transform;
                // Update the playerMapIndex
                playerMapIndex += 1;
            }
        }
        // Check if the player is going from down box to up box
        else if (playerY - playerH > parentY + parentH && p_row != 0)
        {
            int upBoxID = boxMap[p_row - 1][p_col];
            if (upBoxID != emptyBoxIndex)
            {
                player.transform.parent = box[upBoxID].transform;
                playerMapIndex -= boxCol;
            }
        }
        // Check if the player is going from up box to down box
        else if (playerY + playerH < parentY - parentH && p_row != boxRow - 1)
        {
            int downBoxID = boxMap[p_row + 1][p_col];
            if (downBoxID != emptyBoxIndex)
            {
                player.transform.parent = box[downBoxID].transform;
                playerMapIndex += boxCol;
            }
        }

        // Check if player has gone out of bounds
        if (playerX + playerW > parentX + parentW)
        {
            if (p_col == boxCol - 1 || boxMap[p_row][p_col + 1] == emptyBoxIndex)
            {
                SceneTransition st = GetComponent<SceneTransition>();
                st.SetLevels(playerCtr.curLevel, playerCtr.nextLevel);
                st.LoadScene();
            }
        }
        else if (playerX - playerW < parentX - parentW)
        {
            if (p_col == 0 || boxMap[p_row][p_col - 1] == emptyBoxIndex)
            {
                SceneTransition st = GetComponent<SceneTransition>();
                st.SetLevels(playerCtr.curLevel, playerCtr.nextLevel);
                st.LoadScene();
            }
        }
        else if (playerY + playerH > parentY + parentH)
        {
            if (p_row == 0 || boxMap[p_row - 1][p_col] == emptyBoxIndex)
            {
                SceneTransition st = GetComponent<SceneTransition>();
                st.SetLevels(playerCtr.curLevel, playerCtr.nextLevel);
                st.LoadScene();
            }
        }
        else if (playerY - playerH < parentY - parentH)
        {
            if (p_row == boxRow - 1 || boxMap[p_row + 1][p_col] == emptyBoxIndex)
            {
                SceneTransition st = GetComponent<SceneTransition>();
                st.SetLevels(playerCtr.curLevel, playerCtr.nextLevel);
                st.LoadScene();
            }
        }
    }

    public int sendNumberOfBoxMovements (){
        return numberOfBoxMovements;
    }
}
