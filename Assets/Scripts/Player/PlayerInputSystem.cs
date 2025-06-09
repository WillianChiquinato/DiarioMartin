using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    public static PlayerInputManager playerInputManager { get; set; }
    public Camera cameraMouse;
    public GameObject bulletPrefab;

    public GameObject EstilingueObject;
    
    //Mudar na build normal.
    public static bool PlayerWeaponActive = true;
    public static bool PlayerEstilingueActive = true;
    public GameObject WeaponObject;
    public Vector3 mousePosition;

    public UnityEvent<Vector2> OnMovimentInput, OnPointInput;
    public UnityEvent OnAttack;
    public bool carregandoEstilingue = false;
    public float tempoCarregando = 0f;
    private float tempoMinimoAttackEstilingue = 0.4f;

    public InputActionReference Moviment, attack, estilingueAttack, PointPosition;

    private void Awake()
    {
        cameraMouse = FindFirstObjectByType<Camera>();
        if (!PlayerWeaponActive)
        {
            WeaponObject.SetActive(false);
        }

        EstilingueObject = transform.GetChild(3).gameObject;
        EstilingueObject.SetActive(false);
    }

    void Update()
    {
        if (!PlayerWeaponActive)
        {
            WeaponObject.SetActive(false);
        }
        else
        {
            WeaponObject.SetActive(true);
        }

        if (carregandoEstilingue)
        {
            mousePosition = cameraMouse.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePosition - EstilingueObject.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            EstilingueObject.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        OnMovimentInput?.Invoke(Moviment.action.ReadValue<Vector2>().normalized);
        OnPointInput?.Invoke(GetPointerInput());
    }

    private void OnEnable()
    {
        attack.action.performed += PerformAttack;
        estilingueAttack.action.started += EstilingueAttackStarted;
        estilingueAttack.action.canceled += EstilingueAttackCanceled;
    }

    private void OnDisable()
    {
        attack.action.performed -= PerformAttack;
        estilingueAttack.action.started -= EstilingueAttackStarted;
        estilingueAttack.action.canceled -= EstilingueAttackCanceled;
    }

    private void PerformAttack(InputAction.CallbackContext context)
    {
        if (PlayerWeaponActive)
        {
            OnAttack?.Invoke();
        }
    }

    public void EstilingueAttackStarted(InputAction.CallbackContext context)
    {
        if (PlayerEstilingueActive)
        {
            carregandoEstilingue = true;
            tempoCarregando = Time.time;
            WeaponObject.SetActive(false);
            EstilingueObject.SetActive(true);
            Debug.Log("Carregando estilingue!!");
        }
    }

    public void EstilingueAttackCanceled(InputAction.CallbackContext context)
    {
        if (PlayerEstilingueActive && carregandoEstilingue)
        {
            carregandoEstilingue = false;
            float tempoSegurando = Time.time - tempoCarregando;

            if (tempoSegurando >= tempoMinimoAttackEstilingue)
            {
                StartCoroutine(WeaponObjectCoroutina());
                Debug.Log("Atirando estilingue!!");
            }
            else
            {
                Debug.Log("Soltou muito cedo, não atirou!");
            }
        }
    }

    IEnumerator WeaponObjectCoroutina()
    {
        WeaponObject.SetActive(true);

        //Instancia o estilingue na posição do ponto de spawn.
        GameObject estilingue = Instantiate(bulletPrefab, EstilingueObject.transform.position, Quaternion.identity);
        //Define a direção do estilingue para o player.
        Vector2 direction = (mousePosition - EstilingueObject.transform.position).normalized;
        estilingue.GetComponent<Rigidbody2D>().linearVelocity = direction * 35f;

        //O objeto rotaciona para a direção do tiro.
        float angulo = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        estilingue.transform.rotation = Quaternion.AngleAxis(angulo, Vector3.forward);

        yield return new WaitForSeconds(0.2f);
        EstilingueObject.SetActive(false);
        WeaponObject.SetActive(true);
    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = PointPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
