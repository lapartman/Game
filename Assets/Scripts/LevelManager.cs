using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.GetComponent<PlayerMovement>())
        {
            LoadCustomizationScreen();
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(FindObjectOfType<GameManager>().CurrentScene + 1);
    }

    public void LoadHighScoreScene()
    {
        SceneManager.LoadScene("HighScores");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadLoseScreen()
    {
        SceneManager.LoadScene("LoseScreen");
    }

    private void LoadCustomizationScreen()
    {
        SceneManager.LoadScene("Customization");
    }
}