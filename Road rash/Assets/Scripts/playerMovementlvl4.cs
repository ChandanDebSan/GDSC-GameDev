using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovementlvl4 : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private BoxCollider2D coll;
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private LayerMask jumpableWall;
    private float wallJumpCooldown;
    private enum MovementState { idle, Running, jumping, falling }
    // Start is called before the first frame update
    [SerializeField] private AudioSource JumpSoundEffect;
   
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = rb.GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        
        print(IsWall());
        if(wallJumpCooldown < 0.2f)
        {
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                JumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
            if(IsWall() && !IsGrounded())
            {
                rb.gravityScale = 0f;
            }
            else
            {
                rb.gravityScale = 1f;
               
            }
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }
        
        UpdateAnimationsState();
    }
   private void Jump()
    {
        if(IsGrounded()) {
            JumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            UpdateAnimationsState();
        }
        else if(IsWall() && !IsGrounded()) {
            if (Input.GetButtonDown("Jump"))
            {
                JumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            wallJumpCooldown = 0;

        }
    }
    private void UpdateAnimationsState()
    {
        MovementState state;
        if (dirX > 0f)
        {
            state = MovementState.Running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.Running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }
        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }
    private bool IsWall()
    {
        // Use sprite.flipX to determine the direction
        float direction = sprite.flipX ? -1f : 1f;

        // Use the direction in the BoxCast
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, new Vector2(direction, 0), 0.1f, jumpableWall);
    }
}
