using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateCamera : MonoBehaviour
{
    public GameObject vision;
    public Rotate rotate;
    public LeftSideRotate leftSideRotate;
    public float savedSpeed;
    public int delay;
    // Start is called before the first frame update
    void Start()
    {
        if(rotate != null){
            savedSpeed = rotate.rotationSpeed;
            Debug.Log(rotate.rotationSpeed);
        }
        if(leftSideRotate != null){
            savedSpeed = leftSideRotate.rotationSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player")){
            if(Input.GetKey(KeyCode.E)){
                if(rotate != null){
                    rotate.rotationSpeed = 0;
                }
                if(leftSideRotate != null){
                    leftSideRotate.rotationSpeed = 0;
                }
                if (GetComponent<AudioSource>())
                    GetComponent<AudioSource>().Play();
                vision.SetActive(false);
                StartCoroutine(Delay());
            }
        }
    }

    public IEnumerator Delay(){
        GameObject camera = vision.transform.parent.gameObject;
        if(camera.GetComponent<AudioSource>())
            camera.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(delay);
        if(rotate != null){
                    rotate.rotationSpeed = savedSpeed;
                }
                if(leftSideRotate != null){
                    leftSideRotate.rotationSpeed = savedSpeed;
                }
                vision.SetActive(true);
                if (vision.GetComponent<AudioSource>())
                    vision.GetComponent<AudioSource>().Play();
    }
}
