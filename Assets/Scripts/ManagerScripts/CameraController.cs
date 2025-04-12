using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 cameraPos = playerPos + new Vector3(0f, 3.13f, -10f);
        transform.position = cameraPos;
    }
}
