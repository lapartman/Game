public class HighScore
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Score { get; set; }

    public HighScore(int id, string name, int score)
    {
        Id = id;
        Name = name;
        Score = score;
    }
}