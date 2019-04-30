using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int AbilityPoints { get; set; }
    public int TotalScore { get; set; }
    public string PlayerName { get; set; }

    public int playerLives = 3;
    public int playerHealth = 10;
    public int playerDamage = 1;
    public int CurrentScene { get; set; }
    [SerializeField] GameObject quitPrompt;

    private void Awake()
    {
        Singleton();
    }

    private void Update()
    {
        ShowExitPrompt();
        //Csak akkor változtatja a jelenlegi jelenet indexének számát, ha a pálya neve tartalmazza a Level string-et.
        if (SceneManager.GetActiveScene().name.Contains("Level"))
        {
            CurrentScene = SceneManager.GetActiveScene().buildIndex;
        }
    }

    /// <summary>
    /// Ha a játékos megnyomja a ESC gombot, és az egyik játékpályán, vagy a képességfejlesztési képernyőn van, akkor megjelenik a kilépési ablak.
    /// </summary>
    private void ShowExitPrompt()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && (SceneManager.GetActiveScene().name.Contains("Level") || SceneManager.GetActiveScene().name == "Customization"))
        {
            quitPrompt.SetActive(true);
            Time.timeScale = 0f;
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
}