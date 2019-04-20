﻿using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int AbilityPoints { get; private set; }
    public int TotalScore { get; private set; }

    public int playerHealth = 10;
    public int playerDamage = 1;

    private void Awake()
    {
        Singleton();
    }

    private void Singleton()
    {
        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        Debug.Log(TotalScore);
    }

    public bool HaveEnoughAbilityPoints()
    {
        return AbilityPoints > 0;
    }

    public void AddAbilityPoints(int points)
    {
        AbilityPoints += points;
    }

    public void RemoveAbilityPoints(int points)
    {
        AbilityPoints -= points;
    }

    public void AddToTotalScore(int score)
    {
        TotalScore += score;
    }
}