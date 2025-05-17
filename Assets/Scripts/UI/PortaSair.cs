using TMPro;
using UnityEngine;

public class PortaSair : MonoBehaviour
{
    public TextMeshPro textoInteracao;
    public LevelLoader levelLoader;

    void Start()
    {
        textoInteracao.text = "";
        levelLoader = GameObject.FindFirstObjectByType<LevelLoader>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            textoInteracao.text = "Pressione E para sair";
            if (Input.GetKey(KeyCode.E))
            {
                levelLoader.Transicao("CenaPosBoss");
            }
        }
    }
}
