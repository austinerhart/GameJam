using UnityEngine;
using System.Collections;

public class Caught : MonoBehaviour
{
    public bool hidden;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Player")){
            if(!hidden){
            gameOver();
            }
        }
    }

    public void gameOver(){
        Debug.Log("You have been Re-educated");
    }
}
