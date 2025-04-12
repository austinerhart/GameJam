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
public float smallAngle = 20.75f;
public float bigAngle = 65f;
float middleAngle;

    void Start(){
        middleAngle = (smallAngle + bigAngle) / 2;
    }
    void Update()
    {   
        if (!left){
            transform.Rotate(0,0,-rotationSpeed * Time.deltaTime, space);
            if (Eyes.transform.localEulerAngles.z < smallAngle)
                left = true;
        }
        if (left && !right){
            transform.Rotate(0,0,rotationSpeed * Time.deltaTime, space);
            if(Eyes.transform.localEulerAngles.z > bigAngle){
                right = true;
            }
        }
        if (left && right){
            transform.Rotate(0,0,-rotationSpeed * Time.deltaTime, space);
            if (Eyes.transform.localEulerAngles.z < middleAngle){
                left = false;
                right = false;
            }

        }
    }

    public IEnumerator Delay(){
        yield return new WaitForSeconds(delay);
    }

}
