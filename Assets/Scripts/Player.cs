﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D playerBody;
    Animator playerAnimator;
    SpriteRenderer playerSprite;

    [SerializeField] float runSpeed;
    [SerializeField] float jumpForce;

    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        Jump();
        Run();
    }

    private void Update()
    {
        Flip();
        IsPlayerTouchingGround();
    }

    private void Run()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        Vector2 playerMovementVelocity = new Vector2(horizontalAxis * runSpeed, playerBody.velocity.y);
        playerBody.velocity = playerMovementVelocity;

        bool isPlayerMoving = Mathf.Abs(playerBody.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("isRunning", isPlayerMoving);
    }

    private void Flip()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            playerSprite.flipX = true;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            playerSprite.flipX = false;
        }
    }

    private void IsPlayerTouchingGround()
    {
        if (playerBody.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            playerAnimator.SetBool("isJumping", false);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 playerJumpVelocity = new Vector2(0f, jumpForce);
            playerBody.velocity += playerJumpVelocity;
            playerAnimator.SetBool("isJumping", true);
        }
    }
}