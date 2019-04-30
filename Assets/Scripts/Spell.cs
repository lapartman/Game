using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] int spellDamage;

    /// <summary>
    /// Levezényli a tűzcsóva játékosba ütközését, leveszi a játékos életéből a sebzés értékét, és frissíti az életpontok számát a képernyőn.
    /// </summary>
    /// <param name="otherCollider">Az másik objektum ütközője.</param>
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.GetComponent<PlayerMovement>())
        {
            otherCollider.GetComponent<Health>().DealDamage(spellDamage);
            FindObjectOfType<HealthTextDisplay>().DisplayValue();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 2f);
        }
    }
}