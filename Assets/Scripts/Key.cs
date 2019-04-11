using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private Rigidbody2D keyBody;
    private bool isSpinning = true;
    public bool PlayerHasKey { get; private set; } = false;

    private void Start()
    {
        keyBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        PlayerHasTouchedKey();
        SpinKey();
    }

    private void SpinKey()
    {
        if (isSpinning)
        {
            transform.Rotate(0f, 180f * 0.5f * Time.deltaTime, 0f);
        }
    }

    private void PlayerHasTouchedKey()
    {
        if (keyBody != null && keyBody.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            PlayerHasKey = true;
            isSpinning = false;
            foreach (Component component in gameObject.GetComponents<Component>())
            {
                if (component != GetComponent<Key>() && component != GetComponent<Transform>())
                {
                    Destroy(component);
                }
            }
        }
    }
}