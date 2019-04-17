﻿using UnityEngine.UI;

public class ScoreDisplay : Display
{
    void Start()
    {
        textComponent = GetComponent<Text>();
        gameManager = FindObjectOfType<GameManager>();
        DisplayPoints();
    }
    
    void Update()
    {
        DisplayPoints();
    }

    public override void DisplayPoints()
    {
        textComponent.text=$"Pont: {gameManager.TotalScore}";
    }
}
