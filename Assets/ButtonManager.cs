using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PressedStart(){
        Debug.Log("pressed");
    }

    public void PressedExit(){
        Debug.Log("Bye");
        Application.Quit();
    }
        
}
