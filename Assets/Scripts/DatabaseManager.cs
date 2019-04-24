using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class DatabaseManager : MonoBehaviour
{
    private string connectionString;
    private List<HighScore> highScores = new List<HighScore>();

    [SerializeField] GameObject scorePrefab;
    [SerializeField] Transform scoreParent;

    private void Start()
    {
        connectionString = $"URI=file:{Application.dataPath}/Database/highscore.sqlite";
        ShowScores();
    }

    private void GetScores()
    {
        highScores.Clear();
        string sqlQuery = "SELECT player.id, name, score FROM player INNER JOIN scores ON player.id = scores.playerid ORDER BY score DESC LIMIT 15";
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(sqlQuery, connection))
            {
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        highScores.Add(new HighScore(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
                    }
                    reader.Close();
                }
            }
            connection.Close();
        }
    }

    private void InsertScore(string name, int score)
    {
        string sqlInsertPlayer = "INSERT INTO player (name) VALUES (:name)";
        string sqlInsertScore = "INSERT INTO scores(playerid, score) VALUES(:playerid, :score)";
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(sqlInsertPlayer, connection))
            {
                command.Parameters.AddWithValue(":name", name);
                command.ExecuteNonQuery();
            }

            string sqlForLastId = "SELECT last_insert_rowid()";
            SqliteCommand cmd = connection.CreateCommand();
            cmd.CommandText = sqlForLastId;
            int playerId = Convert.ToInt32(cmd.ExecuteScalar());

            using (SqliteCommand command = new SqliteCommand(sqlInsertScore, connection))
            {
                command.Parameters.AddWithValue(":playerid", playerId);
                command.Parameters.AddWithValue(":score", score);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    private void ShowScores()
    {
        GetScores();
        for (int i = 0; i < highScores.Count; i++)
        {
            GameObject scoreInstance = Instantiate(scorePrefab);
            HighScore score = highScores[i];
            scoreInstance.GetComponent<HighScoreDisplay>().SetScore($"#{(i + 1).ToString()}", score.Name, score.Score.ToString());
            scoreInstance.transform.SetParent(scoreParent);
            scoreInstance.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
    }
}