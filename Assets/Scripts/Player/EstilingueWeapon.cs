using UnityEngine;

public class EstilingueWeapon : MonoBehaviour
{
    public bool IsAttacking { get; private set; }
    public SpriteRenderer characterRender, WeaponRender;

    void Update()
    {
       if (IsAttacking)
        {
            return;
        }

        Vector2 direcao = ((Vector2)GetComponentInParent<PlayerInputSystem>().mousePosition - (Vector2)transform.position).normalized;
        transform.right = direcao;

        Vector2 scale = transform.localScale;
        if (direcao.x < 0)
        {
            scale.y = -1;
        }
        else if (direcao.x > 0)
        {
            scale.y = 1;
        }

        transform.localScale = scale;

        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        {
            WeaponRender.sortingOrder = characterRender.sortingOrder - 1;
        }
        else
        {
            WeaponRender.sortingOrder = characterRender.sortingOrder + 1;
        } 
    }
}
