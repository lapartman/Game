using UnityEngine;

public class Chest : MonoBehaviour
{
    private Rigidbody2D chestBody;
    private Animator chestAnimator;
    private bool chestIsOpen = false;

    private void Start()
    {
        chestBody = GetComponent<Rigidbody2D>();
        chestAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D()
    {
        OpenSesame();
    }

    private void OpenSesame()
    {
        if (FindObjectOfType<PlayerMovement>().playerKeyCount > 0 && chestBody.IsTouchingLayers(LayerMask.GetMask("Player")) && !chestIsOpen)
        {
            chestAnimator.SetTrigger("openChest");
            chestIsOpen = true;
            FindObjectOfType<GameManager>().AddAbilityPoints(1);
            FindObjectOfType<PlayerMovement>().playerKeyCount--;
        }
    }
}