using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    Rigidbody2D chestBody;
    Animator chestAnimator;

    void Start()
    {
        chestBody = GetComponent<Rigidbody2D>();
        chestAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        OpenSesame();
        Debug.Log("player has key" + FindObjectOfType<PlayerMovement>().PlayerOwnsKey());
        Debug.Log("is touching player" + chestBody.IsTouchingLayers(LayerMask.GetMask("Player")));
    }

    public void OpenSesame()
    {
        if (FindObjectOfType<PlayerMovement>().PlayerOwnsKey() && chestBody.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            chestAnimator.SetTrigger("openChest");
        }
    }
}