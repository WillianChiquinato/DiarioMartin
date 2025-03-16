using TMPro;
using UnityEngine;

public class PegarItem : MonoBehaviour
{
    public TextMeshPro textoWeapon;

    void Awake()
    {
        textoWeapon.text = "";
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textoWeapon.text = "Pressione E para pegar a arma";
            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(gameObject);
                PlayerInputSystem.PlayerWeaponActive = true;
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
