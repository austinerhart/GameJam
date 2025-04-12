using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideBehindInteract : MonoBehaviour
{

    public Caught player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Player")){
            if(Input.GetKey(KeyCode.E)){
            player.hidden = true;
            }
            if(Input.GetKey(KeyCode.W) ||  Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)){
                player.hidden = false;
            }
        }
    }
}
