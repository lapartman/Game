using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int AbilityPoints { get; private set; }

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
}