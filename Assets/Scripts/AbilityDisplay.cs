using UnityEngine.UI;

public class AbilityDisplay : Display
{
    private void Start()
    {
        textComponent = GetComponent<Text>();
        gameManager = FindObjectOfType<GameManager>();
        DisplayPoints();
    }

    public override void DisplayPoints()
    {
        textComponent.text = $"Képességpontjaid száma: {gameManager.AbilityPoints}";
    }
}