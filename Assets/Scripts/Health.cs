using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health;

    public void DealDamage(int damage)
    {
        health -= damage;
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    public void SetStartingHealth(int startingHealth)
    {
        health = startingHealth;
    }
}