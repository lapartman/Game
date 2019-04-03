﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Animator playerAnimator;
    [SerializeField] LayerMask enemyMask;

    [SerializeField] int damage;
    [SerializeField] float timeBetweenAttacks;
    [SerializeField] float attackRadius;
    [SerializeField] Transform slashPosition;

    private float attackTimer;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Slash();
    }

    private void Slash()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && attackTimer <= 0)
        {
            playerAnimator.SetTrigger("slash");
            Collider2D[] enemies = Physics2D.OverlapCircleAll(slashPosition.position, attackRadius, enemyMask);
            foreach (Collider2D enemy in enemies)
            {
                if (enemy.GetComponent<Enemy>())
                {
                    enemy.GetComponent<Health>().DealDamage(damage);
                }
            }
            attackTimer = timeBetweenAttacks;

        }
        else
        {
            attackTimer -= Time.deltaTime;
        }
    }

    public void SetSlashPosition(bool facingRight)
    {
        float offset = facingRight ? 1.25f : -1.25f;
        slashPosition.position = new Vector2(transform.position.x + offset, transform.position.y);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(slashPosition.position, attackRadius);
    }
}