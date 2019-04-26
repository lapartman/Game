using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void DealDamage(int damage)
    {
        health -= damage;
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    public void SetHealth(int startingHealth)
    {
        health = startingHealth;
    }
}