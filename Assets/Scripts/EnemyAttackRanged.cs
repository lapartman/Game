using UnityEngine;

public class EnemyAttackRanged : Attack
{
    private EnemyMovement enemyMovement;
    public float attackRange;

    [SerializeField] GameObject spell;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        enemyMovement = GetComponent<EnemyMovement>();
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
            attackTimer = timeBetweenAttacks;
            Instantiate(spell, slashPosition.position, Quaternion.identity);
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
        float offset = facingRight ? 1f : -1f;
        slashPosition.position = new Vector2(transform.position.x + offset, transform.position.y);
    }
}