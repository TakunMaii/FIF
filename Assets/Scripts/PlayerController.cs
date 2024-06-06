using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private float jumpSpeed = 7f;
    [SerializeField] private float RisingGravityScale = 5f;
    [SerializeField] private float fallingGravityScale = 10f;
    [SerializeField] private Vector3 resetPos;

    private Rigidbody2D rb;
    private BoxCollider2D foot;
    private BoxCollider2D body;
    private float moveInput;
    private float stepTimer = 0;
    [SerializeField] private float KPressedTimer = 0f; //记录K键按下的时间
    [SerializeField] private float jumpPressThreshold = 0.3f;

    private bool isMovingRight = true;
    private bool isWalking => moveInput != 0;
    private bool isGrounded => foot.IsTouchingLayers(Ground);
    private bool isJumping => rb.velocity.y > 1e-4;
    private bool isFalling => rb.velocity.y < -1e-4;

    // 是否带着松子
    private bool isTakingAcorn;
    public bool GetIsTakingAcorn() => isTakingAcorn;
    public void GetAcorn() { isTakingAcorn = true; }

    private void Start()
    {
        body = gameObject.GetComponents<BoxCollider2D>()[1];
        foot = gameObject.GetComponents<BoxCollider2D>()[0];
        rb = gameObject.GetComponent<Rigidbody2D>();

        isTakingAcorn = false;
    }

    public void Reset()
    {
        isTakingAcorn = false;
        rb.velocity = Vector2.zero;
        gameObject.transform.position = resetPos;
    }

    private void Update()
    {
        if (GameManager.Instance.GetIsGaming())
            CheckInput();

        if (transform.position.y < -30)
        {
            GameManager.Instance.OnPlayerDied();
        }
    }

    private void CheckInput()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        stepTimer -= Time.deltaTime;
        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && stepTimer <= 0)
        {
            stepTimer = 0.3f;
            AudioManager.Ins.PlaySounds("walk", GameManager.Instance.GetPlayer().transform.position);
        }
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                AudioManager.Ins.PlaySounds("jump", GameManager.Instance.GetPlayer().transform.position);
                rb.gravityScale = RisingGravityScale;
                rb.velocity += new Vector2(0, jumpSpeed);
                KPressedTimer = 0f;
            }
        }
        ControllJump();
        CheckDirection();
    }

    private void CheckDirection()
    {
        if (isMovingRight && moveInput < 0)
        {
            Flip();
        }
        else if (!isMovingRight && moveInput > 0)
        {
            Flip();
        }
    }


    private void ControllJump()
    {
        if(isJumping)
        {
            KPressedTimer += Time.deltaTime;
            if (KPressedTimer < jumpPressThreshold && Input.GetKeyUp(KeyCode.K))
            {
                rb.gravityScale = fallingGravityScale;
            }
        }
        if (isFalling)
        {
            //下落加快
            rb.gravityScale = fallingGravityScale;
        }
    }
    private void Flip()
    {
        transform.Rotate(0, 180f, 0);
        isMovingRight = !isMovingRight;
    }

    public bool IsWalking() => isWalking;

    public bool IsGrounded() => isGrounded;

    public bool IsJumping() => isJumping;

    public bool IsFalling() => isFalling;
}
