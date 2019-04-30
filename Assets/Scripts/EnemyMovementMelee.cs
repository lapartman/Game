using System.Collections;
using UnityEngine;

public class EnemyMovementMelee : Movement
{
    private PlayerMovement player;
    private CapsuleCollider2D characterCollider;
    private GameManager gameManager;
    private EnemyAttackMelee attackMelee;

    [SerializeField] float playerDetectionRange;
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
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //Ha a karakter halott, vagy hozzáért a csapdához, akkor megkezdődik a halált lekezelő metódus, és ettől a ponttól visszatér, hogy az ellenség ne tudjon mozogni.
        if (health.IsDead())
        {
            StartCoroutine(TriggerDeath());
            return;
        }
        if (body.IsTouchingLayers(LayerMask.GetMask("Trap")))
        {
            if (!isTrapSoundEffectPlaying)
            {
                isTrapSoundEffectPlaying = true;
                audioSource.PlayOneShot(trapSoundEffect);
                StartCoroutine(TriggerDeath());
            }
            return;
        }
        IsCharacterTouchingGround();
        Move();
        Flip();
        Jump();
    }

    /// <summary>
    /// Ha megsemmisül a karakter, a pontok hozzáadódnak a GameManager-hez.
    /// </summary>
    private void OnDestroy()
    {
        if (health.IsDead())
        {
            gameManager.TotalScore += scoreValue;
            FindObjectOfType<ScoreTextDisplay>().DisplayValue();
        }
    }

    /// <summary>
    /// Megadja a mozgását a közelharcos ellenségnek, beállítja a futás animációt.
    /// </summary>
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

    /// <summary>
    /// Megfordítja az ellenség sprite-ját, és a támadási pozícióját a másik oldalra.
    /// </summary>
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

    /// <summary>
    /// A karakter a földön tartózkodik-e ebben a pillanatban.
    /// </summary>
    /// <returns>Igaz vagy hamisssal tér vissza, attól függően, hogy a karakter a földön van-e éppen.</returns>
    public override bool IsCharacterTouchingGround()
    {
        if (body.IsTouchingLayers(LayerMask.GetMask("Ground")) && body.velocity.y < Mathf.Epsilon)
        {
            animator.SetBool("isJumping", false);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Megadja a karakter ugrását, és beállítja az ugrás animációját.
    /// </summary>
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

    /// <summary>
    /// Megadja mi történjen akkor, ha a karakter meghal, a test típusa statikussá változik, és megsemmisül az ütközője, hogy a játékos át tudjon rajta haladni.
    /// </summary>
    /// <returns>Késleltetéssel tér vissza, de mivel csak a játékos halála esetében kell késleltetni, itt ez az érték 0.</returns>
    protected override IEnumerator TriggerDeath()
    {
        yield return new WaitForSeconds(0f);
        animator.SetTrigger("death");
        body.bodyType = RigidbodyType2D.Static;
        Destroy(characterCollider);
        Destroy(gameObject, deathDelay);
    }

    /// <summary>
    /// A játékos benne van-e a paraméterben átadott pozícióban.
    /// </summary>
    /// <param name="rangeType">Kétféle típus van, az attackRange és a detectionRange, az előbbi azt adja meg, hogy a játékos elég közel van-e ahhoz, hogy az ellenség meg tudja támadni, a detectionRange, meg hogy elég közel van-e a játékos, hogy az ellenség észlelje, és elkezdjen felé futni.</param>
    /// <returns>Megadja, hogy a paraméterben megadott pozíciónál közelebb van-e a játékos, ez alapján tér vissza igazzal, vagy hamissal.</returns>
    public bool IsPlayerInSpecifiedRange(float rangeType)
    {
        return Vector2.Distance(transform.position, player.transform.position) <= rangeType;
    }

    /// <summary>
    /// Megadja az ellenség X tengelyén lévő ugrási erejét, illetve, hogy melyik oldalra ugorjon attól függően, hogy melyik oldalon van a játékos.
    /// </summary>
    /// <returns>Az ugrás erejét.</returns>
    private float JumpDirection()
    {
        bool isPlayerLeft = player.transform.position.x > body.transform.position.x;
        float horizontalJumpForce = isPlayerLeft ? 7f : -7f;
        return horizontalJumpForce;
    }

    /// <summary>
    /// Megadja, hogy a játékos, és az ellenség közötti távolság értéke meghaladja a 0.1f-et, ha igen, akkor engedélyezve van az ugrás ezen feltétele.
    /// </summary>
    /// <returns>Igazzal tér vissza, ha a felüli feltétel teljesül.</returns>
    private bool AllowJump()
    {
        Vector2 distance = transform.position - player.transform.position;
        if (Mathf.Abs(distance.y) > .1f)
        {
            return true;
        }
        return false;
    }
}