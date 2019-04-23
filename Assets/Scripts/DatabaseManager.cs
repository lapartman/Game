using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class DatabaseManager : MonoBehaviour
{
    private string connectionString;

    void Start()
    {
        connectionString = $"URI=file:{Application.dataPath}/Database/highscore.sqlite";
        GetScores();
    }

    private void GetScores()
    {
        string sqlQuery = "SELECT name, score FROM player INNER JOIN scores ON player.id = scores.playerid";
        using (SqliteConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (SqliteCommand dbCommand = new SqliteCommand(sqlQuery, dbConnection))
            {
                using (SqliteDataReader reader = dbCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Debug.Log($"{reader.GetValue(0)}\n{reader.GetValue(1)}");
                    }
                    reader.Close();
                }
            }
            dbConnection.Close();
        }
    }
}