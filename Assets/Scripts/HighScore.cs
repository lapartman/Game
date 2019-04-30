public class HighScore
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Score { get; set; }

    /// <summary>
    /// DatabaseManager-hez szükséges osztály konstruktora, ilyen típusú listát hozunk majd létre a DatabaseManageren belül, a rekordok kiírásához szükséges
    /// </summary>
    /// <param name="id">Játékos ID-je</param>
    /// <param name="name">Játékos neve</param>
    /// <param name="score">Játékos pontja</param>
    public HighScore(int id, string name, int score)
    {
        Id = id;
        Name = name;
        Score = score;
    }
}