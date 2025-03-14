using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int currentHealth, maxHealth;

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

        if (currentHealth > 0)
        {
            OnHitWithReferences.Invoke(sender);
        }
        else
        {
            isDead = true;
            OnDeathWithReferences.Invoke(sender);
            Destroy(gameObject, 0.5f);
        }
    }
}
