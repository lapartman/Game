using System.Collections;
using UnityEngine;

public class EnemyMovementRanged : Movement
{
    private PlayerMovement player;
    private CapsuleCollider2D characterCollider;
    private EnemyAttackRanged attackRanged;
    private GameManager gameManager;

    [SerializeField] float playerDetectionRange;
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
        //Megegyezik a közelharcoséval.
        if (health.IsDead() || body.IsTouchingLayers(LayerMask.GetMask("Trap")))
        {
            StartCoroutine(TriggerDeath());
            return;
        }
        IsCharacterTouchingGround();
        Move();
        Flip();
        Jump();
    }

    //Megegyezik a közelharcoséval.
    private void OnDestroy()
    {
        if (health.IsDead())
        {
            gameManager.AddToTotalScore(scoreValue);
            FindObjectOfType<ScoreTextDisplay>().DisplayValue();
        }
    }

    //Megegyezik a közelharcoséval.
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

    //Megegyezik a közelharcoséval.
    public bool IsPlayerInSpecifiedRange(float rangeType)
    {
        return Vector2.Distance(transform.position, player.transform.position) <= rangeType;
    }

    /// <summary>
    /// Megegyezik a közelharcoséval, azzal a kivétellel, hogy itt egy property-t is átállít igaz vagy hamisra, ennek a tűzcsóva irányának meghatározásában van szerepe.
    /// </summary>
    protected override void Flip()
    {
        if (player.transform.position.x < body.transform.position.x)
        {
            spriteRenderer.flipX = true;
            attackRanged.SetSlashPosition(true);
            IsCharacterFlipped = true;
        }
        else if (player.transform.position.x > body.transform.position.x)
        {
            spriteRenderer.flipX = false;
            attackRanged.SetSlashPosition(false);
            IsCharacterFlipped = false;
        }
    }

    //Megegyezik a közelharcoséval.
    protected override IEnumerator TriggerDeath()
    {
        yield return new WaitForSeconds(0f);
        animator.SetTrigger("death");
        body.bodyType = RigidbodyType2D.Static;
        Destroy(characterCollider);
        Destroy(gameObject, deathDelay);
    }

    //Megegyezik a közelharcoséval.
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

    //Megegyezik a közelharcoséval.
    private float JumpDirection()
    {
        bool isPlayerLeft = player.transform.position.x > body.transform.position.x;
        float horizontalJumpForce = isPlayerLeft ? 5f : -5f;
        return horizontalJumpForce;
    }

    //Megegyezik a közelharcoséval.
    private bool AllowJump()
    {
        Vector2 distance = transform.position - player.transform.position;
        if (Mathf.Abs(distance.y) > 1.1f)
        {
            return true;
        }
        return false;
    }

    //Megegyezik a közelharcoséval.
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