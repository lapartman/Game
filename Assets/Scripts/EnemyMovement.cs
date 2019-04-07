using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private PlayerMovement player;

    private Animator enemyAnimator;
    private Health health;
    private Rigidbody2D characterBody;
    private CapsuleCollider2D characterCollider;
    private SpriteRenderer characterSprite;
    private EnemyAttack attack;

    [SerializeField] float runSpeed;
    [SerializeField] float playerDetectionRange;

    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        health = GetComponent<Health>();
        characterBody = GetComponent<Rigidbody2D>();
        characterCollider = GetComponent<CapsuleCollider2D>();
        player = FindObjectOfType<PlayerMovement>();
        characterSprite = GetComponentInChildren<SpriteRenderer>();
        attack = GetComponent<EnemyAttack>();
    }

    void Update()
    {
        TriggerDeath();
        
        if (health.IsDead()) { return; }
        PlayerInRange();
        Flip();
    }

    private void PlayerInRange()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= playerDetectionRange)
        {
            enemyAnimator.SetBool("isRunning", true);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, runSpeed * Time.deltaTime);
        }
        else
        {
            enemyAnimator.SetBool("isRunning", false);
        }
    }

    private void Flip()
    {
        if (player.transform.position.x < characterBody.transform.position.x)
        {
            characterSprite.flipX = true;
            attack.SetSlashPosition(false);
        }
        else if (player.transform.position.x > characterBody.transform.position.x)
        {
            characterSprite.flipX = false;
            attack.SetSlashPosition(true);
        }
    }

    private void TriggerDeath()
    {
        if (health.IsDead())
        {
            enemyAnimator.SetTrigger("death");
            characterBody.bodyType = RigidbodyType2D.Static;
            Destroy(characterCollider);
            Destroy(gameObject, 1.5f);
        }
    }
}