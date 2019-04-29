using UnityEngine;

public class EnemyAttackMelee : Attack
{
    private EnemyMovementMelee enemyMovement;
    public float attackRange;

    [SerializeField] LayerMask enemyMask;
    [SerializeField] int damage;
    [SerializeField] float attackRadius;
    [SerializeField] AudioClip[] noImpactSlashSounds;
    [SerializeField] AudioClip[] hitSounds;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        enemyMovement = GetComponent<EnemyMovementMelee>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (health.IsDead()) { return; }
        Slash();
    }

    /// <summary>
    /// Megadja az ellenség támadását, ha támadhat, akkor támad, ha nem, akkor meg a támadások közti cooldown ideje csökken, hogy újra támadhasson.
    /// </summary>
    protected override void Slash()
    {
        if (SlashCondition())
        {
            animator.SetBool("isRunning", false);
            animator.SetTrigger("slash");
            Collider2D enemyCollider = Physics2D.OverlapCircle(slashPosition.position, attackRadius, enemyMask);
            attackTimer = timeBetweenAttacks;
            if (enemyCollider == null)
            {
                audioSource.PlayOneShot(PlayRandomSound(noImpactSlashSounds));
                return;
            }
            if (enemyCollider.GetComponent<PlayerMovement>())
            {
                audioSource.PlayOneShot(PlayRandomSound(hitSounds));
                enemyCollider.GetComponent<Health>().DealDamage(damage);
                FindObjectOfType<HealthTextDisplay>().DisplayValue();
            }
        }
        else
        {
            attackTimer -= Time.deltaTime;
            animator.SetBool("isRunning", false);
        }
    }

    /// <summary>
    /// Megadja a támadás feltételeit, és ha mindegyik teljesül, az ellenség támadhat.
    /// </summary>
    /// <returns>Ha teljesülnek a feltételek, akkor támadhat</returns>
    protected override bool SlashCondition()
    {
        return attackTimer <= 0 && player.IsCharacterTouchingGround() && enemyMovement.IsPlayerInSpecifiedRange(attackRange) && !player.GetComponent<Health>().IsDead();
    }

    /// <summary>
    /// Megadja, hogy jobbra néz-e a karakter. Ha nem, akkor a támadás pozícióját átrakja a karakter másik oldalára.
    /// </summary>
    /// <param name="facingRight">A karakter jobbra néz-e</param>
    public override void SetSlashPosition(bool facingRight)
    {
        float offset = facingRight ? -1f : 1f;
        slashPosition.position = new Vector2(transform.position.x + offset, transform.position.y);
    }
}