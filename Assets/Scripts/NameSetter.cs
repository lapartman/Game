using UnityEngine;
using UnityEngine.UI;

public class NameSetter : MonoBehaviour
{
    [SerializeField] InputField inputField;
    [SerializeField] Button startButton;

    private void Start()
    {
        startButton.enabled = false;
    }

    /// <summary>
    /// Játékos nevének a beállítása, ha az input mező hosszabb mint 0, akkor el lehet kezdeni a játékot.
    /// </summary>
    public void SetPlayerName()
    {
        if (inputField.text.Length > 0)
        {
            FindObjectOfType<GameManager>().PlayerName = inputField.text;
            startButton.enabled = true;
        }
        else
        {
            startButton.enabled = false;
        }
    }
}