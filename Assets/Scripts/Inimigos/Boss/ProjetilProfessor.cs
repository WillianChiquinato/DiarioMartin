using UnityEngine;

public class ProjetilProfessor : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<Knockback>().Knock(collision.gameObject);
                collision.GetComponent<Health>().GetHit(2, gameObject);
                Destroy(gameObject, 0.01f);
            }
        }
    }
}
