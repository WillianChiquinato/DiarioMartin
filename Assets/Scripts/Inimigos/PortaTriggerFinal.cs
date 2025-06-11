using UnityEngine;

public class PortaTriggerFinal : MonoBehaviour
{
    Health healthTrigger;
    public GameObject portaTrigger;

    void Start()
    {
        healthTrigger = GetComponent<Health>();
    }

    void Update()
    {
        if (healthTrigger.isDead)
        {
            Destroy(portaTrigger, 0.5f);
        }
    }
}
