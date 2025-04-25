using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    public static PlayerInputManager playerInputManager { get; set; }
    public Camera cameraMouse;

    public GameObject EstilingueObject;
    public static bool PlayerWeaponActive = false;
    public static bool PlayerEstilingueActive = false;
    public GameObject WeaponObject;
    public  Vector3 mousePosition;

    public UnityEvent<Vector2> OnMovimentInput, OnPointInput;
    public UnityEvent OnAttack;
    public bool carregandoEstilingue = false;

    [SerializeField]
    private InputActionReference Moviment, attack, estilingueAttack, PointPosition;

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
            WeaponObject.SetActive(true);
            EstilingueObject.SetActive(false);
            StartCoroutine(WeaponObjectCoroutina());
            Debug.Log("Atirando estilingue!!");
        }
    }

    IEnumerator WeaponObjectCoroutina()
    {
        yield return new WaitForSeconds(0.5f);
        WeaponObject.SetActive(true);
    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = PointPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
