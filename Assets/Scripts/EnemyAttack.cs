using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : Attack
{
    [SerializeField] float attackRange;
    Health health;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
    }

    private void Update()
    {
        if (health.IsDead()) { return; }
        Slash();
        Debug.Log(Vector2.Distance(transform.position, player.transform.position));
    }

    protected override void Slash()
    {
        if (SlashCondition())
        {
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
        }
    }

    protected override bool SlashCondition()
    {
        return attackTimer <= 0 && player.IsPlayerTouchingGround() && Vector2.Distance(transform.position, player.transform.position) <= attackRange;
    }
}