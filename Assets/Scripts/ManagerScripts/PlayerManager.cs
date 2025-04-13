using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody2D player_rb;
    [SerializeField] private BoxCollider2D player_collider;
    [SerializeField] private SpriteRenderer player_sprite;
    [SerializeField] private Animator player_animator;

    [Header("Speed Settings")]
    [SerializeField] private float move_speed = 10f;
    [SerializeField] private float jump_height = 8f;
    [SerializeField] private float climb_speed = 3f;
    [SerializeField] private float hang_time = 0.12f;
    [SerializeField] private float jump_buffer_length = 0.07f;

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask ladderLayer;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private AudioClip[] climbingSounds;
    [SerializeField] private float footstepInterval = 0.3f;
    [SerializeField] private float climbingInterval = 0.4f;
    [SerializeField] private float footstepVolume = 0f;
    [SerializeField] private float climbingVolume = 0f;

    // Private Variables
    private PlayerInput PlayerController;
    private Vector2 direction = Vector2.zero;
    private InputAction movement;
    private InputAction jump;
    private float hang_counter;
    private float jump_buffer_counter;
    private float player_scale_x;
    private float player_scale_y;
    private float player_scale_z;
    private float player_crouch_y;
    private float player_crouch_offset;
    private bool isCrouching;

    // Sound variables
    private float lastFootstepTime;
    private float lastClimbingTime;
    private bool isClimbing = false;
    private bool footstepSoundPlaying = false;
    private bool climbingSoundPlaying = false;
    private AudioSource currentFootstepSource;
    private AudioSource currentClimbingSource;

    private void Awake()
    {
        player_scale_x = player_collider.size.x;
        player_scale_y = player_collider.size.y;
        player_crouch_y = 0.3737239f;
        player_crouch_offset = -0.2144155f;
        player_rb.freezeRotation = true;
        PlayerController = new PlayerInput();
    }

    private void OnEnable()
    {
        movement = PlayerController.Player.Move;
        jump = PlayerController.Player.Jump;
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
        bool onLadder = IsOnLadder();
        player_animator.SetBool("isClimbing", onLadder);
        player_animator.SetBool("isCrouching", isCrouching);
        player_animator.SetBool("isJumping", (!IsGrounded() && !onLadder));
        player_animator.SetFloat("xVelocity", Math.Abs(player_rb.velocity.x));
        player_animator.SetFloat("yVelocity", player_rb.velocity.y);

        direction = movement.ReadValue<Vector2>();

        if (IsGrounded())
        {
            hang_counter = hang_time;

            if (Math.Abs(player_rb.velocity.x) > 0.5f && !isCrouching)
            {
                if (Time.time >= lastFootstepTime + footstepInterval)
                {
                    PlayFootstepSound();
                    lastFootstepTime = Time.time;
                }
            }
            else
            {
                StopFootstepSound();
            }
        }
        else
        {
            hang_counter -= Time.deltaTime;
            StopFootstepSound();
        }

        if (onLadder && Math.Abs(player_rb.velocity.y) > 0.1f)
        {
            isClimbing = true;
            if (Time.time >= lastClimbingTime + climbingInterval)
            {
                PlayClimbingSound();
                lastClimbingTime = Time.time;
            }
        }
        else
        {
            isClimbing = false;
            StopClimbingSound();
        }

        if (jump.WasPressedThisFrame())
        {
            jump_buffer_counter = jump_buffer_length;
        }
        else
        {
            jump_buffer_counter -= Time.deltaTime;
        }

        if (jump.WasReleasedThisFrame() && player_rb.velocity.y > 0)
        {
            player_rb.velocity = new Vector2(player_rb.velocity.x, player_rb.velocity.y * 0.5f);
        }
    }

    private void FixedUpdate()
    {
        Vector2 local_direction = movement.ReadValue<Vector2>();
        float dir_x = 0;
        float dir_y = player_rb.velocity.y;

        if (IsOnLadder())
        {
            dir_y = local_direction.y * climb_speed;
            dir_x = local_direction.x * climb_speed;

            player_rb.gravityScale = 0f;
        }
        else
        {
            player_rb.gravityScale = 5f;
        }

        if (local_direction.y > 0 && hang_counter > 0f && jump_buffer_counter >= 0)
        {
            dir_y = local_direction.y * jump_height + move_speed;

            jump_buffer_counter = 0;
        }

        if (local_direction.x != 0)
        {
            if (local_direction.x < 0)
            {
                player_sprite.flipX = true;
            }
            else
            {
                player_sprite.flipX = false;
            }

            dir_x = local_direction.x * move_speed;
        }

        if (IsWallLeft())
        {
            if (local_direction.x < 0)
            {
                dir_x = 0;
            }
            else
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

        if (local_direction.y < 0)
        {
            if (!IsOnLadder() && IsGrounded())
            {
                isCrouching = true;
                dir_x = 0;
                dir_y = 0;
                player_collider.size = new Vector2(player_scale_x, player_crouch_y);
                player_collider.offset = new Vector2(0.02f, player_crouch_offset);
            }
        }
        else
        {
            isCrouching = false;
            player_collider.size = new Vector2(player_scale_x, player_scale_y);
            player_collider.offset = new Vector2(0.02f, 0f);
        }

        player_rb.velocity = new Vector2(dir_x, dir_y);
    }

    private bool IsGrounded()
    {
        float extra_height_test = 0.1f;

        RaycastHit2D raycast_hit = Physics2D.BoxCast(player_collider.bounds.center, player_collider.bounds.size, 0f, Vector2.down, extra_height_test, groundLayer);

        return raycast_hit.collider != null;
    }

    private bool IsWallLeft()
    {
        float extra_length_test = 0.1f;

        Vector2 box_size = player_collider.bounds.size;
        box_size.y = box_size.y * 0.9f;
        RaycastHit2D raycast_left_hit = Physics2D.BoxCast(player_collider.bounds.center, box_size, 0f, Vector2.left, extra_length_test, groundLayer);

        return raycast_left_hit.collider != null;
    }

    private bool IsWallRight()
    {
        float extra_length_test = 0.1f;

        Vector2 box_size = player_collider.bounds.size;
        box_size.y = box_size.y * 0.9f;
        RaycastHit2D raycast_left_hit = Physics2D.BoxCast(player_collider.bounds.center, box_size, 0f, Vector2.right, extra_length_test, groundLayer);

        return raycast_left_hit.collider != null;
    }

    private bool IsOnLadder()
    {
        float extra_length_test = 0.1f;

        RaycastHit2D ladder_left_hit = Physics2D.BoxCast(player_collider.bounds.center, player_collider.bounds.size, 0f, Vector2.left, extra_length_test, ladderLayer);
        RaycastHit2D ladder_right_hit = Physics2D.BoxCast(player_collider.bounds.center, player_collider.bounds.size, 0f, Vector2.right, extra_length_test, ladderLayer);

        bool ladder_hit = ladder_left_hit.collider != null || ladder_right_hit.collider != null;
        return ladder_hit;
    }

    public void PlayFootstepSound()
    {
        if (footstepSounds.Length > 0 && IsGrounded() && !footstepSoundPlaying)
        {
            footstepSoundPlaying = true;

            float randomPitch = UnityEngine.Random.Range(0.9f, 1.1f);

            AudioClip selectedClip = footstepSounds[UnityEngine.Random.Range(0, footstepSounds.Length)];

            AudioSource audioSource = SoundFXManager.Instance.CreateAudioSource(selectedClip, transform, footstepVolume, randomPitch);
            currentFootstepSource = audioSource;

            ResetFootstepFlag(selectedClip.length);
        }
    }

    public void StopFootstepSound()
    {
        if (currentFootstepSource != null)
        {
            Destroy(currentFootstepSource.gameObject);
            currentFootstepSource = null;
            footstepSoundPlaying = false;
            StopCoroutine("ResetFootstepFlag");
        }
    }

    private IEnumerator ResetFootstepFlag(float delay)
    {
        yield return new WaitForSeconds(delay);
        footstepSoundPlaying = false;
        currentFootstepSource = null;
    }

    public void PlayClimbingSound()
    {
        if (climbingSounds.Length > 0 && isClimbing && !climbingSoundPlaying)
        {
            climbingSoundPlaying = true;

            float randomPitch = UnityEngine.Random.Range(0.95f, 1.05f);

            AudioClip selectedClip = climbingSounds[UnityEngine.Random.Range(0, climbingSounds.Length)];

            AudioSource audioSource = SoundFXManager.Instance.CreateAudioSource(selectedClip, transform, climbingVolume, randomPitch);
            currentClimbingSource = audioSource;

            ResetClimbingFlag(selectedClip.length);
        }
    }

    public void StopClimbingSound()
    {
        if (currentClimbingSource != null)
        {
            Destroy(currentClimbingSource.gameObject);
            currentClimbingSource = null;
            climbingSoundPlaying = false;
            StopCoroutine("ResetClimbingFlag");
        }
    }

    private IEnumerator ResetClimbingFlag(float delay)
    {
        yield return new WaitForSeconds(delay);
        climbingSoundPlaying = false;
        currentClimbingSource = null;
    }
}