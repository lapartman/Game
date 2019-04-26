﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int AbilityPoints { get; private set; }
    public int TotalScore { get; private set; }
    public string PlayerName { get; set; }

    public int playerLives = 3;
    public int playerHealth = 10;
    public int playerDamage = 1;

    public int CurrentScene { get; set; }

    private void Awake()
    {
        Singleton();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name.Contains("Level"))
        {
            CurrentScene = SceneManager.GetActiveScene().buildIndex;
        }
    }

    private void Start()
    {
        
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