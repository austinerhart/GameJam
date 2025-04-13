using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MMenu : MonoBehaviour
{

    [SerializeField] private GameObject Start;
    [SerializeField] private GameObject Settings;
    [SerializeField] private GameObject Quit;
    [SerializeField] private GameObject Back;
    [SerializeField] private Slider SFXMainVolume;


    public void PressedStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PressedSettings()
    {
        Start.SetActive(false);
        Quit.SetActive(false);
        Settings.SetActive(false);
        Back.SetActive(true);
    }

    public void PressedBack()
    {
        Start.SetActive(true);
        Quit.SetActive(true);
        Settings.SetActive(true);
        Back.SetActive(false);
    }

    public void PressedExit()
    {
        Application.Quit();
    }

}
