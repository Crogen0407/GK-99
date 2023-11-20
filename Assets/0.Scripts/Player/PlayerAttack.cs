using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public Action<CurrentAnimation, bool> ChangeAnimationAction;
    public Action<float> AttackCountAction;
    public Action ChangeAnimationGameOverAction;
    public bool isAttacking;

    private void Awake()
    {
        isAttacking = false;
    }

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
        ChangeAnimationAction?.Invoke(CurrentAnimation.Attack, true);
    }
    
    private void OnSwitchAttackMode(InputValue value)
    {
        Vector3 vec = value.Get<Vector3>();
        Debug.Log(vec);
        float p = 0;
        if (vec != Vector3.zero)
        {
            if(vec.x >= 1)
            {
                p = .0f;
            }
            else if(vec.y >= 1)
            {
                p = 0.5f;
            }
            else if(vec.z >= 1)
            {
                p = 1.0f;
            }
            AttackCountAction?.Invoke(p);
        }
    }
}