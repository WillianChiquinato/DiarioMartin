using UnityEngine;
using UnityEngine.UI;

public class BotoesMenu : MonoBehaviour
{
    public Button botaoIniciar;
    public Button botaoOpções;
    public Button botaoSair;

    public LevelLoader levelLoader;

    void Start()
    {
        levelLoader = GameObject.FindFirstObjectByType<LevelLoader>();
    }

    public void Iniciar()
    {
        Debug.Log("Iniciar");
        levelLoader.Transicao("Introducao-CutStory");
    }

    public void Opções()
    {
        Debug.Log("Opções");
        levelLoader.Transicao("Introducao-CutStory");
    }

    public void Sair()
    {
        Debug.Log("Sair");
        Application.Quit();
    }

}
