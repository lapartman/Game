using UnityEngine;

public class Chest : MonoBehaviour
{
    private Rigidbody2D chestBody;
    private Animator chestAnimator;
    private AudioSource audioSource;
    [SerializeField] AudioClip openSound;
    private bool chestIsOpen = false;

    private void Start()
    {
        chestBody = GetComponent<Rigidbody2D>();
        chestAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D()
    {
        OpenSesame();
    }

    /// <summary>
    /// Amennyiben teljesülnek a feltételek, a láda kinyílik, és a játékos megkapja a képességpontot.
    /// </summary>
    private void OpenSesame()
    {
        if (FindObjectOfType<PlayerMovement>().playerKeyCount > 0 && chestBody.IsTouchingLayers(LayerMask.GetMask("Player")) && !chestIsOpen)
        {
            chestAnimator.SetTrigger("openChest");
            audioSource.PlayOneShot(openSound);
            chestIsOpen = true;
            FindObjectOfType<GameManager>().AbilityPoints += 1;
            FindObjectOfType<PlayerMovement>().playerKeyCount--;
        }
    }
}