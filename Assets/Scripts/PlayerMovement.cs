using System.Collections;
using UnityEngine;

public class PlayerMovement : Movement
{
    private PlayerAttack attack;

    private int jumpCount;
    public int playerKeyCount = 0;

    private GameManager gameManager;
    private BoxCollider2D feetCollider;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        attack = GetComponent<PlayerAttack>();
        health = GetComponent<Health>();
        gameManager = FindObjectOfType<GameManager>();
        feetCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        health.SetHealth(gameManager.playerHealth);
        FindObjectOfType<HealthTextDisplay>().DisplayValue();
        FindObjectOfType<LivesTextDisplay>().DisplayValue();
    }

    private void Update()
    {
        //Ha hozzáér a csapdához, beállítja a játékos életét 0-ra.
        if (body.IsTouchingLayers(LayerMask.GetMask("Trap")))
        {
            health.SetHealth(0);
            if (!isTrapSoundEffectPlaying)
            {
                isTrapSoundEffectPlaying = true;
                audioSource.PlayOneShot(trapSoundEffect);
            }
        }
        if (health.IsDead())
        {
            StartCoroutine(TriggerDeath());
            return;
        }
        Flip();
        Move();
        Jump();
        IsCharacterTouchingGround();
    }

    /// <summary>
    /// Játékos mozgását, és animációnak feltételét kezeli le.
    /// </summary>
    protected override void Move()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        Vector2 playerMovementVelocity = new Vector2(horizontalAxis * runSpeed, body.velocity.y);
        body.velocity = playerMovementVelocity;

        bool isPlayerMoving = Mathf.Abs(body.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isRunning", isPlayerMoving);
    }

    /// <summary>
    /// Megváltoztatja a karakter irányát, illetve a támadási terület irányát is a másik oldalra, attól függően, merre néz a játékos.
    /// </summary>
    protected override void Flip()
    {
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.RightArrow)))
        {
            spriteRenderer.flipX = true;
            attack.SetSlashPosition(false);
        }
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftArrow)))
        {
            spriteRenderer.flipX = false;
            attack.SetSlashPosition(true);
        }
    }

    /// <summary>
    /// A karakter a földön tartózkodik-e éppen.
    /// </summary>
    /// <returns>Amennyiben igaz, kikapcsolja az ugrás animációt, és az ugrás lehetőségeinek a számát frissíti 2-re.</returns>
    public override bool IsCharacterTouchingGround()
    {
        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && body.velocity.y < Mathf.Epsilon)
        {
            jumpCount = 2;
            animator.SetBool("isJumping", false);
            return true;
        }
        return false;
    }
    
    /// <summary>
    /// Játékos ugrását adja meg, csökkenti az ugráslehetőségek számát.
    /// </summary>
    protected override void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0 && !feetCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            jumpCount--;
            Vector2 playerJumpVelocity = new Vector2(0f, jumpForce);
            body.velocity += playerJumpVelocity;
            animator.SetBool("isJumping", true);
        }
    }
    
    /// <summary>
    /// Játékos életpontjainak elfogytakor csökken az életének a száma, ha a maradandó életeinek száma nem egyenlő nullával, akkor újratölti a jelenlegi pályát, ha elérte a 0-át, akkor betölti a vesztes jelenetet. 
    /// </summary>
    private void OnDestroy()
    {
        if (health.IsDead())
        {
            gameManager.playerLives--;
            if (gameManager.playerLives != 0)
            {
                FindObjectOfType<LevelManager>().LoadCurrentLevel();
            }
            else
            {
                FindObjectOfType<LevelManager>().LoadLoseScreen();
            }
        }
    }

    /// <summary>
    /// Lekezeli a játékos halálát, megsemmisíti a játékost. 
    /// </summary>
    /// <returns>Késleltetés mértéke</returns>
    protected override IEnumerator TriggerDeath()
    {
        animator.SetTrigger("death");
        body.velocity = new Vector2(0f, 0f);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}