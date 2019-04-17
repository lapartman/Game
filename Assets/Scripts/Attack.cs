using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public float timeBetweenAttacks;
    public float attackRadius;

    public Transform slashPosition;
    public LayerMask enemyMask;

    protected Animator animator;
    protected PlayerMovement player;
    protected Health health;

    protected float attackTimer;

    protected abstract void Slash();
    protected abstract bool SlashCondition();

    public virtual void SetSlashPosition(bool facingRight)
    {
        float offset = facingRight ? 1.25f : -1.25f;
        slashPosition.position = new Vector2(transform.position.x + offset, transform.position.y);
    }
}