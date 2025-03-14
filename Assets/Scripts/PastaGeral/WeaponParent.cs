using System.Collections;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    [Header("Instances")]
    public Animator animatorWeapon;
    public Transform circleWeapon;
    public SpriteRenderer characterRender, WeaponRender;
    public Vector2 PointerPosition { get; set; }


    [Header("Weapon & Attack")]
    private float delay = 0.7f;
    public bool AttackBlock;
    public bool IsAttacking { get; private set; }
    public float radius;
    public LayerMask layerMaskDetect;


    void Awake()
    {
        animatorWeapon = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (IsAttacking)
        {
            return;
        }

        Vector2 direcao = (PointerPosition - (Vector2)transform.position).normalized;
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

    public void Attack()
    {
        if (AttackBlock)
        {
            return;
        }

        AttackBlock = true;
        animatorWeapon.SetTrigger("Attack");
        IsAttacking = true;
        StartCoroutine(DelayAttack());
    }

    public void ResetIsAttacking()
    {
        IsAttacking = false;
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);

        AttackBlock = false;
    }

    public void DetectColliders()
    {
        foreach (Collider2D colisorEnemy in Physics2D.OverlapCircleAll(circleWeapon.position, radius, layerMaskDetect))
        {
            Health health;
            if (health = colisorEnemy.GetComponent<Health>())
            {
                health.GetHit(1, transform.parent.gameObject);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        //Criação do attack sphere.
        Gizmos.color = Color.blue;
        Vector3 position = circleWeapon == null ? Vector3.zero : circleWeapon.position;

        Gizmos.DrawWireSphere(position, radius);
    }
}
