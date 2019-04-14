using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    private GameManager gameManager;
    private ScoreDisplay scoreDisplay;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        scoreDisplay = FindObjectOfType<ScoreDisplay>();
    }

    public void IncreaseHealth()
    {
        if (gameManager.HaveEnoughAbilityPoints())
        {
            gameManager.playerHealth += 1;
            gameManager.RemoveAbilityPoints(1);
            scoreDisplay.DisplayAbilityPoints();
        }
    }

    public void IncreaseDamage()
    {
        if (gameManager.HaveEnoughAbilityPoints())
        {
            gameManager.playerDamage += 1;
            gameManager.RemoveAbilityPoints(1);
            scoreDisplay.DisplayAbilityPoints();
        }
    }
}