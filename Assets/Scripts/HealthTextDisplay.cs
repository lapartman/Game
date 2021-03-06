﻿using UnityEngine.UI;

public class HealthTextDisplay : DisplayOnScreen
{
    void Start()
    {
        textComponent = GetComponent<Text>();
        gameManager = FindObjectOfType<GameManager>();
        DisplayValue();
    }

    /// <summary>
    /// Életpontok kiírása a képernyőre
    /// </summary>
    public override void DisplayValue()
    {
        if (textComponent != null)
        {
            textComponent.text = $"Életpontok: {FindObjectOfType<PlayerMovement>().GetComponent<Health>().health}";
        }
    }
}