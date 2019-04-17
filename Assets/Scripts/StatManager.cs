using UnityEngine;

public class StatManager : MonoBehaviour
{
    private GameManager gameManager;
    private AbilityDisplay scoreDisplay;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        scoreDisplay = FindObjectOfType<AbilityDisplay>();
    }

    public void IncreaseHealth()
    {
        if (gameManager.HaveEnoughAbilityPoints())
        {
            gameManager.playerHealth += 1;
            gameManager.RemoveAbilityPoints(1);
            scoreDisplay.DisplayPoints();
        }
    }

    public void IncreaseDamage()
    {
        if (gameManager.HaveEnoughAbilityPoints())
        {
            gameManager.playerDamage += 1;
            gameManager.RemoveAbilityPoints(1);
            scoreDisplay.DisplayPoints();
        }
    }
}