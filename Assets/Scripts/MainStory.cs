using UnityEngine;
using UnityEngine.SceneManagement;

public class MainStory : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Primeira transicao") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            SceneManager.LoadScene("Banheiro");
        }
    }
}
