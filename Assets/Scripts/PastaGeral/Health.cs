using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int currentHealth, maxHealth;

    public UnityEvent<GameObject> OnHitWithReferences, OnDeathWithReferences;

    public bool isDead = false;

    public void InitializeHealth(int health)
    {
        currentHealth = health;
        maxHealth = health;
        isDead = false;
    }

    public void GetHit(int amount, GameObject sender)
    {
        if (isDead)
        {
            return;
        }
        if (sender.layer == gameObject.layer)
        {
            return;
        }

        currentHealth -= amount;
        this.gameObject.GetComponentInChildren<Animator>().SetTrigger("Hit");

        if (currentHealth > 0)
        {
            OnHitWithReferences.Invoke(sender);
        }
        else
        {
            isDead = true;
            OnDeathWithReferences.Invoke(sender);
            this.gameObject.GetComponentInChildren<Animator>().SetBool("Death", true);
            this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            Destroy(gameObject, 3f);
        }
    }
}
