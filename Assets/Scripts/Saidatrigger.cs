using TMPro;
using UnityEngine;

public class Saidatrigger : MonoBehaviour
{
    public TextMeshPro textoSaida;
    public LevelLoader levelLoader;

    public string SceneName;

    void Awake()
    {
        levelLoader = GameObject.FindFirstObjectByType<LevelLoader>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textoSaida.text = "Pressione E para sair";
            if (Input.GetKeyDown(KeyCode.E))
            {
                levelLoader.Transicao(SceneName);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textoSaida.text = "";
        }
    }
}
