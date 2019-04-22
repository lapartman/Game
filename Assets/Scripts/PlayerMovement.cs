using System.Collections;
using UnityEngine;

public class PlayerMovement : Movement
{
    private PlayerAttack attack;

    private int jumpCount;
    public int playerKeyCount = 0;

    private GameManager gameManager;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        attack = GetComponent<PlayerAttack>();
        health = GetComponent<Health>();
        gameManager = FindObjectOfType<GameManager>();
        health.SetStartingHealth(gameManager.playerHealth);
    }

    private void Update()
    {
        if (health.IsDead())
        {
            StartCoroutine(TriggerDeath());
            return;
        }
        Flip();
        Move();
        Jump();
        IsCharacterTouchingGround();
        Debug.Log(playerKeyCount);
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
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.RightArrow)))
        {
            spriteRenderer.flipX = true;
            attack.SetSlashPosition(false);
        }
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftArrow)))
        {
            spriteRenderer.flipX = false;
            attack.SetSlashPosition(true);
        }
    }

    public override bool IsCharacterTouchingGround()
    {
        if (body.IsTouchingLayers(LayerMask.GetMask("Ground")) && body.velocity.y < Mathf.Epsilon)
        {
            jumpCount = 2;
            animator.SetBool("isJumping", false);
            return true;
        }
        return false;
    }

    protected override void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            jumpCount--;
            Vector2 playerJumpVelocity = new Vector2(0f, jumpForce);
            body.velocity += playerJumpVelocity;
            animator.SetBool("isJumping", true);
        }
    }

    protected override IEnumerator TriggerDeath()
    {
        yield return new WaitForSeconds(0f);
        animator.SetTrigger("death");
        body.velocity = new Vector2(0f, 0f);
    }
}