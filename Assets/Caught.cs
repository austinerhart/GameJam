using UnityEngine;
using System.Collections;

public class Caught : MonoBehaviour
{
    public bool hidden;
    public GameObject spawnPoint;
    public GameObject player; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPoint = GameObject.Find("playerSpawnPoint");
        player = GameObject.Find("Player");
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

    public void OnTriggerStay2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Player")){
            if(!hidden){
            gameOver();
            }
        }
    }

    public void gameOver(){
        player.transform.position = spawnPoint.transform.position;
        Debug.Log("You have been Re-educated");
    }
}
