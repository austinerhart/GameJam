using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterTower : MonoBehaviour
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
        if(collision.gameObject.CompareTag("PlayerEnd")){
            if(Input.GetKey(KeyCode.E)){
                SceneManager.LoadSceneAsync("RadioTower");
            }
        } 
   }
   public void OnTriggerStay2D(Collider2D collision){
        if(collision.gameObject.CompareTag("PlayerEnd")){
            if(Input.GetKey(KeyCode.E)){
                SceneManager.LoadSceneAsync("RadioTower");
            }
        }
        
   }
}
