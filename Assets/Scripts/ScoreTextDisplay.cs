using UnityEngine.UI;

public class ScoreTextDisplay : DisplayOnScreen
{
    void Start()
    {
        textComponent = GetComponent<Text>();
        gameManager = FindObjectOfType<GameManager>();
        DisplayValue();
    }

    /// <summary>
    /// Kiírja a szerzett pontok számát a képernyőre
    /// </summary>
    public override void DisplayValue()
    {
        textComponent.text = $"Pont: {gameManager.TotalScore.ToString()}";
    }
}