using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    private PlayerAttack attack;

    [SerializeField] float jumpForce;

    private int jumpCount;
    public bool PlayerHasKey { get; private set; } = false;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        attack = GetComponent<PlayerAttack>();
        health = GetComponent<Health>();
    }

    private void Update()
    {
        PlayerGotKey();
        TriggerDeath();
        if (health.IsDead()) { return; }
        Flip();
        Move();
        Jump();
        IsPlayerTouchingGround();
    }

    protected override void Move()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        Vector2 playerMovementVelocity = new Vector2(horizontalAxis * runSpeed, body.velocity.y);
        body.velocity = playerMovementVelocity;

        bool isPlayerMoving = Mathf.Abs(body.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isRunning", isPlayerMoving);
    }

    protected override void Flip()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            spriteRenderer.flipX = true;
            attack.SetSlashPosition(false);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            spriteRenderer.flipX = false;
            attack.SetSlashPosition(true);
        }
    }

    public bool IsPlayerTouchingGround()
    {
        if (body.IsTouchingLayers(LayerMask.GetMask("Ground")) && body.velocity.y < Mathf.Epsilon)
        {
            jumpCount = 2;
            animator.SetBool("isJumping", false);
            return true;
        }
        return false;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            jumpCount--;
            Vector2 playerJumpVelocity = new Vector2(0f, jumpForce);
            body.velocity += playerJumpVelocity;
            animator.SetBool("isJumping", true);
        }
    }

    private void PlayerGotKey()
    {
        if (FindObjectOfType<Key>().PlayerHasKey)
        {
            PlayerHasKey = true;
        }
    }

    protected override void TriggerDeath()
    {
        if (health.IsDead())
        {
            animator.SetTrigger("death");
        }
    }
}