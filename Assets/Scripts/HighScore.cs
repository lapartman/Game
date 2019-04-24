using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Linq;

public class HighScore
{
    public string Name { get; set; }
    public int Id { get; set; }
    public int Score { get; set; }

    public HighScore(int id, string name, int score)
    {
        Id = id;
        Name = name;
        Score = score;
    }
}