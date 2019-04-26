using UnityEngine.UI;

public class ScoreTextDisplay : DisplayOnScreen
{
    void Start()
    {
        textComponent = GetComponent<Text>();
        gameManager = FindObjectOfType<GameManager>();
        DisplayValue();
    }

    public override void DisplayValue()
    {
        textComponent.text = $"Pont: {gameManager.TotalScore.ToString()}";
    }
}