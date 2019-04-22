using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public float timeBetweenAttacks;
    public Transform slashPosition;

    protected Animator animator;
    protected PlayerMovement player;
    protected Health health;

    protected float attackTimer;

    protected abstract void Slash();
    protected abstract bool SlashCondition();
    public abstract void SetSlashPosition(bool facingRight);
}