using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    public UnityEvent<Vector2> OnMovimentInput, OnPointInput;
    public UnityEvent OnAttack;

    [SerializeField]
    private InputActionReference Moviment, attack, PointPosition;

    void Update()
    {
        OnMovimentInput?.Invoke(Moviment.action.ReadValue<Vector2>().normalized);
        OnPointInput?.Invoke(GetPointerInput());
    }

    private void OnEnable()
    {
        attack.action.performed += PerformAttack;
    }

    private void OnDisable()
    {
        attack.action.performed -= PerformAttack;
    }

    private void PerformAttack(InputAction.CallbackContext context)
    {
        OnAttack?.Invoke();
    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = PointPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
