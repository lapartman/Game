using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    [SerializeField] float timeBetweenAttacks;
    [SerializeField] float attackRadius;
    [SerializeField] int damage;

    [SerializeField] Transform slashPosition;
    [SerializeField] LayerMask enemyMask;

    protected Animator animator;
    protected PlayerMovement player;

    protected float attackTimer;

    protected abstract void Slash();
    protected abstract bool SlashCondition();
    protected abstract void SetSlashPosition(bool facingRight);

    protected void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<PlayerMovement>();
    }

    protected void Update()
    {
        Slash();
    }
}
