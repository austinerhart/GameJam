using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject PauseMenuPanel;
    public void PressedPause()
    {
        PauseMenuPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void PressedunPause()
    {
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void PressedMainMenu()
    {
        SceneManager.LoadScene("StartScreen");
    }
}
