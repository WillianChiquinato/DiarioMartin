using UnityEngine;
using UnityEngine.InputSystem;

public class Personagem : MonoBehaviour
{
    public PlayerAnimation agentAnimations;
    public PersonagemMover agentMover;

    private WeaponParent weaponParent;

    private Vector2 pointerInput, movementInput;

    public Vector2 PointerInput { get => pointerInput; set => pointerInput = value; }
    public Vector2 MovementInput { get => movementInput; set => movementInput = value; }

    private void Awake()
    {
        agentAnimations = GetComponentInChildren<PlayerAnimation>();
        weaponParent = GetComponentInChildren<WeaponParent>();
        agentMover = GetComponent<PersonagemMover>();
    }

    public void PerformAttack()
    {
        weaponParent.Attack();
    }

    private void Update()
    {
        // pointerInput = GetPointerInput();
        // movementInput = Moviment.action.ReadValue<Vector2>().normalized;

        agentMover.movimentInput = MovementInput;
        weaponParent.PointerPosition = pointerInput;
        AnimateCharacter();
    }

    private void AnimateCharacter()
    {
        Vector2 lookDirection = pointerInput - (Vector2)transform.position;
        agentAnimations.RotateToPointer(lookDirection);
        agentAnimations.PlayAnimation(MovementInput);
    }
}
