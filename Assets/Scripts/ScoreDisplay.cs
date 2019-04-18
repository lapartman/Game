using UnityEngine.UI;

public class ScoreDisplay : Display
{
    private void Start()
    {
        textComponent = GetComponent<Text>();
        gameManager = FindObjectOfType<GameManager>();
        DisplayPoints();
    }

    public override void DisplayPoints()
    {
        textComponent.text=$"Pont: {gameManager.TotalScore}";
    }
}
