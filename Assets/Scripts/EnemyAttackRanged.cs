using UnityEngine;

public class EnemyAttackRanged : Attack
{
    private EnemyMovementRanged enemyMovement;
    public float attackRange;

    [SerializeField] GameObject spell;
    [SerializeField] float spellSpeed;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        enemyMovement = GetComponent<EnemyMovementRanged>();
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
            GameObject fireSpell = Instantiate(spell, slashPosition.position, Quaternion.identity);
            fireSpell.GetComponent<Rigidbody2D>().velocity = FireSpellDirection();
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

    private Vector2 FireSpellDirection()
    {
        Vector2 direction = new Vector2();
        if (enemyMovement.isCharacterFlipped)
        {
            direction = new Vector2(5f, 0f);
        }
        else if (!enemyMovement.isCharacterFlipped)
        {
            direction = -new Vector2(5f, 0f);
        }
        return direction;
    }
}