using UnityEngine;
using UnityEngine.Events;

public class AnimationWeapon : MonoBehaviour
{
    public UnityEvent OnAnimationTrigger, OnAttackPerformed;

    public void TriggerEvent()
    {
        OnAnimationTrigger?.Invoke();
    }

    public void TriggerAttack()
    {
        OnAttackPerformed?.Invoke();
    }
}
