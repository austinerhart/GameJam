using UnityEngine;

public class VerticalPlatformMover : MonoBehaviour
{
    public float moveDistance;       // How far to move up and down
    public float moveSpeed;          // Speed of movement

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // PingPong returns a value that goes back and forth between 0 and moveDistance
        float newY = startPosition.y + Mathf.PingPong(Time.time * moveSpeed, moveDistance);
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}

