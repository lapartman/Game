using UnityEngine;

public class Key : MonoBehaviour
{
    private Rigidbody2D keyBody;
    private PlayerMovement player;

    private void Start()
    {
        keyBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        PlayerHasTouchedKey();
        SpinKey();
    }

    private void SpinKey()
    {
        transform.Rotate(0f, 180f * 0.5f * Time.deltaTime, 0f);
    }

    private void PlayerHasTouchedKey()
    {
        if (keyBody.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (player != null)
        {
            player.playerKeyCount++;
        }
    }
}