using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    private Text abilityText;
    private GameManager gameManager;

    private void Start()
    {
        abilityText = GetComponent<Text>();
        gameManager = FindObjectOfType<GameManager>();
        DisplayAbilityPoints();
    }

    public void DisplayAbilityPoints()
    {
        abilityText.text = $"Képességpontjaid száma: {gameManager.AbilityPoints.ToString()}";
    }
}