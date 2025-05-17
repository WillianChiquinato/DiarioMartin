using UnityEngine;
using UnityEngine.UI;

public class BotoesMenu : MonoBehaviour
{
    public Button botaoIniciar;
    public Button botaoOpções;
    public Button botaoSair;

    public LevelLoader levelLoader;
    public GameObject comoJogarMenu;

    void Start()
    {
        levelLoader = GameObject.FindFirstObjectByType<LevelLoader>();
        comoJogarMenu.SetActive(false);
    }

    public void Iniciar()
    {
        Debug.Log("Iniciar");
        levelLoader.Transicao("Introducao-CutStory");
    }

    public void ComoJogar()
    {
        Debug.Log("Como Jogar");
        comoJogarMenu.SetActive(true);
    }
    public void FecharComoJogar()
    {
        Debug.Log("Fechar Como Jogar");
        comoJogarMenu.SetActive(false);
    }

    public void Sair()
    {
        Debug.Log("Sair");
        Application.Quit();
    }
}
