using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    Text abilityText;
    GameManager gameManager;

    void Start()
    {
        abilityText = GetComponent<Text>();
        gameManager = FindObjectOfType<GameManager>();
        DisplayAbilityPoints();
    }

    private void DisplayAbilityPoints()
    {
        abilityText.text = $"Képességpontjaid száma: {gameManager.AbilityPoints.ToString()}";
    }
}