using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour, IConsoleText
{
    public Action<CurrentAnimation, bool> ChangeAnimationAction;
    public Action<float> AttackCountAction;
    public Action ChangeAnimationGameOverAction;
    public bool isAttacking;

    private Attack _currentAttackMode;
    
    private ConsoleTextController _consoleTextController;
    
    private void Awake()
    {
        isAttacking = false;
    }

    private void Start()
    {
        _consoleTextController = ConsoleTextController.Instance;
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
        float p = 0;
        if (vec != Vector3.zero)
        {
            if(vec.x >= 1)
            {
                p = .0f;
                _currentAttackMode = Attack.JAP;
            }
            else if(vec.y >= 1)
            {
                p = 0.5f;
                _currentAttackMode = Attack.HOOK;
            }
            else if(vec.z >= 1)
            {
                p = 1.0f;
                _currentAttackMode = Attack.UPPERCUT;

            }
            AttackCountAction?.Invoke(p);
        }

        ConsoleTextUpdate();
    }

    public void ConsoleTextUpdate()
    {
        _consoleTextController.CURRENT_ATTACK_MODE = _currentAttackMode;
    }
}