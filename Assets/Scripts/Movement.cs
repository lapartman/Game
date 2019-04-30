using System.Collections;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    protected Rigidbody2D body;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected Health health;
    protected AudioSource audioSource;
    protected bool isTrapSoundEffectPlaying = false;

    public float runSpeed;
    public float jumpForce;
    public float deathDelay;
    public AudioClip trapSoundEffect;

    /// <summary>
    /// Karakter mozgása.
    /// </summary>
    protected abstract void Move();

    /// <summary>
    /// Karakter/támadási terület beállítása.
    /// </summary>
    /// 
    protected abstract void Flip();

    /// <summary>
    /// Lekezeli a karakter halálát.
    /// </summary>
    /// <returns>Késleltetés mértéke.</returns>
    protected abstract IEnumerator TriggerDeath();

    /// <summary>
    /// Karakter ugrása.
    /// </summary>
    protected abstract void Jump();

    /// <summary>
    /// A karakter érinti-e a talajt.
    /// </summary>
    /// <returns>Ha érinti a talajt, igazzal tér vissza.</returns>
    public abstract bool IsCharacterTouchingGround();
}