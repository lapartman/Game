using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    protected Rigidbody2D body;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected Health health;

    public float runSpeed;
    public float jumpForce;

    protected abstract void Move();
    protected abstract void Flip();
    protected abstract void TriggerDeath();
    protected abstract void Jump();
    public abstract bool IsCharacterTouchingGround();
}