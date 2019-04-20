﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementRanged : Movement
{
    private PlayerMovement player;
    private CapsuleCollider2D characterCollider;
    private Vector2 distance;
    private GameManager gameManager;
    private EnemyAttackRanged attackRanged;

    [SerializeField] float playerDetectionRange;
    [SerializeField] int abilityValue;
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
        attackRanged = GetComponent<EnemyAttackRanged>();
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        IsCharacterTouchingGround();
        if (health.IsDead())
        {
            StartCoroutine(TriggerDeath());
            return;
        }
        Move();
        Flip();
        Jump();
    }

    protected override void Move()
    {
        if (IsPlayerInSpecifiedRange(playerDetectionRange) && !IsPlayerInSpecifiedRange(attackRanged.attackRange))
        {
            animator.SetBool("isRunning", true);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, runSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    public bool IsPlayerInSpecifiedRange(float rangeType)
    {
        return Vector2.Distance(transform.position, player.transform.position) <= rangeType;
    }

    protected override void Flip()
    {
        if (player.transform.position.x < body.transform.position.x)
        {
            spriteRenderer.flipX = true;
            attackRanged.SetSlashPosition(false);
        }
        else if (player.transform.position.x > body.transform.position.x)
        {
            spriteRenderer.flipX = false;
            attackRanged.SetSlashPosition(true);
        }
    }

    protected override IEnumerator TriggerDeath()
    {
        animator.SetTrigger("death");
        body.bodyType = RigidbodyType2D.Static;
        Destroy(characterCollider);
        Destroy(gameObject, 1.5f);
        yield return new WaitForSeconds(1.48f);
        gameManager.AddAbilityPoints(abilityValue);
        gameManager.AddToTotalScore(scoreValue);
        FindObjectOfType<ScoreDisplay>().DisplayPoints();
    }

    protected override void Jump()
    {
        if (AllowJump() && jumpResetTimer <= 0f && IsPlayerInSpecifiedRange(playerDetectionRange) && !IsPlayerInSpecifiedRange(attackRanged.attackRange))
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

    private float JumpDirection()
    {
        bool isPlayerLeft = player.transform.position.x > body.transform.position.x;
        float horizontalJumpForce = isPlayerLeft ? 5f : -5f;
        return horizontalJumpForce;
    }

    private bool AllowJump()
    {
        distance = transform.position - player.transform.position;
        if (Mathf.Abs(distance.y) > 0.1f)
        {
            return true;
        }
        return false;
    }

    public override bool IsCharacterTouchingGround()
    {
        if (body.IsTouchingLayers(LayerMask.GetMask("Ground")) && body.velocity.y < Mathf.Epsilon)
        {
            animator.SetBool("isJumping", false);
            return true;
        }
        return false;
    }
}
