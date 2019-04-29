using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    /// <summary>
    /// Támadáskor hívódik meg, a karakter ellenségének az életéből veszi le a meghatározott értéket.
    /// </summary>
    /// <param name="damage">A támadás erőssége.</param>
    public void DealDamage(int damage)
    {
        health -= damage;
    }

    /// <summary>
    /// Halott-e a karakter.
    /// </summary>
    /// <returns>Ha kevesebb vagy egyenlő az életpontja, mint 0, akkor igazzal tér vissza, különben hamis.</returns>
    public bool IsDead()
    {
        return health <= 0;
    }

    /// <summary>
    /// Beállítja a játékos életét a játék kezdetekor.
    /// </summary>
    /// <param name="startingHealth">Mennyi élete van a játékosnak.</param>
    public void SetHealth(int startingHealth)
    {
        health = startingHealth;
    }
}