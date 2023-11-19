using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public Action<CurrentAnimation, bool> ChangeAnimationAction;
    public Action ChangeAnimationGameOverAction;

    private void OnAttackStateChange()
    {
        
    }

    private void OnAttack(InputValue value)
    {
        float dir = value.Get<float>();
        if (dir != 0)
        {
            if (dir == -1)
            {
                ChangeAnimationAction?.Invoke(CurrentAnimation.Direction, false);
            }
            else
            {
                ChangeAnimationAction?.Invoke(CurrentAnimation.Direction, true);
            }
        }
        ChangeAnimationAction?.Invoke(CurrentAnimation.Jap, true);
    }
    
    private void OnSwitchAttackMode()
    {
    }
}
