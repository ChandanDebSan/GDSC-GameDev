using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Movement")]
    public float playerSpeed = 1.9f;
    public float playerSprint = 3f;


    [Header("Player Health Thimgs")]
    private float playerHealth = 120f;
    public float presentHealth;
    public GameObject Damage;
    public Slider slider;
    public Gradient gradient;
    public Image fill;


    [Header("Player Script Cameras")]
    public Transform playerCamera;

    [Header("Player Animator and Gravity")]
    public CharacterController cc;
    public float gravity = -9.81f;
    public Animator animator;

    [Header("Player Jumping and Velocity")]
    public float turnCalmTime =0.1f;
    private float turnCalmVelocity;
    private float jumpRange = 1.5f;
    Vector3 velocity;
    public Transform surfaceCheck;
    private bool onSurface;
    public float surfaceDistance = 0.4f;
    public LayerMask surfaceMask;

 
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        presentHealth = playerHealth;
        slider.value = presentHealth;
        fill.color = gradient.Evaluate(1f);
    }
    private void Update()
    {
        CheckGroundStatus();
        ApplyGravity();
        PlayerMove();
        Jump();
        Sprint();
    }

    void CheckGroundStatus()
    {
        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);
       

        if (onSurface && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }

    void PlayerMove()
    {
        float horizontal_axis = Input.GetAxisRaw("Horizontal");
        float vertical_axis = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;

        if (direction.magnitude >= 0.1f)
        {
            //Walking Animation
            animator.SetBool("Idle", false);
            animator.SetBool("Walk", true);
            animator.SetBool("Running", false);
            animator.SetBool("RifleWalk", false);
            animator.SetBool("IdleAim", false);

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            cc.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);
        }
        else
        {
            //Idle Animation
            animator.SetBool("Idle", true);
            animator.SetBool("Walk", false);
            animator.SetBool("Running", false);
           
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("Idle", false);
            animator.SetTrigger("Jump");
          
            velocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.ResetTrigger("Jump");
        }
    }
    void  Sprint()
    {
        if(Input.GetButton("Sprint")&& Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            float horizontal_axis = Input.GetAxisRaw("Horizontal");
            float vertical_axis = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;

            if (direction.magnitude >= 0.1f)
            {
           
                animator.SetBool("Walk", false);
                animator.SetBool("Running", true);


                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                cc.Move(moveDirection.normalized * playerSprint * Time.deltaTime);
            }
            else
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Running", false);
            }
        }
    }
    public void playerHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;
        slider.value = presentHealth;
        fill.color= gradient.Evaluate(slider.normalizedValue);
        StartCoroutine(playerDamage());

        if(presentHealth <= 0) {

            playerDie();
        }
    }
    private void playerDie()
    {
        Cursor.lockState = CursorLockMode.None;
        Object.Destroy(gameObject, 1.0f);
    }
    IEnumerator playerDamage()
    {
        Damage.SetActive(true);
        yield return new WaitForSeconds(1f);
        Damage.SetActive(false);
    }
}
