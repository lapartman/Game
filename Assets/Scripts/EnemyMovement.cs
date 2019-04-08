using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    private PlayerMovement player;
    private CapsuleCollider2D characterCollider;
    private EnemyAttack attack;

    [SerializeField] float playerDetectionRange;

    private void Start()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        body = GetComponent<Rigidbody2D>();
        characterCollider = GetComponent<CapsuleCollider2D>();
        player = FindObjectOfType<PlayerMovement>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        attack = GetComponent<EnemyAttack>();
    }

    private void Update()
    {
        TriggerDeath();

        if (health.IsDead()) { return; }
        Move();
        Flip();
    }

    protected override void Move()
    {
        if (!IsPlayerInMeleeRange())
        {
            if (Vector2.Distance(transform.position, player.transform.position) <= playerDetectionRange)
            {
                animator.SetBool("isRunning", true);
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, runSpeed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
        }
    }

    private bool IsPlayerInMeleeRange()
    {
        return Vector2.Distance(transform.position, player.transform.position) <= attack.attackRange;
    }

    protected override void Flip()
    {
        if (player.transform.position.x < body.transform.position.x)
        {
            spriteRenderer.flipX = true;
            attack.SetSlashPosition(false);
        }
        else if (player.transform.position.x > body.transform.position.x)
        {
            spriteRenderer.flipX = false;
            attack.SetSlashPosition(true);
        }
    }

    protected override void TriggerDeath()
    {
        if (health.IsDead())
        {
            animator.SetTrigger("death");
            body.bodyType = RigidbodyType2D.Static;
            Destroy(characterCollider);
            Destroy(gameObject, 1.5f);
        }
    }
}