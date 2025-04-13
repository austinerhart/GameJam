using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBox : MonoBehaviour
{
    public GameObject box;
    public BoxFall boxFall;
    public GameObject newParent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Drone")){
            Debug.Log("Passed Through");
            box.transform.SetParent(newParent.transform);
            boxFall.fall = true;
        }
    }
}
