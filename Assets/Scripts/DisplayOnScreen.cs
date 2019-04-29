using UnityEngine;
using UnityEngine.UI;

public abstract class DisplayOnScreen : MonoBehaviour
{
    protected GameManager gameManager;
    protected Text textComponent;

    /// <summary>
    /// Kiírja a leszármazott osztályokban az aktuális értéket a képernyőre.
    /// </summary>
    public abstract void DisplayValue();
}
