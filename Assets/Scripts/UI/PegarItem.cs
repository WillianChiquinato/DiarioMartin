using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PegarItem : MonoBehaviour
{
    public TextMeshPro textoWeapon;
    public GameObject objetoMissao;
    public GameObject KnifeIcon;
    public GameObject estilingueIcon;

    void Awake()
    {
        if (estilingueIcon == null)
        {
            Debug.Log("Item missão");
        }
        else
        {
            estilingueIcon.SetActive(false);
        }

        if (KnifeIcon == null)
        {
            Debug.Log("Item missão");
        }
        else
        {
            KnifeIcon.SetActive(false);
        }

        if (objetoMissao == null)
        {
            Debug.Log("Item weapon");
        }
        else
        {
            objetoMissao.SetActive(false);
        }

        textoWeapon.text = "";
    }

    void Start()
    {
        if (PlayerInputSystem.PlayerWeaponActive && !this.gameObject.CompareTag("Missao"))
        {
            Destroy(this.gameObject);
            if (KnifeIcon == null)
            {
                Debug.Log("Item missão");
            }
            else
            {
                KnifeIcon.SetActive(true);
            }
        }

        if (PlayerInputSystem.PlayerWeaponActive && !this.gameObject.CompareTag("Missao"))
        {
            Destroy(this.gameObject);
            if (KnifeIcon == null)
            {
                Debug.Log("Item missão");
            }
            else
            {
                KnifeIcon.SetActive(true);
            }
        }

        if (PlayerInputSystem.PlayerEstilingueActive && !this.gameObject.CompareTag("Missao"))
        {
            Destroy(this.gameObject);
            if (estilingueIcon == null)
            {
                Debug.Log("Item missão");
            }
            else
            {
                estilingueIcon.SetActive(true);
            }
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
                    this.objetoMissao.SetActive(true);
                    PlayerInputSystem.PlayerWeaponActive = true;
                }
                else if (this.gameObject.name == "WeaponPegar")
                {
                    PlayerInputSystem.PlayerWeaponActive = true;
                    KnifeIcon.SetActive(true);
                    Destroy(this.gameObject);
                }
                else
                {
                    PlayerInputSystem.PlayerEstilingueActive = true;
                    estilingueIcon.SetActive(true);
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

    public void sairButton()
    {
        this.objetoMissao.SetActive(false);
        Destroy(this.gameObject, 1f);
    }
}
