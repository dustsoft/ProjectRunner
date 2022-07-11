using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Components
    Rigidbody2D rb;
    Animator _anim;
    #endregion

    [Header("Movement Info")]
    public float moveSpeed;
    public float maxMoveSpeed;
    bool canRun = false;

    [SerializeField] float speedMultipler;
    [SerializeField] float speedIncreaseMilestone;
    float speedMilestone;

    [Header("Jump Info")]
    public float jumpForce;
    bool canDoubleJump;
    float defaultJumpForce;
    public float doubleJumpForce;

    [Header("Slide Info")]
    public float slideSpeedMultipler;
    bool isSliding;
    bool canSlide;
    [SerializeField] float slidingCooldown;
    [SerializeField] float slidingTime;
    float slidingBegun;

    [Header("Collision Dectection")]
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    bool isGrounded;
    bool isRunning;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

        defaultJumpForce = jumpForce;
    }

    void Update()
    {
        if (Input.anyKey)
        {
            canRun = true;
        }

        CheckForRun();
        CheckForJump();
        CheckForSlide();

        AnimationControllers();
        CheckForCollision();
    }

    private void AnimationControllers()
    {
        _anim.SetFloat("yVelocity", rb.velocity.y);
        _anim.SetBool("IsRunning", isRunning);
        _anim.SetBool("IsGrounded", isGrounded);
        _anim.SetBool("IsSliding", isSliding);

    }

    private void CheckForRun()
    {
        if (transform.position.x > speedMilestone)
        {
            speedMilestone = speedMilestone + speedIncreaseMilestone;
            moveSpeed = moveSpeed * speedMultipler;
            speedIncreaseMilestone = speedIncreaseMilestone * speedMultipler;

            if (moveSpeed > maxMoveSpeed)
            {
                moveSpeed = maxMoveSpeed;
            }
        }


        if (isSliding)
        {
            rb.velocity = new Vector2(moveSpeed * slideSpeedMultipler, rb.velocity.y);
        }
        else if (canRun == true)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }

        if (rb.velocity.x > 0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    private void CheckForJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (isGrounded)
            {
                Jump();
            }
            else if (canDoubleJump)
            {
                jumpForce = doubleJumpForce;
                Jump();
                canDoubleJump = false;
            }    
        }

        if (isGrounded == true)
        {
            jumpForce = defaultJumpForce;
            canDoubleJump = true;
        }
    }

    private void CheckForSlide()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canSlide && isGrounded)
        {
            isSliding = true;
            canSlide = false;
            slidingBegun = Time.time;
        }

        if (Time.time > slidingBegun + slidingTime)
        {
            isSliding = false;
        }

        if (Time.time > slidingBegun + slidingCooldown)
        {
            canSlide = true;
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

    }

    private void CheckForCollision()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}