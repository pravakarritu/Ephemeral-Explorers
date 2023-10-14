using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad; // Name of the scene you want to load

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}