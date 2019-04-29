﻿using UnityEngine;

public class Key : MonoBehaviour
{
    private Rigidbody2D keyBody;
    private PlayerMovement player;
    private AudioSource audioSource;
    [SerializeField] AudioClip keyPickUpSound;
    bool isPicked = false;

    private void Start()
    {
        keyBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
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
            if (!isPicked)
            {
                isPicked = true;
                audioSource.PlayOneShot(keyPickUpSound);
                Destroy(gameObject, 1f);
            }
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