using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerBody;
    private Animator playerAnimator;
    private SpriteRenderer playerSprite;
    private Attack attack;

    [SerializeField] float runSpeed;
    [SerializeField] float jumpForce;

    private int jumpCount;

    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        attack = GetComponent<Attack>();
    }

    private void Update()
    {
        Flip();
        Run();
        Jump();
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
            attack.SetSlashPosition(false);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            playerSprite.flipX = false;
            attack.SetSlashPosition(true);
        }
    }

    public bool IsPlayerTouchingGround()
    {
        if (playerBody.IsTouchingLayers(LayerMask.GetMask("Ground")) && playerBody.velocity.y < Mathf.Epsilon)
        {
            jumpCount = 2;
            playerAnimator.SetBool("isJumping", false);
            return true;
        }
        return false;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            jumpCount--;
            Vector2 playerJumpVelocity = new Vector2(0f, jumpForce);
            playerBody.velocity += playerJumpVelocity;
            playerAnimator.SetBool("isJumping", true);
        }
    }
}