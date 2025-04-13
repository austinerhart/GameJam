using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] GameObject PauseMenu;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0f)
            {

                PauseMenu.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                PauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
        }

        if(PlayerManager.Instance.times_caught == 5)
        {
            SceneManager.LoadScene("Lose");
        }
    }
}
