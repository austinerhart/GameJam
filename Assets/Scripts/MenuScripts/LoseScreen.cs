using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour
{
    [SerializeField] private GameObject Replay;
    [SerializeField] private GameObject MainMenu;


    public void PressedReplay()
    {
        SceneManager.LoadScene("Level Design");
    }

    public void PressedMainMenu()
    {
        SceneManager.LoadScene("StartScreen");
    }
}
