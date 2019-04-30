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
        GameObject.Find("Quit Prompt").SetActive(false);
        Destroy(GameObject.Find("GameManager"));
        SceneManager.LoadScene("Main Menu");
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
