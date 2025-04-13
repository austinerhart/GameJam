using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class DroneCamera : MonoBehaviour
{
    public GameObject drone;
    public Vector2 increment = new Vector2(1f, 0f);
    public float leftbound;
    public float rightbound;
    public bool left;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {   
        if(!left){
            transform.Translate(-increment * Time.deltaTime);
            if(drone.transform.position.x < leftbound){
                transform.Rotate(0, 180, 0);
                left = true;
            }
        }
        if(left){
            transform.Translate(-increment * Time.deltaTime);
            if(drone.transform.position.x > rightbound){
                transform.Rotate(0,-180,0);
                left = false;
            }
        }

    }
}

    //public IEnumerator Movement(){
        
    //}

