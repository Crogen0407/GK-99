using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyAttack : MonoBehaviour
{
    public Action<string, bool> ChangeAnimationAction;
    public Action ChangeAnimationGameOverAction;
    public AnimatorStateInfo currentAnimatorStateInfo;

    [SerializeField] private float fireForce = 4;
    
    private bool _isAttacking;
    public float playerToMyDistance = 100;
    
    //Components
    private Rigidbody _rigidbody;

    //Managements
    private GameManager _gameManager;
    
    //Controllers
    private EffectController _effectController;
    
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

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _effectController = _gameManager.effectController;
    }

    public void OnAttack()
    {
        if (isAttacking == false)
        {
            bool isCurrentAnimatorStateEqualsDefault = currentAnimatorStateInfo.IsName("Idle");
            if (isCurrentAnimatorStateEqualsDefault)
            {
                ChangeAnimationAction?.Invoke("Attack", true);
                ChangeAnimationAction?.Invoke("Idle", false);
            }
        }
    }
}
