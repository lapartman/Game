using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(ResetGameSession());
    }

    private IEnumerator ResetGameSession()
    {
        yield return new WaitForSeconds(3f);
        Destroy(GameObject.Find("GameManager"));
        SceneManager.LoadScene("Main Menu");
    }
}
