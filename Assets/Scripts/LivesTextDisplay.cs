using UnityEngine.UI;

public class LivesTextDisplay : DisplayOnScreen
{
    void Start()
    {
        textComponent = GetComponent<Text>();
        gameManager = FindObjectOfType<GameManager>();
        DisplayValue();
    }

    public override void DisplayValue()
    {
        if (textComponent != null)
        {
            textComponent.text = $"Életek: {gameManager.playerLives.ToString()}";
        }
    }
}