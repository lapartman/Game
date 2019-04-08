using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private Rigidbody2D keyBody;
    private SpriteRenderer spriteRenderer;
    public bool PlayerHasKey { get; private set; } = false;

    private void Start()
    {
        keyBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.Rotate(0f, 180f * 0.5f * Time.deltaTime, 0f);
        PlayerHasTouchedKey();
    }

    private void PlayerHasTouchedKey()
    {
        if (keyBody.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            PlayerHasKey = true;
            Destroy(spriteRenderer);
        }
    }
}