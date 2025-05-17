using System.Collections;
using UnityEngine;

public class TriggerCutSceneBoss : MonoBehaviour
{
    public bool isCutScene;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isCutScene = true;
        }
    }
}
