using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMenu : MonoBehaviour
{
    public void PressedStart()
    {
        Debug.Log("pressed");
    }

    public void PressedExit()
    {
        Debug.Log("Bye");
        Application.Quit();
    }

}
