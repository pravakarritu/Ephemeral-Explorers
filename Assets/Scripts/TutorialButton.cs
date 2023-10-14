using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject wholeWorld;

    public GameObject tutorial;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void unActiveWorld(){

        wholeWorld.SetActive(false);
        tutorial.SetActive(true);
        
    }

    public void ActiveWorld(){
        wholeWorld.SetActive(true);
        tutorial.SetActive(false);
        
    }
}
