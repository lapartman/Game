using System.Collections;
using UnityEngine;

public class PlayerMovement : Movement
{
    private PlayerAttack attack;

    private int jumpCount;
    public int playerKeyCount = 0;

    private GameManager gameManager;
    private BoxCollider2D feetCollider;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        attack = GetComponent<PlayerAttack>();
        health = GetComponent<Health>();
        gameManager = FindObjectOfType<GameManager>();
        feetCollider = GetComponent<BoxCollider2D>();
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
        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && body.velocity.y < Mathf.Epsilon)
        {
            jumpCount = 2;
            animator.SetBool("isJumping", false);
            return true;
        }
        return false;
    }

    protected override void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0 && !feetCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            jumpCount--;
            Vector2 playerJumpVelocity = new Vector2(0f, jumpForce);
            body.velocity += playerJumpVelocity;
            animator.SetBool("isJumping", true);
        }
    }

    private void OnDestroy()
    {
        if (health.IsDead())
        {
            gameManager.playerLives--;
            if (gameManager.playerLives != 0)
            {
                FindObjectOfType<LevelManager>().LoadCurrentLevel();
            }
            else
            {
                FindObjectOfType<LevelManager>().LoadLoseScreen();
            }
        }
    }

    protected override IEnumerator TriggerDeath()
    {
        animator.SetTrigger("death");
        body.velocity = new Vector2(0f, 0f);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}