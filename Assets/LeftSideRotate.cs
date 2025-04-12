using UnityEngine;
using System.Collections;
using UnityEditor.Rendering;

public class LeftSideRotate : MonoBehaviour
{
    // Attach this script to the parent (RotationPivot)
public float rotationSpeed; 
public GameObject Eyes;
public Space space;
public bool left;
public bool right;
public int delay;
public int upperAngle;
public int lowerAngle;
private int middleAngle;

    void Start(){
        middleAngle = (upperAngle + lowerAngle) / 2;
    }
    void Update()
    {   
        if (!right){
            transform.Rotate(0,0,rotationSpeed * Time.deltaTime, space);
            if (Eyes.transform.localEulerAngles.z < upperAngle)
                right = true;
        }
        if (right && !left){
            transform.Rotate(0,0,-rotationSpeed * Time.deltaTime, space);
            if(Eyes.transform.localEulerAngles.z > lowerAngle){
                left = true;
            }
        }
        if (right && left){
            transform.Rotate(0,0,rotationSpeed * Time.deltaTime, space);
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
