using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCameraControl : MonoBehaviour
{
    public CameraController cameraController;
    public GameObject cameraObject;
    // Start is called before the first frame update
    void Start()
    {
        cameraObject = GameObject.Find("Main Camera");
        cameraController = cameraObject.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player")){
            Debug.Log("STOP");
            cameraController.boundary = true;
        }
    }

    public void OnTriggerStay2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player")){
            Debug.Log("STOP");
            cameraController.boundary = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player")){
            cameraController.boundary = false;
        }
    }
}
