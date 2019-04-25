using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health;
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

    public void SetStartingHealth(int startingHealth)
    {
        health = startingHealth;
    }
}