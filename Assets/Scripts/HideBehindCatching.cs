using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideBehindCatching : MonoBehaviour
{

    public Catching vision;
    Renderer render;
    public GameObject player;
    int oldLayer;
    public int newLayer;
    void Start()
    {
        player = GameObject.Find("Player");
        render = player.GetComponent<Renderer>();
        oldLayer = 8;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Player")){
            if(Input.GetKey(KeyCode.E)){
                vision.hidden = true;
                render.sortingOrder = newLayer;
            }
            if(Input.GetKey(KeyCode.W) ||  Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
                vision.hidden = false;
                render.sortingOrder = oldLayer;
            }
        }
    }
}
