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
    [SerializeField] private PlayerInput PlayerController;
    [SerializeField] private LayerMask groundLayer;

    // Private Variables
    private InputAction movement;
    private InputAction jump;
    private Vector2 direction = Vector2.zero;
    private bool hasJumped = false;

    private void Awake()
    {
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
            hasJumped = false;
        }

        if(jump.WasReleasedThisFrame() && player.velocity.y > 0)
        {
            player.velocity = new Vector2(player.velocity.x, player.velocity.y * 0.5f);
        }
    }

    private void FixedUpdate()
    {
        Vector2 local_direction = movement.ReadValue<Vector2>();
        float dir_x = 0;
        float dir_y = player.velocity.y;

        if (local_direction.y > 0 && IsGrounded() && !hasJumped)
        {
            dir_y = local_direction.y * jump_height + move_speed;
            hasJumped = true;
        }

        if (local_direction.x != 0)
        {
            dir_x = local_direction.x * move_speed;
        }

        player.velocity = new Vector2(dir_x, dir_y);
    }

    private bool IsGrounded()
    {
        float extraHeightTest = 0.1f;
        RaycastHit2D raycast_hit = Physics2D.BoxCast(player_collider.bounds.center, player_collider.bounds.size, 0f, Vector2.down, extraHeightTest, groundLayer);

        return raycast_hit.collider != null;

    }

}