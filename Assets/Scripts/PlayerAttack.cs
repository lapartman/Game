using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Attack
{
    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<PlayerMovement>();
        health = GetComponent<Health>();
    }

    private void Update()
    {
        Slash();
    }

    protected override void Slash()
    {
        if (SlashCondition() && !health.IsDead())
        {
            animator.SetTrigger("slash");
            Collider2D enemyCollider = Physics2D.OverlapCircle(slashPosition.position, attackRadius, enemyMask);
            attackTimer = timeBetweenAttacks;
            if (enemyCollider == null) { return; }
            if (enemyCollider.GetComponent<EnemyMovement>())
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
        return Input.GetKeyDown(KeyCode.Mouse0) && attackTimer <= 0 && player.IsPlayerTouchingGround();
    }
}