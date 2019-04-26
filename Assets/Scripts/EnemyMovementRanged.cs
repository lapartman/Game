using System.Collections;
using UnityEngine;

public class EnemyMovementRanged : Movement
{
    private PlayerMovement player;
    private CapsuleCollider2D characterCollider;
    private EnemyAttackRanged attackRanged;
    private GameManager gameManager;

    [SerializeField] float playerDetectionRange;
    [SerializeField] int abilityValue;
    [SerializeField] float jumpTimer;
    [SerializeField] int scoreValue;

    private float jumpResetTimer;
    public bool IsCharacterFlipped { get; private set; } = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        body = GetComponent<Rigidbody2D>();
        characterCollider = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        attackRanged = GetComponent<EnemyAttackRanged>();
        player = FindObjectOfType<PlayerMovement>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (health.IsDead())
        {
            StartCoroutine(TriggerDeath());
            return;
        }
        IsCharacterTouchingGround();
        Move();
        Flip();
        Jump();
    }

    private void OnDestroy()
    {
        if (health.IsDead())
        {
            gameManager.AddAbilityPoints(abilityValue);
            gameManager.AddToTotalScore(scoreValue);
            FindObjectOfType<ScoreDisplay>().DisplayPoints();
        }
    }

    protected override void Move()
    {
        if (IsPlayerInSpecifiedRange(playerDetectionRange) && !IsPlayerInSpecifiedRange(attackRanged.attackRange))
        {
            animator.SetBool("isRunning", true);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, runSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    public bool IsPlayerInSpecifiedRange(float rangeType)
    {
        return Vector2.Distance(transform.position, player.transform.position) <= rangeType;
    }

    protected override void Flip()
    {
        if (player.transform.position.x < body.transform.position.x)
        {
            spriteRenderer.flipX = true;
            attackRanged.SetSlashPosition(true);
            IsCharacterFlipped = false;
        }
        else if (player.transform.position.x > body.transform.position.x)
        {
            spriteRenderer.flipX = false;
            attackRanged.SetSlashPosition(false);
            IsCharacterFlipped = true;
        }
    }

    protected override IEnumerator TriggerDeath()
    {
        yield return new WaitForSeconds(0f);
        animator.SetTrigger("death");
        body.bodyType = RigidbodyType2D.Static;
        Destroy(characterCollider);
        Destroy(gameObject, deathDelay);
    }

    protected override void Jump()
    {
        if (AllowJump() && jumpResetTimer <= 0f && IsPlayerInSpecifiedRange(playerDetectionRange) && !IsPlayerInSpecifiedRange(attackRanged.attackRange))
        {
            jumpResetTimer = jumpTimer;
            Vector2 jumpVelocity = new Vector2(JumpDirection(), jumpForce);
            body.velocity += jumpVelocity;
            animator.SetBool("isJumping", true);
        }
        else
        {
            jumpResetTimer -= Time.deltaTime;
        }
    }

    private float JumpDirection()
    {
        bool isPlayerLeft = player.transform.position.x > body.transform.position.x;
        float horizontalJumpForce = isPlayerLeft ? 5f : -5f;
        return horizontalJumpForce;
    }

    private bool AllowJump()
    {
        Vector2 distance = transform.position - player.transform.position;
        if (Mathf.Abs(distance.y) > 1.1f)
        {
            return true;
        }
        return false;
    }

    public override bool IsCharacterTouchingGround()
    {
        if (body.IsTouchingLayers(LayerMask.GetMask("Ground")) && body.velocity.y < Mathf.Epsilon)
        {
            animator.SetBool("isJumping", false);
            return true;
        }
        return false;
    }
}