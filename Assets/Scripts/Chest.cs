using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Rigidbody2D chestBody;
    private Animator chestAnimator;

    void Start()
    {
        chestBody = GetComponent<Rigidbody2D>();
        chestAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        OpenSesame();
    }

    public void OpenSesame()
    {
        if (FindObjectOfType<PlayerMovement>().PlayerHasKey && chestBody.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            chestAnimator.SetTrigger("openChest");
        }
    }
}