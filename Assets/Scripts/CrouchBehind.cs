using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrouchBehind : MonoBehaviour
{

    public Caught vision;
    public GameObject player;
    Renderer render;
    int oldLayer;
    public int newLayer;
    void Start()
    {
        player = GameObject.Find("Player");
        render = player.GetComponent<Renderer>();
        oldLayer = render.sortingOrder;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Player")){
            if(Input.GetKey(KeyCode.S)){    
                vision.hidden = true;
                render.sortingOrder = newLayer;
            }
            if(!Input.GetKey(KeyCode.S) && (Input.GetKey(KeyCode.W) ||  Input.GetKey(KeyCode.A)  || Input.GetKey(KeyCode.D))){
                render.sortingOrder = oldLayer;
                vision.hidden = false;
            }
        }
    }
}
