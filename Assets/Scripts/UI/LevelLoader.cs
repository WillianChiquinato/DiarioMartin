using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator animator;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Transicao(string sceneName)
    {
        StartCoroutine(loadScene(sceneName));
        Debug.Log("iniciando");
    }

    IEnumerator loadScene(string sceneName)
    {
        animator.SetTrigger("start");

        yield return new WaitForSeconds(1.1f);

        SceneManager.LoadScene(sceneName);
    }
}
