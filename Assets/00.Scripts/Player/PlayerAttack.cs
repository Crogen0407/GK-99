using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour, IConsoleText
{
    public Action<string, bool> ChangeAnimationAction;
    public Action<float> AttackCountAction;
    public Action ChangeAnimationGameOverAction;
    public AnimatorStateInfo currentAnimatorStateInfo;
    private float attackDelayTime = 0;
    
    private bool _isAttacking;
    
    //Components
    private Rigidbody _rigidbody;

    //Managements
    private GameManager _gameManager;
    
    public bool isAttacking
    {
        get
        {
            return _isAttacking;
        }
        set
        {
            _isAttacking = value;
        }
    }

    public Attack currentAttackMode;
    
    private ConsoleTextController _consoleTextController;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _consoleTextController = _gameManager.consoleTextController;
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
                currentAttackMode = Attack.JAP;
            }
            else if(vec.y >= 1)
            {
                p = 0.5f;
                currentAttackMode = Attack.HOOK;
            }
            else if(vec.z >= 1)
            {
                p = 1.0f;
                currentAttackMode = Attack.UPPERCUT;
            }
            AttackCountAction?.Invoke(p);
        }
        ConsoleTextUpdate();
    }

    public void ConsoleTextUpdate()
    {
        _consoleTextController.CURRENT_ATTACK_MODE = currentAttackMode;
    }

    private void Update()
    {
        attackDelayTime += Time.deltaTime;
    }
}