using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Components

    public Rigidbody2D rb;
    Animator _anim;
    SpriteRenderer sr;
    private Coroutine hurtAnimCoroutine;

    #endregion

    #region Camera Shake Items
    [Header("Camera Shake Info")]
    [SerializeField] CameraShake _cameraShakeScript;
    [SerializeField] float screenShakeIntesity = 7f;
    [SerializeField] float screenSakeTime = 0.1f;
    #endregion

    #region Variables
    [Header("Movement Info")]
    public float moveSpeed;
    public float maxMoveSpeed;
    float defaultMoveSpeed;
    public bool canRun = false;
    bool isRunning;
    bool canRoll;

    [SerializeField] float speedMultipler;
    [SerializeField] float speedIncreaseMilestone;
    float speedMilestone;
    float defaultSpeedIncreaseMileston;


    [Header("Jump Info")]
    public float jumpForce;
    bool canDoubleJump;
    float defaultJumpForce;
    public float doubleJumpForce;
    [SerializeField] int _rollSetVelocity = 25;


    [Header("Knockback Info")]

    [SerializeField]Vector2 knockBackDirection;
    [SerializeField] float knockBackPower;
    bool canBeKnocked = true;
    bool isKnocked;

    [Header("Slide Info")]
    public float slideSpeedMultipler;
    bool isSliding;
    bool canSlide;
    [SerializeField] float slidingCooldown;
    [SerializeField] float slidingTime;
    float slidingBegun;

    [Header("Collision Dectection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] Transform bottomWallCheck;
    [SerializeField] Transform wallCheck;
    [SerializeField] Transform ceillingCheck;

    public float groundCheckRadius;
    public float wallCheckDistance;
    public LayerMask whatIsGround;

    bool isBottomWallDetected;
    bool isWallDetected;
    bool isGrounded;
    bool isCeillingDetected;

    [Header("Ledge Climb Info")]
    [SerializeField] Transform ledgeCheck;

    bool isTouchingLedge;
    bool isLedgeDetected;
    bool canClimbLedge;


    public float ledgeClimb_Xoffset1 = 0f;
    public float ledgeClimb_Yoffset1 = 0f;
    public float ledgeClimb_Xoffset2 = 0f;
    public float ledgeClimb_Yoffset2 = 0f;
    #endregion

    Vector2 ledgePosBot;
    Vector2 ledgePost1; // position to hold the player before animation ends
    Vector2 ledgePos2;  // position where to move player after animation ends

    void Start()
    {

        SettingDefaultValues();

        rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        CheckForRun();
        CheckForJump();
        CheckForSlide();
        CheckForSpeedingUp();
        CheckForLedgeClimb();
        AnimationControllers();
        CheckForCollision();
    }

    void SettingDefaultValues()
    {
        defaultJumpForce = jumpForce;
        defaultMoveSpeed = moveSpeed;
        defaultSpeedIncreaseMileston = speedIncreaseMilestone;
        speedMilestone = defaultSpeedIncreaseMileston;
    }

    void AnimationControllers()
    {
        _anim.SetFloat("xVelocity", rb.velocity.x);
        _anim.SetFloat("yVelocity", rb.velocity.y);
        _anim.SetBool("IsRunning", isRunning);
        _anim.SetBool("IsGrounded", isGrounded);
        _anim.SetBool("IsSliding", isSliding);
        _anim.SetBool("CanClimbLedge", canClimbLedge);
        _anim.SetBool("CanDoubleJump", canDoubleJump);
        _anim.SetBool("CanRoll", canRoll);
        _anim.SetBool("IsKnocked", isKnocked);

        if (rb.velocity.y < - _rollSetVelocity) // determines how much velocity is needed for a roll
        {
            canRoll = true;
        }
    }

    void RollAnimationFinished()
    {
        canRoll = false;
    }

    public void KnockBack()
    {
        if (canBeKnocked)
        {
            isKnocked = true;
            HurtVFX();
            SpeedReset();
            CameraShake.Instance.ShakeCamera(screenShakeIntesity, screenSakeTime); // Camera Shake FX
        }
    }

    void KnockBackAnimationFinished()
    {
        isKnocked = false;
        canRun = true;
    }

    void CheckForRun()
    {
        if (isKnocked && canBeKnocked)
        {
            canBeKnocked = false;
            canRun = false;
            rb.velocity = knockBackDirection * knockBackPower;
        }

        if (canRun)
        {
            if (isBottomWallDetected || isWallDetected && !isSliding)
            {
                SpeedReset();
            }
            else if (isSliding)
            {
                rb.velocity = new Vector2(moveSpeed * slideSpeedMultipler, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }
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

    void CheckForJump()
    {

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse1) && !isKnocked)
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

    void CheckForSlide()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Mouse0) && canSlide && isGrounded && rb.velocity.x > defaultMoveSpeed)
        {
            isSliding = true;
            canSlide = false;
            slidingBegun = Time.time;
        }

        if (Time.time > slidingBegun + slidingTime && !isCeillingDetected)
        {
            isSliding = false; // makes sliding over
        }

        if (Time.time > slidingBegun + slidingCooldown)
        {
            canSlide = true;
        }
    }

    void CheckForSpeedingUp()
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
    }

    void CheckForLedgeClimb()
    {
        if (isLedgeDetected && !canClimbLedge)
        {
            canClimbLedge = true;

            ledgePost1 = new Vector2((ledgePosBot.x + wallCheckDistance) + ledgeClimb_Xoffset1, (ledgePosBot.y) + ledgeClimb_Yoffset1);
            ledgePos2 = new Vector2(ledgePosBot.x + wallCheckDistance + ledgeClimb_Xoffset2, (ledgePosBot.y) + ledgeClimb_Yoffset2);

            canRun = false;
        }

        if (canClimbLedge)
        {
            transform.position = ledgePost1;
        }
    }

    void CheckIfLedgeClimbFinished()
    {
        transform.position = ledgePos2;
        canClimbLedge = false;
        canRun = true;
        isLedgeDetected = false;
    }

    void Jump()
    {
        AudioManager.instance.PlaySFX(1);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void SpeedReset()
    {
        moveSpeed = defaultMoveSpeed;
        speedIncreaseMilestone = defaultSpeedIncreaseMileston;
    }

    void CheckForCollision()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isBottomWallDetected = Physics2D.Raycast(bottomWallCheck.position, Vector2.right, wallCheckDistance, whatIsGround);
        isCeillingDetected = Physics2D.Raycast(ceillingCheck.position, Vector2.up, wallCheckDistance + 0.5f, whatIsGround);

        isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, Vector2.right, wallCheckDistance, whatIsGround);
        isWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, whatIsGround);


        if (isWallDetected && !isTouchingLedge && !isLedgeDetected)
        {
            isLedgeDetected = true;
            ledgePosBot = wallCheck.position;
        }
    }

    IEnumerator HurtVFXRoutine()
    {
        sr.color = Color.white;

        Color darkenColor = new Color(sr.color.r, sr.color.g, sr.color.b, 0.1f);

        sr.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        sr.color = Color.white;

        yield return new WaitForSeconds(0.1f);

        sr.color = darkenColor;

        yield return new WaitForSeconds(0.1f);

        sr.color = Color.white;

        yield return new WaitForSeconds(0.1f);

        sr.color = darkenColor;

        yield return new WaitForSeconds(0.1f);

        sr.color = Color.white;

        yield return new WaitForSeconds(0.2f);

        sr.color = darkenColor;

        yield return new WaitForSeconds(0.3f);

        sr.color = Color.white;

        yield return new WaitForSeconds(0.2f);

        sr.color = darkenColor;

        yield return new WaitForSeconds(0.3f);

        sr.color = Color.white;

        yield return new WaitForSeconds(0.1f);

        canBeKnocked = true;

        hurtAnimCoroutine = null;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
        Gizmos.DrawLine(ledgeCheck.position, new Vector3(ledgeCheck.position.x + wallCheckDistance, ledgeCheck.position.y, ledgeCheck.position.z));
        Gizmos.DrawLine(bottomWallCheck.position, new Vector3(bottomWallCheck.position.x + wallCheckDistance, bottomWallCheck.position.y, bottomWallCheck.position.z));

        Gizmos.DrawLine(ceillingCheck.position, new Vector3(ceillingCheck.position.x, ceillingCheck.position.y + wallCheckDistance, ceillingCheck.position.z));
    }

    public void HurtVFX()
    {
        if (hurtAnimCoroutine != null)
        {
            StopCoroutine(hurtAnimCoroutine); // stops active coroutine before activating new one
        }   

        
        hurtAnimCoroutine = StartCoroutine(HurtVFXRoutine()); // starts coroutine
    }
}