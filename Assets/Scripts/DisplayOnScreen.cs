using UnityEngine;
using UnityEngine.UI;

public abstract class DisplayOnScreen : MonoBehaviour
{
    protected GameManager gameManager;
    protected Text textComponent;

    public abstract void DisplayValue();
}
