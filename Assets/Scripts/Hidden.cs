using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class Hidden : MonoBehaviour
{
    public Catching vision;
    public GameObject player;
    Renderer render;
    public int newLayer;
    public int oldLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        render = player.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Player")){
            render.sortingOrder = newLayer;
            vision.hidden = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Player")){
            render.sortingOrder = oldLayer;
            vision.hidden = false;
        }
    }
}