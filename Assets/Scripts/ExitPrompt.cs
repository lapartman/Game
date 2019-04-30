using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPrompt : MonoBehaviour
{
    /// <summary>
    /// Kilépési ablak "Igen" gombja.
    /// </summary>
    public void YesButton()
    {
        FindObjectOfType<GameManager>().TotalScore = 0;
        GameObject.Find("Quit Prompt").SetActive(false);
        SceneManager.LoadScene("Main Menu");
        Destroy(FindObjectOfType<GameManager>());
    }

    /// <summary>
    /// Kilépési ablak "Nem gombja".
    /// </summary>
    public void NoButton()
    {
        GameObject.Find("Quit Prompt").SetActive(false);
        Time.timeScale = 1f;
    }
}
