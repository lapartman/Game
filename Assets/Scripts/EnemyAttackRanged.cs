using UnityEngine;

public class EnemyAttackRanged : Attack
{
    private EnemyMovementRanged enemyMovement;
    public float attackRange;

    [SerializeField] GameObject fireSpell;
    GameObject spellReference;

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
            spellReference = Instantiate(fireSpell, slashPosition.position, Quaternion.identity);
            spellReference.GetComponent<Rigidbody2D>().velocity = FireSpellDirection();
        }
        else
        {
            attackTimer -= Time.deltaTime;
            animator.SetBool("isRunning", false);
        }
    }

    protected override bool SlashCondition()
    {
        return attackTimer <= 0 && player.IsCharacterTouchingGround() && enemyMovement.IsPlayerInSpecifiedRange(attackRange) && !player.GetComponent<Health>().IsDead() && Mathf.Abs(transform.position.y - player.transform.position.y) < 1.1f;
    }

    public override void SetSlashPosition(bool facingRight)
    {
        float offset = facingRight ? -1f : 1f;
        slashPosition.position = new Vector2(transform.position.x + offset, transform.position.y);
    }

    private Vector2 FireSpellDirection()
    {
        Vector2 direction = new Vector2();
        switch (enemyMovement.IsCharacterFlipped)
        {
            case true:
                direction = -new Vector2(5f, 0f);
                spellReference.GetComponent<SpriteRenderer>().flipX = true;
                break;
            case false:
                direction = new Vector2(5f, 0f);
                spellReference.GetComponent<SpriteRenderer>().flipX = false;
                break;
        }
        return direction;
    }
}