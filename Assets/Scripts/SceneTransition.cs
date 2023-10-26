using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad = "LevelComplete"; // Name of the scene you want to load
    private string m_curLevel, m_nextLevel;

    public void SetLevels(string curLevel, string nextLevel) {
        m_curLevel = curLevel;
        m_nextLevel = nextLevel;
    }

    public void LoadScene()
    {
        SceneManager.sceneLoaded += transitSceneEnv;
        SceneManager.LoadScene(sceneToLoad);
    }

    private void transitSceneEnv(Scene next, LoadSceneMode mode) {
        SceneTransition st = GameObject.FindWithTag("Player").GetComponent<SceneTransition>();
        st.m_curLevel = m_curLevel;
        st.m_nextLevel = m_nextLevel;
        SceneManager.sceneLoaded -= transitSceneEnv;
    }

    public void Retry() {
        SceneTransition st = GameObject.FindWithTag("Player").GetComponent<SceneTransition>();
        SceneManager.LoadScene(st.m_curLevel);
    }

    public void Next() {
        SceneTransition st = GameObject.FindWithTag("Player").GetComponent<SceneTransition>();
        SceneManager.LoadScene(st.m_nextLevel);
    }
}