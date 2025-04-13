using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskPickup : MonoBehaviour
{
    GameObject player;
    GameObject self;
    public Sprite newSprite;
    Sprite oldSprite;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        self = GameObject.Find("Mask");
        oldSprite = player.GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Player")){
            if(Input.GetKey(KeyCode.E)){
                player.tag = "PlayerEnd";
                player.GetComponent<SpriteRenderer>().sprite = newSprite;
                Destroy(self);
            }
        }
    }
}
