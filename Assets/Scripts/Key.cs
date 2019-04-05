using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    Rigidbody2D keyBody;
    SpriteRenderer spriteRenderer;
    private bool playerHaskey = false;

    void Start()
    {
        keyBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.Rotate(0f, 180f * Time.deltaTime * 0.5f, 0f);
        IsPlayerTouchingKey();
        Debug.Log(playerHaskey);
    }

    private void IsPlayerTouchingKey()
    {
        if (keyBody.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            playerHaskey = true;
            Destroy(spriteRenderer);
        }
    }

    public bool PlayerHasKey()
    {
        return playerHaskey;
    }
}