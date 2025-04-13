using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class BoxFall : MonoBehaviour
{
    public bool fall;
    public Vector2 increment = new (0f, 1f);
    public float dropDistance;
    public GameObject parent;
    GameObject box;
    GameObject mask;

    // Start is called before the first frame update
    void Start()
    {
        box = GameObject.Find("Box");
        mask = GameObject.Find("Mask");
    }

    // Update is called once per frame
    void Update()
    {
        if(fall){
            transform.Translate(-increment * Time.deltaTime * 2);
            Debug.Log("Entered");
            if(transform.position.y < dropDistance){
                fall = false;
                mask.transform.SetParent(parent.transform);
                Destroy(box);

            }
        }
    }
}
