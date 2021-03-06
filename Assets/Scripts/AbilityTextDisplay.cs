﻿using UnityEngine.UI;
using UnityEngine;

public class AbilityTextDisplay : DisplayOnScreen
{
    private void Start()
    {
        textComponent = GetComponent<Text>();
        gameManager = FindObjectOfType<GameManager>();
    }

    /// <summary>
    /// Képességfejlesztő képernyőn kiírja a képességpontok számát.
    /// </summary>
    public override void DisplayValue()
    {
        textComponent.text = $"Képességpontjaid száma: {gameManager.AbilityPoints}";
    }
}