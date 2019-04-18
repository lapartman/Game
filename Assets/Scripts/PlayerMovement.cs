using UnityEngine;

public class PlayerMovement : Movement
{
    private PlayerAttack attack;

    private int jumpCount;
    public bool PlayerHasKey { get; private set; } = false;
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
        PlayerGotKey();
        TriggerDeath();
        if (health.IsDead()) { return; }
        Flip();
        Move();
        Jump();
        IsCharacterTouchingGround();
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
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            spriteRenderer.flipX = true;
            attack.SetSlashPosition(false);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
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
            body.velocity = new Vector2(0f, 0f);
        }
    }
}