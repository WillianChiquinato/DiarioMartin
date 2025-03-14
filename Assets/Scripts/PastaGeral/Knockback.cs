using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Knockback : MonoBehaviour
{
    public Rigidbody2D rb;

    [SerializeField]
    private float force = 16, delay = 0.15f;

    public UnityEvent OnBegin, OnDone;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Knock(GameObject sender)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;

        rb.AddForce(direction * force, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb.linearVelocity = Vector3.zero;
        OnDone?.Invoke();
    }
}
