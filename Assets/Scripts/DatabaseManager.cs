using System.Collections.Generic;
using UnityEngine;
using System;
using Mono.Data.Sqlite;

public class DatabaseManager : MonoBehaviour
{
    private string connectionString;
    private List<HighScore> highScores = new List<HighScore>();

    [SerializeField] GameObject scorePrefab;
    [SerializeField] Transform scoreParent;

    private void Start()
    {
        connectionString = $"URI=file:{Application.dataPath}/highscore.sqlite";
        CreateTables();

        if (scorePrefab != null && scoreParent != null)
        {
            ShowScores();
        }

        if (FindObjectOfType<GameManager>() && FindObjectOfType<GameManager>().TotalScore != 0)
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            InsertScore(gameManager.PlayerName, gameManager.TotalScore);
        }
    }

    private void CreateTables()
    {
        string createTablesSql = "CREATE TABLE IF NOT EXISTS player (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, name VARCHAR NOT NULL);CREATE TABLE IF NOT EXISTS scores (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, playerid INTEGER NOT NULL REFERENCES player (id) ON DELETE CASCADE ON UPDATE CASCADE, score INTEGER NOT NULL);";
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(createTablesSql, connection))
            {
                command.ExecuteScalar();
            }
            connection.Close();
        }
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
        string sqlInsertScore = "INSERT INTO scores (playerid, score) VALUES (:playerid, :score)";
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
            scoreInstance.GetComponent<HighScoreDisplay>().SetScore($"#{i + 1}", highScores[i].Name, highScores[i].Score.ToString());
            scoreInstance.transform.SetParent(scoreParent);
            scoreInstance.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }
}