using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour, IConsoleText
{
    public Action<string, bool> ChangeAnimationAction;
    public Action<float> AttackCountAction;
    public Action ChangeAnimationGameOverAction;
    public AnimatorStateInfo currentAnimatorStateInfo;
    
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

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (isAttacking == false)
        {
            bool isCurrentAnimatorStateEqualsDefault = currentAnimatorStateInfo.IsName("Default");
            if (isCurrentAnimatorStateEqualsDefault)
            {
                if (attackDelayTime > currentAnimatorStateInfo.length+0.1f)
                {
                    attackDelayTime = 0;
                    Debug.Log("키눌림");
                    ChangeAnimationAction?.Invoke("Attack", true);
                    ChangeAnimationAction?.Invoke("Default", false);
                    float dir = context.ReadValue<float>();
                    if (dir != 0)
                    {
                        if (dir < 0)
                        {
                            ChangeAnimationAction?.Invoke("Direction", false);
                        }
                        else if(dir > 0)
                        {
                            ChangeAnimationAction?.Invoke("Direction", true);
                        }
                    }
                }
            }
        }
    }
    
    public void OnSwitchAttackMode(InputAction.CallbackContext context)
    {
        Vector3 vec = context.ReadValue<Vector3>();
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