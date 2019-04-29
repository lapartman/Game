using UnityEngine;
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
        //Csak akkor változtatja a jelenlegi jelenet indexének számát, ha a pálya neve tartalmazza a Level string-et.
        if (SceneManager.GetActiveScene().name.Contains("Level"))
        {
            CurrentScene = SceneManager.GetActiveScene().buildIndex;
        }
    }

    /// <summary>
    /// Megsemmisíti az újonnan legenerált GameManagereket, biztosítja, hogy csak egy GameManager létezzen.
    /// </summary>
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

    /// <summary>
    /// Megadja, hogy van-e elég képességpontja a játékosnak ahhoz, hogy rányomjon a gombra.
    /// </summary>
    /// <returns>Igazzal tér vissza, ha van legalább 1 pontja.</returns>
    public bool HaveEnoughAbilityPoints()
    {
        return AbilityPoints > 0;
    }

    /// <summary>
    /// Hozzáad a képességpontokhoz paraméterben átadott mennyiséget.
    /// </summary>
    /// <param name="points">Hozzáadott pont mennyisége.</param>
    public void AddAbilityPoints(int points)
    {
        AbilityPoints += points;
    }

    /// <summary>
    /// Levesz egy pontot a képességpontok közül paraméterben megadva.
    /// </summary>
    /// <param name="points">Levont mennyiség</param>
    public void RemoveAbilityPoints(int points)
    {
        AbilityPoints -= points;
    }

    /// <summary>
    /// Hozzáad paraméterben megadott mennyiséget az összes pontszámhoz.
    /// </summary>
    /// <param name="score">Pontszám mennyisége</param>
    public void AddToTotalScore(int score)
    {
        TotalScore += score;
    }
}