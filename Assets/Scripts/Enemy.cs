using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private Health health;

    void Start()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
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
            Destroy(gameObject, 2);
        }
    }
}