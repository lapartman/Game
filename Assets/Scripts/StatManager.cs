using UnityEngine;

public class StatManager : MonoBehaviour
{
    private GameManager gameManager;
    private AbilityTextDisplay scoreDisplay;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        scoreDisplay = FindObjectOfType<AbilityTextDisplay>();
        scoreDisplay.DisplayValue();
    }

    /// <summary>
    /// Megnöveli az játékos életét, frissíti a képernyőn az értéket.
    /// </summary>
    public void IncreaseHealth()
    {
        if (gameManager.HaveEnoughAbilityPoints())
        {
            gameManager.playerHealth += 1;
            gameManager.AbilityPoints -= 1;
            scoreDisplay.DisplayValue();
        }
    }

    /// <summary>
    /// Megnöveli a játékos sebzését, frissíti a képernyőn az értéket.
    /// </summary>
    public void IncreaseDamage()
    {
        if (gameManager.HaveEnoughAbilityPoints())
        {
            gameManager.playerDamage += 1;
            gameManager.AbilityPoints -= 1;
            scoreDisplay.DisplayValue();
        }
    }
}