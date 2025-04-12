using UnityEngine;
using System.Collections;
using UnityEditor.Rendering;

public class Rotate : MonoBehaviour
{
    // Attach this script to the parent (RotationPivot)
public float rotationSpeed; 
public GameObject Eyes;
public Space space;
public bool left;
public bool right;
public int delay;

    void Start(){

    }
    void Update()
    {   
        if (!left){
            transform.Rotate(0,0,-rotationSpeed * Time.deltaTime, space);
            if (Eyes.transform.localEulerAngles.z < 20.78f)
                left = true;
        }
        if (left && !right){
            transform.Rotate(0,0,rotationSpeed * Time.deltaTime, space);
            if(Eyes.transform.localEulerAngles.z > 65f){
                right = true;
            }
        }
        if (left && right){
            transform.Rotate(0,0,-rotationSpeed * Time.deltaTime, space);
            if (Eyes.transform.localEulerAngles.z < 33f){
                left = false;
                right = false;
            }

        }
    }

    public IEnumerator Delay(){
        yield return new WaitForSeconds(delay);
    }

}
