using UnityEngine;

public class PlayerAttack : Attack
{
    private GameManager gameManager;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] float attackRadius;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<PlayerMovement>();
        health = GetComponent<Health>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        Slash();
    }

    protected override void Slash()
    {
        if (SlashCondition())
        {
            animator.SetTrigger("slash");
            Collider2D enemyCollider = Physics2D.OverlapCircle(slashPosition.position, attackRadius, enemyMask);
            attackTimer = timeBetweenAttacks;
            if (enemyCollider == null) { return; }
            if (enemyCollider.GetComponent<EnemyMovementMelee>() || enemyCollider.GetComponent<EnemyMovementRanged>())
            {
                enemyCollider.GetComponent<Health>().DealDamage(gameManager.playerDamage);
                enemyCollider.GetComponent<Animator>().SetTrigger("hurt");
            }
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }
    }

    protected override bool SlashCondition()
    {
        return Input.GetKeyDown(KeyCode.Mouse0) && attackTimer <= 0 && player.IsCharacterTouchingGround() && !health.IsDead();
    }

    public override void SetSlashPosition(bool facingRight)
    {
        float offset = facingRight ? 1.25f : -1.25f;
        slashPosition.position = new Vector2(transform.position.x + offset, transform.position.y);
    }
}