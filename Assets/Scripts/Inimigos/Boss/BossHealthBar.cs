using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider slider;
    public Health health;

    void Start()
    {
        slider = GetComponent<Slider>();
        health = GameObject.FindFirstObjectByType<Boss>().GetComponent<Health>();
        health.currentHealth = health.maxHealth;
    }

    void Update()
    {
        if (health != null)
        {
            if (slider.value != health.currentHealth)
            {
                slider.value = health.currentHealth;
            }
        }
        else
        {
            Debug.Log("Barra de vida falhada " + gameObject.name);
        }
    }
}
