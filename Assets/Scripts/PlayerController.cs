using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private float horizontalInput;
    private float forwardInput;
    public float speed = 20.0f;
    public GameObject character;
    public Rigidbody2D rb;
    public float jumpAmount = 10;
        public float jumpHeight;
    public float jumpStrength = 10f;
    bool Hitted_Level1=false;
    bool Hitted_Level2=false;
    bool collected_key = false;



    void Start()
    {
        rb = character.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float movespeed = 1.0f;
        transform.position = new Vector2(transform.position.x + movespeed * Time.deltaTime, transform.position.y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
        }
        AudioListener[] aL = FindObjectsOfType<AudioListener>();
        for (int i = 0; i < aL.Length; i++)
        {
            //Destroy if AudioListener is not on the MainCamera
            if (!aL[i].CompareTag("MainCamera"))
            {
                DestroyImmediate(aL[i]);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Door1")  && SceneManager.GetActiveScene().name== "Level1Platform1" && (Hitted_Level2 == false))
        {
            Hitted_Level2= true;
            Debug.Log(GameObject.Find("Door1").transform.position);
            GameObject.Find("Door1").transform.position = new Vector2(9.62f, 3);
            Application.Quit();
            //SceneManager.LoadScene("Level1Platform1", LoadSceneMode.Additive);
            //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Level1Platform1"));

        }
        if (col.gameObject.CompareTag("Door") && SceneManager.GetActiveScene().name == "Start Menu" && Hitted_Level1==false)
        {
            Hitted_Level1 = true;
            GameObject.Find("Door").transform.position = new Vector2(8, 0.5f);

           // StartCoroutine(LoadYourAsyncScene());
            SceneManager.LoadScene("Level1Platform1", LoadSceneMode.Additive);
           // AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Level1Platform1", LoadSceneMode.Additive);
            //while (!asyncLoad.isDone)
            //{
              //  yield return null;
            //}
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Level1Platform1"));
            GameObject.Find("Door").transform.position = new Vector2(8, -3);

        }
        /* IEnumerator LoadYourAsyncScene()
         {


             AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Level1Platform1", LoadSceneMode.Additive);

             while (!asyncLoad.isDone)
             {
                 yield return null;
             }
             SceneManager.SetActiveScene(SceneManager.GetSceneByName("Level1Platform1"));

         }*/

        if (col.gameObject.CompareTag("Key"))
        {
            collected_key=true;
            Debug.Log("Player has collected key");
            Destroy(col.gameObject);
        }

    }
}
