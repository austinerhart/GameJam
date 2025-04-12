using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private Rigidbody2D player;
    [SerializeField] private BoxCollider2D player_collider;
    [SerializeField] private float move_speed = 10f;
    [SerializeField] private float jump_height = 8f;
    [SerializeField] private float hang_time = 0.12f;
    [SerializeField] private float jump_buffer_length = 0.07f;
    [SerializeField] private PlayerInput PlayerController;
    [SerializeField] private LayerMask groundLayer;

    // Private Variables
    private Vector2 direction = Vector2.zero;
    private InputAction movement;
    private InputAction jump;
    private float hang_counter;
    private float jump_buffer_counter;

    private void Awake()
    {

        player.freezeRotation = true;
        PlayerController = new PlayerInput();
    }

    private void OnEnable()
    {
        jump = PlayerController.Player.Jump;
        movement = PlayerController.Player.Move;
        movement.Enable();
        jump.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        jump.Disable();
    }

    void Update()
    {
        direction = movement.ReadValue<Vector2>();

        if (IsGrounded())
        {
            hang_counter = hang_time;
        } else
        {
            hang_counter -= Time.deltaTime;
        }

        if (jump.WasPressedThisFrame())
        {
            jump_buffer_counter = jump_buffer_length;
        } else
        {
            jump_buffer_counter -= Time.deltaTime;
        }

        if (jump.WasReleasedThisFrame() && player.velocity.y > 0)
        {
            player.velocity = new Vector2(player.velocity.x, player.velocity.y * 0.5f);
        }
    }

    private void FixedUpdate()
    {
        Vector2 local_direction = movement.ReadValue<Vector2>();
        float dir_x = 0;
        float dir_y = player.velocity.y;

        if (local_direction.y > 0 && hang_counter > 0f && jump_buffer_counter >= 0)
        {
            dir_y = local_direction.y * jump_height + move_speed;

            jump_buffer_counter = 0;
        }

        if (local_direction.x != 0)
        {
            dir_x = local_direction.x * move_speed;
        }

        if (IsWallLeft())
        {
            if(local_direction.x < 0)
            {
                dir_x = 0;
            } else
            {
                dir_x = local_direction.x * move_speed;
            }
        }

        if (IsWallRight())
        {
            if (local_direction.x > 0)
            {
                dir_x = 0;
            }
            else
            {
                dir_x = local_direction.x * move_speed;
            }
        }

        player.velocity = new Vector2(dir_x, dir_y);
    }

    private bool IsGrounded()
    {
        float extraHeightTest = 0.1f;

        RaycastHit2D raycast_hit = Physics2D.BoxCast(player_collider.bounds.center, player_collider.bounds.size, 0f, Vector2.down, extraHeightTest, groundLayer);

        return raycast_hit.collider != null;
    }

    private bool IsWallLeft()
    {
        float extraLengthTest = 0.1f;

        Vector2 box_size = player_collider.bounds.size;
        box_size.y = box_size.y * 0.9f;
        RaycastHit2D raycast_left_hit = Physics2D.BoxCast(player_collider.bounds.center, box_size, 0f, Vector2.left, extraLengthTest, groundLayer);

        return raycast_left_hit.collider != null;
    }

    private bool IsWallRight()
    {
        float extraLengthTest = 0.1f;

        Vector2 box_size = player_collider.bounds.size;
        box_size.y = box_size.y * 0.9f;
        RaycastHit2D raycast_left_hit = Physics2D.BoxCast(player_collider.bounds.center, box_size, 0f, Vector2.right, extraLengthTest, groundLayer);

        return raycast_left_hit.collider != null;
    }



}