using UnityEngine;

public class EnemyAttackMelee : Attack
{
    private EnemyMovementMelee enemyMovement;
    public float attackRange;

    [SerializeField] LayerMask enemyMask;
    [SerializeField] int damage;
    [SerializeField] float attackRadius;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        enemyMovement = GetComponent<EnemyMovementMelee>();
    }

    private void Update()
    {
        if (health.IsDead()) { return; }
        Slash();
    }

    protected override void Slash()
    {
        if (SlashCondition())
        {
            animator.SetBool("isRunning", false);
            animator.SetTrigger("slash");
            Collider2D enemyCollider = Physics2D.OverlapCircle(slashPosition.position, attackRadius, enemyMask);
            attackTimer = timeBetweenAttacks;
            if (enemyCollider == null) { return; }
            if (enemyCollider.GetComponent<PlayerMovement>())
            {
                enemyCollider.GetComponent<Health>().DealDamage(damage);
                enemyCollider.GetComponent<Animator>().SetTrigger("hurt");
            }
        }
        else
        {
            attackTimer -= Time.deltaTime;
            animator.SetBool("isRunning", false);
        }
    }

    protected override bool SlashCondition()
    {
        return attackTimer <= 0 && player.IsCharacterTouchingGround() && enemyMovement.IsPlayerInSpecifiedRange(attackRange) && !player.GetComponent<Health>().IsDead();
    }

    public override void SetSlashPosition(bool facingRight)
    {
        float offset = facingRight ? -1f : 1f;
        slashPosition.position = new Vector2(transform.position.x + offset, transform.position.y);
    }
}