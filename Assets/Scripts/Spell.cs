﻿using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] int spellDamage;

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.GetComponent<PlayerMovement>())
        {
            otherCollider.GetComponent<Health>().DealDamage(spellDamage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 1.5f);
        }
    }
}