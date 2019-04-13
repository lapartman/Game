using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int AbilityPoints { get; private set; }

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

    public void AddAbilityPoints(int amount)
    {
        AbilityPoints += amount;
    }
}