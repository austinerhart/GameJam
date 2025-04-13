using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCheckPoint : MonoBehaviour
{
    GameObject spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = GameObject.Find("playerSpawnPoint");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player")){
            spawnPoint.transform.position = transform.position;
        }    
    }
}
