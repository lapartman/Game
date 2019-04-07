using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator playerAnimator;
    private PlayerMovement player;

    [SerializeField] LayerMask enemyMask;

    [SerializeField] int damage;
    [SerializeField] float timeBetweenAttacks;
    [SerializeField] float attackRadius;
    [SerializeField] Transform slashPosition;

    private float attackTimer;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        player = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        Slash();
    }

    private void Slash()
    {
        if (SlashCondition())
        {
            playerAnimator.SetTrigger("slash");
            Collider2D enemyCollider = Physics2D.OverlapCircle(slashPosition.position, attackRadius, enemyMask);
            attackTimer = timeBetweenAttacks;
            if (enemyCollider == null) { return; }
            if (enemyCollider.GetComponent<EnemyMovement>())
            {
                enemyCollider.GetComponent<Health>().DealDamage(damage);
                enemyCollider.GetComponent<Animator>().SetTrigger("hurt");
            }
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }
    }

    private bool SlashCondition()
    {
        return Input.GetKeyDown(KeyCode.Mouse0) && attackTimer <= 0 && player.IsPlayerTouchingGround();
    }

    public void SetSlashPosition(bool facingRight)
    {
        float offset = facingRight ? 1.25f : -1.25f;
        slashPosition.position = new Vector2(transform.position.x + offset, transform.position.y);
    }
}