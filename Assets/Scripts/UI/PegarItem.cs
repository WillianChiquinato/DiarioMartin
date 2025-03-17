using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PegarItem : MonoBehaviour
{
    public TextMeshPro textoWeapon;
    public GameObject objetoMissao;

    void Awake()
    {
        objetoMissao.SetActive(false);

        textoWeapon.text = "";
        if (PlayerInputSystem.PlayerWeaponActive)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textoWeapon.text = "Pressione E para pegar a arma";
            if (Input.GetKey(KeyCode.E))
            {
                if (this.gameObject.CompareTag("Missao"))
                {
                    objetoMissao.SetActive(true);
                    Destroy(this.gameObject);
                    PlayerInputSystem.PlayerWeaponActive = true;
                }
                else
                {
                    PlayerInputSystem.PlayerWeaponActive = true;
                    Destroy(this.gameObject);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textoWeapon.text = "";
        }
    }
}
