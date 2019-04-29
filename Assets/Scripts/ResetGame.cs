using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    [SerializeField] AudioClip soundEffect;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(ResetGameSession());
    }

    private IEnumerator ResetGameSession()
    {
        audioSource.PlayOneShot(soundEffect);
        yield return new WaitForSeconds(3f);
        Destroy(GameObject.Find("GameManager"));
        SceneManager.LoadScene("Main Menu");
    }
}
