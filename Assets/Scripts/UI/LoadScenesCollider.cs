using UnityEngine;

public class LoadScenesCollider : MonoBehaviour
{
    public LevelLoader levelLoader;

    void Start()
    {
        levelLoader = FindFirstObjectByType<LevelLoader>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            levelLoader.Transicao("CenaPosBoss");
        }
    }
}
