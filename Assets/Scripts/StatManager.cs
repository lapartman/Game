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

    public void IncreaseHealth()
    {
        if (gameManager.HaveEnoughAbilityPoints())
        {
            gameManager.playerHealth += 1;
            gameManager.AbilityPoints -= 1;
            scoreDisplay.DisplayValue();
        }
    }

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