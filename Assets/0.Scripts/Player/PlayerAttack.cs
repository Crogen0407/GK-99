using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Action<CurrentAnimation, bool> ChangeAnimationAction;
    public Action ChangeAnimationGameOverAction;

    private void OnAttackStateChange()
    {
        
    }

    private void OnAttack()
    {
        ChangeAnimationAction?.Invoke(CurrentAnimation.Direction, true);
        ChangeAnimationAction?.Invoke(CurrentAnimation.Jap, true);
    }
}
