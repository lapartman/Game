using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private Health health;
    private Rigidbody2D enemyBody;
    private CapsuleCollider2D enemyCollider;

    void Start()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        enemyBody = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        TriggerDeath();
    }

    private void TriggerDeath()
    {
        if (health.IsDead())
        {
            animator.SetTrigger("death");
            enemyBody.bodyType = RigidbodyType2D.Static;
            Destroy(enemyCollider);
            Destroy(gameObject, 1.5f);
        }
    }
}