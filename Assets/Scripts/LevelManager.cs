using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// Ha a játékos beleütközik az ajtóba, betöltődik a következő jelenet.
    /// </summary>
    /// <param name="otherCollider">Másik objektum ütközője</param>
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.GetComponent<PlayerMovement>())
        {
            if (FindObjectOfType<GameManager>().CurrentScene == 3)
            {
                SceneManager.LoadScene("WinScreen");
            }
            else
            {
                LoadCustomizationScreen();
            }
        }
    }

    /// <summary>
    /// Következő jelenet betöltése.
    /// </summary>
    public void LoadNextScene()
    {
        SceneManager.LoadScene(FindObjectOfType<GameManager>().CurrentScene + 1);
    }

    /// <summary>
    /// Betölti a toplista jelenetet.
    /// </summary>
    public void LoadHighScoreScene()
    {
        SceneManager.LoadScene("HighScores");
    }

    /// <summary>
    /// Kilép a játékból.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Betölti a főmenüt.
    /// </summary>
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    /// <summary>
    /// Betölti a jelenlegi szintet.
    /// </summary>
    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Betölti a vesztes jelenetet.
    /// </summary>
    public void LoadLoseScreen()
    {
        SceneManager.LoadScene("LoseScreen");
    }

    /// <summary>
    /// Betölti a képességfejlesztő jelenetet.
    /// </summary>
    private void LoadCustomizationScreen()
    {
        SceneManager.LoadScene("Customization");
    }
}