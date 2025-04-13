using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{

    [SerializeField] private RectTransform IrisTransform;
    [SerializeField] private float move_speed;
    [SerializeField] private float move_scale;

    private float transform_x;
    private float transform_y;


    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float local_move_speed = move_speed;
        Vector3 mousePosition = Input.mousePosition;
        transform_y = mousePosition.y;
        transform_x = mousePosition.x;



        transform_y = Mathf.Clamp(transform_y - 800f, -80f, 80f);
        transform_x = Mathf.Clamp(transform_x - 945f, -220f, 220f);

        Vector2 iris_clamp = new Vector2(transform_x, transform_y);

        IrisTransform.anchoredPosition = Vector2.Lerp(IrisTransform.anchoredPosition, iris_clamp, local_move_speed);
    }
}
