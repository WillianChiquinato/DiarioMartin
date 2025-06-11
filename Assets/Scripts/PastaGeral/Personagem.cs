using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (!gameObject.GetComponent<Health>().isDead)
        {
            weaponParent.Attack();
        }
    }

    private void Update()
    {
        if (!gameObject.GetComponent<Health>().isDead)
        {
            agentMover.movimentInput = MovementInput;
            weaponParent.PointerPosition = pointerInput;
            AnimateCharacter();
        }
        else
        {
            agentMover.movimentInput = Vector2.zero;
        }
    }

    private void AnimateCharacter()
    {
        Vector2 lookDirection = pointerInput - (Vector2)transform.position;
        agentAnimations.RotateToPointer(lookDirection);
        agentAnimations.PlayAnimation(MovementInput);
    }
}
