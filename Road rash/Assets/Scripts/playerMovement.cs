using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private BoxCollider2D coll;
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField]private float jumpForce = 7f;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private LayerMask jumpableWall;
    private float wallJumpCooldown;
    private enum MovementState {idle,Running,jumping,falling}
    // Start is called before the first frame update
    [SerializeField] private AudioSource JumpSoundEffect;
    public Joystick joy;
    public float dirY;
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
        //dirX = Input.GetAxisRaw("Horizontal");
        dirX = joy.Horizontal;
        dirY = joy.Vertical;

        if(dirX >= 0.5f)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        else if(dirX <= -0.5f)
        {
            rb.velocity = new Vector2(-1f*moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        //rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        //if (Input.GetButtonDown("Jump") && IsGrounded())
        if (dirY >= 0.5f && IsGrounded())
        {
            JumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); 
        }
        UpdateAnimationsState();
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
        if(rb.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        else if(rb.velocity.y < -0.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state",(int)state);
    }
    private bool IsGrounded()
    {
       return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }
    private bool IsWall()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, new Vector2(transform.localScale.x,0), 0.1f, jumpableWall);
    }
}
