using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] int spellDamage;
    [SerializeField] float spellSpeed;

    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.GetComponent<PlayerMovement>())
        {
            otherCollider.GetComponent<Health>().DealDamage(spellDamage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 1.5f);
        }
    }

    private void Move()
    {
        transform.Translate(transform.position * spellSpeed * Time.deltaTime);
    }
}