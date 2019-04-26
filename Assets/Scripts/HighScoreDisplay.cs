using UnityEngine;
using UnityEngine.UI;

public class HighScoreDisplay : MonoBehaviour
{
    [SerializeField] GameObject rank;
    [SerializeField] GameObject playerName;
    [SerializeField] GameObject score;

    public void SetScore(string rank, string playerName, string score)
    {
        this.rank.GetComponent<Text>().text = rank;
        this.playerName.GetComponent<Text>().text = playerName;
        this.score.GetComponent<Text>().text = score;
    }
}
