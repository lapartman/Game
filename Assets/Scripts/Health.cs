﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health;

    private void Start()
    {

    }

    private void Update()
    {

    }

    public void DealDamage(int damage)
    {
        health -= damage;
    }

    public bool IsDead()
    {
        return health <= 0;
    }
}