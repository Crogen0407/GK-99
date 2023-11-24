using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour, IConsoleText
{
    public Action<string, bool> ChangeAnimationAction;
    public Action<float> AttackCountAction;
    public Action ChangeAnimationGameOverAction;

    [SerializeField] private Transform _attackEffectPointRight;
    [SerializeField] private Transform _attackEffectPointLeft;

    
    private float attackDelayTime = 0;
    
    private bool _isAttacking;
    public bool isAttacking
    {
        get
        {
            return _isAttacking;
        }
        set
        {
            Debug.Log($"isattack을 {value} 설정");
            _isAttacking = value;
        }
    }

    private Attack _currentAttackMode;
    
    private ConsoleTextController _consoleTextController;
    
    private void Start()
    {
        _consoleTextController = ConsoleTextController.Instance;
    }

    private void OnAttack(InputValue value)
    {
        Debug.Log("키눌림");
        if (attackDelayTime > (PlayerAnimator.Instance._animator.GetCurrentAnimatorStateInfo(0).length + 0.1f))
        {
            if (PlayerAnimator.Instance._animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 &&
                PlayerAnimator.Instance._animator.GetBool("Attack") == false)
            
            {
                ChangeAnimationAction?.Invoke("Default", false);
                attackDelayTime = 0;
                float dir = value.Get<float>();
                if (dir != 0)
                {
                    if (dir == -1)
                    {
                        ChangeAnimationAction?.Invoke("Direction", false);
                    }
                    else
                    {
                        ChangeAnimationAction?.Invoke("Direction", true);
                    }
                }
                ChangeAnimationAction?.Invoke("Attack", true);
                isAttacking = true;
            }
        }
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

    private void Update()
    {
        attackDelayTime += Time.deltaTime;
    }
}