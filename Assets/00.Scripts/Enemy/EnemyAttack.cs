using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyAttack : MonoBehaviour
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

    private ConsoleTextController _consoleTextController;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    public void OnAttack()
    {
        if (isAttacking == false)
        {
            bool isCurrentAnimatorStateEqualsDefault = currentAnimatorStateInfo.IsName("Idle");
            if (isCurrentAnimatorStateEqualsDefault)
            {
                if (attackDelayTime > currentAnimatorStateInfo.length+0.1f)
                {
                    attackDelayTime = 0;
                    ChangeAnimationAction?.Invoke("Attack", true);
                    ChangeAnimationAction?.Invoke("Idle", false);
                }
            }
        }
    }
    
    private void Update()
    {
        attackDelayTime += Time.deltaTime;
    }
}
