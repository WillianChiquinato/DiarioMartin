using UnityEngine;

public class ProjetilFantasma : MonoBehaviour
{
    public Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        if (rb.linearVelocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Knockback>().Knock(collision.gameObject);
            collision.GetComponent<Health>().GetHit(1, gameObject);
            Destroy(gameObject, 0.01f);
        }
    }
}
