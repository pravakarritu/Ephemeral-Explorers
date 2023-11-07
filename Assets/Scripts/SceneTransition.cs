using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad = "LevelComplete"; // Name of the scene you want to load
    private string m_curLevel, m_nextLevel;

    public void SetLevels(string curLevel, string nextLevel)
    {
        m_curLevel = curLevel;
        m_nextLevel = nextLevel;
    }

    public void LoadScene()
    {

        // // Add delay of falling, before gameover
        // if (sceneToLoad == "GameOver")
        // {
        //     // Wait for 2 seconds before loading the GameOver scene
        //     Invoke("DelayedLoadScene", 1.5f);
        // }
        // else
        // {
        // Adds a method called transitSceneEnv as a listener to the SceneManager.sceneLoaded event 
        // This event is triggered when a new scene is loaded and allows the script to perform actions 
        // after the scene has finished loading.
        SceneManager.sceneLoaded += transitSceneEnv;
        // When the new scene is loaded, the transitSceneEnv method is called
        SceneManager.LoadScene(sceneToLoad);
        // }
    }

    private void DelayedLoadScene()
    {
        // Adds a method called transitSceneEnv as a listener to the SceneManager.sceneLoaded event
        SceneManager.sceneLoaded += transitSceneEnv;

        // Load the GameOver scene
        SceneManager.LoadScene(sceneToLoad);
    }

    private void transitSceneEnv(Scene next, LoadSceneMode mode)
    {
        SceneTransition st = GameObject.FindWithTag("Player").GetComponent<SceneTransition>();
        st.m_curLevel = m_curLevel;
        st.m_nextLevel = m_nextLevel;
        // Removes the transitSceneEnv method from the SceneManager.sceneLoaded event
        SceneManager.sceneLoaded -= transitSceneEnv;
    }

    public void Retry()
    {
        SceneTransition st = GameObject.FindWithTag("Player").GetComponent<SceneTransition>();
        SceneManager.LoadScene(st.m_curLevel);
    }

    public void Next()
    {
        SceneTransition st = GameObject.FindWithTag("Player").GetComponent<SceneTransition>();
        SceneManager.LoadScene(st.m_nextLevel);
    }
}