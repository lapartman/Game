using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] int spellDamage;
    [SerializeField] float spellSpeed;

    private void Update()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D otherCollider)
    {
        GameObject otherObject = otherCollider.gameObject;
        if (otherObject.GetComponent<PlayerMovement>())
        {
            otherObject.GetComponent<Health>().DealDamage(spellDamage);
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        transform.Translate(transform.position * spellSpeed * Time.deltaTime);
    }
}