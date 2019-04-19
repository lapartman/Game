using UnityEngine;
using UnityEngine.UI;

public abstract class Display : MonoBehaviour
{
    protected Text textComponent;
    protected GameManager gameManager;

    public abstract void DisplayPoints();
}