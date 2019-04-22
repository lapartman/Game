using System.Collections;
using UnityEngine;

public class EnemyMovementMelee : Movement
{
    private PlayerMovement player;
    private CapsuleCollider2D characterCollider;
    private GameManager gameManager;
    private EnemyAttackMelee attackMelee;

    [SerializeField] float playerDetectionRange;
    [SerializeField] int abilityValue;
    [SerializeField] float jumpTimer;
    [SerializeField] int scoreValue;

    private float jumpResetTimer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        body = GetComponent<Rigidbody2D>();
        characterCollider = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        attackMelee = GetComponent<EnemyAttackMelee>();
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        IsCharacterTouchingGround();
        if (health.IsDead())
        {
            StartCoroutine(TriggerDeath());
            return;
        }
        Move();
        Flip();
        Jump();
    }

    protected override void Move()
    {
        if (IsPlayerInSpecifiedRange(playerDetectionRange) && !IsPlayerInSpecifiedRange(attackMelee.attackRange))
        {
            animator.SetBool("isRunning", true);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, runSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    protected override void Flip()
    {
        if (player.transform.position.x < body.transform.position.x)
        {
            spriteRenderer.flipX = true;
            attackMelee.SetSlashPosition(true);
        }
        else if (player.transform.position.x > body.transform.position.x)
        {
            spriteRenderer.flipX = false;
            attackMelee.SetSlashPosition(false);
        }
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

    protected override void Jump()
    {
        if (AllowJump() && jumpResetTimer <= 0f && IsPlayerInSpecifiedRange(playerDetectionRange) && !IsPlayerInSpecifiedRange(attackMelee.attackRange))
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

    protected override IEnumerator TriggerDeath()
    {
        animator.SetTrigger("death");
        body.bodyType = RigidbodyType2D.Static;
        Destroy(characterCollider);
        Destroy(gameObject, deathDelay);
        yield return new WaitForSeconds(deathDelay - 0.02f);
        gameManager.AddAbilityPoints(abilityValue);
        gameManager.AddToTotalScore(scoreValue);
        FindObjectOfType<ScoreDisplay>().DisplayPoints();
    }

    public bool IsPlayerInSpecifiedRange(float rangeType)
    {
        return Vector2.Distance(transform.position, player.transform.position) <= rangeType;
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
        if (Mathf.Abs(distance.y) > 0.1f)
        {
            return true;
        }
        return false;
    }
}