using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player")){
            if(Input.GetKey(KeyCode.E)){
                SceneManager.LoadSceneAsync("VictoryScene");
            }
        }
    }
    public void OnTriggerStay2D(Collider2D collision){
       if(collision.gameObject.CompareTag("Player")){
            if(Input.GetKey(KeyCode.E)){
                SceneManager.LoadSceneAsync("VictoryScene");
            }
        } 
    }
}
