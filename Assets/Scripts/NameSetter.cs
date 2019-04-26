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

    public void SetPlayerName()
    {
        if (inputField.text.Length>0)
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
