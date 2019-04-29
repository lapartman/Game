using UnityEngine;

public class EnemyAttackRanged : Attack
{
    private EnemyMovementRanged enemyMovement;
    public float attackRange;

    [SerializeField] GameObject fireSpell;
    [SerializeField] AudioClip[] spellSounds;
    GameObject spellReference;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        enemyMovement = GetComponent<EnemyMovementRanged>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (health.IsDead()) { return; }
        Slash();
    }

    /// <summary>
    /// Megadja a mágus támadását, ha támadhat, akkor megidézi a tűzcsóvát, ha nem, akkor csökken a cooldown ideje, hogy újra támadhasson.
    /// </summary>
    protected override void Slash()
    {
        if (SlashCondition())
        {
            animator.SetBool("isRunning", false);
            animator.SetTrigger("slash");
            attackTimer = timeBetweenAttacks;
            spellReference = Instantiate(fireSpell, slashPosition.position, Quaternion.identity);
            audioSource.PlayOneShot(PlayRandomSound(spellSounds));
            spellReference.GetComponent<Rigidbody2D>().velocity = FireSpellDirection();
        }
        else
        {
            attackTimer -= Time.deltaTime;
            animator.SetBool("isRunning", false);
        }
    }

    /// <summary>
    /// Megadja a támadáshoz szükséges paramétereket, ha teljesülnek, akkor támadhat.
    /// </summary>
    /// <returns>Ha igaz, akkor támadásba lendülhet.</returns>
    protected override bool SlashCondition()
    {
        return attackTimer <= 0 && player.IsCharacterTouchingGround() && enemyMovement.IsPlayerInSpecifiedRange(attackRange) && !player.GetComponent<Health>().IsDead() && Mathf.Abs(transform.position.y - player.transform.position.y) < 1.1f;
    }

    /// <summary>
    /// Beállítja a tűzcsóva kiindulási pozícióját annak függvényében, hogy merre néz a mágus.
    /// </summary>
    /// <param name="facingRight">A karakter jobbra néz-e.</param>
    public override void SetSlashPosition(bool facingRight)
    {
        float offset = facingRight ? -1f : 1f;
        slashPosition.position = new Vector2(transform.position.x + offset, transform.position.y);
    }

    /// <summary>
    /// Beállítja a tűzcsóva, és a tűzcsóva animáció irányát attól függően, hogy a mágus sprite-ja meg van-e fordítva.
    /// </summary>
    /// <returns>Visszatér a feltétel alapján a helyes, játékos felé néző iránnyal.</returns>
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