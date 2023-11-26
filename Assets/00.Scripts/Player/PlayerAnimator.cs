using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Transform[] _attackEffectPointRight;
    [SerializeField] private Transform[] _attackEffectPointLeft;
    
    //Components
    public Animator _animator;
    private PlayerAttack _playerAttack;
    
    //Managements
    private GameManager _gameManager;

    //Controllers
    private EffectController _effectController;

    void Awake()
    {
        _playerAttack = GetComponentInParent<PlayerAttack>();
        _animator = GetComponent<Animator>();
        _playerAttack.ChangeAnimationAction += ChangeAnimation;
        _playerAttack.ChangeAnimationGameOverAction += ChangeAnimationGameOver;
        _playerAttack.AttackCountAction += ChangeAttackCount;
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _effectController = _gameManager.effectController;
    }

    public void GetCurrentAnimatorStateInformation(InputAction.CallbackContext context)
    {
        _playerAttack.currentAnimatorStateInfo  = _animator.GetCurrentAnimatorStateInfo(0);
    }
    
    private void ChangeAnimation(string currentAnimation, bool parameter)
    {
        int hashCode = Animator.StringToHash(currentAnimation);
        _animator.SetBool(hashCode, parameter);
        if (currentAnimation.Equals("Attack"))
        {
            _playerAttack.isAttacking = parameter;
        }
    }
    
    public void ChangeStateToTrueAnimation(string currentAnimation)
    {
        _animator.SetBool(currentAnimation, true);
        if (currentAnimation.Equals("Attack"))
        {
            _playerAttack.isAttacking = true;
        }
    }
    
    public void ChangeStateToFalseAnimation(string currentAnimation)
    {
        _animator.SetBool(currentAnimation, false);
        if (currentAnimation.Equals("Attack"))
        {
            _playerAttack.isAttacking = false;
        }
    }

    public void ChangeAttackCount(float parameter)
    {
        _animator.SetFloat("AttackCount", parameter);
    }
    
    private void ChangeAnimationGameOver()
    {
        _animator.SetTrigger("GameOver");
    }

    public void CreateAttackEffect(float direction)
    {
        int attackIndex = 0;
        switch (_playerAttack.currentAttackMode)
        {
            case Attack.JAP :
                attackIndex = 0;
                break;
            case Attack.HOOK :
                attackIndex = 1;
                break;
            case Attack.UPPERCUT :
                attackIndex = 2;
                break;
        }
        if (direction > 0)
        {
             GameObject obj = _effectController.CreateEffect("PlayerAttackEffect", _attackEffectPointRight[attackIndex].position,
                _attackEffectPointRight[attackIndex].rotation, 1);
             obj.GetComponent<VisualEffect>().Play();
        }
        else if(direction < 0)
        {
            GameObject obj = _effectController.CreateEffect("PlayerAttackEffect", _attackEffectPointLeft[attackIndex].position,
                _attackEffectPointLeft[attackIndex].rotation, 1);
            obj.GetComponent<VisualEffect>().Play();
        }
    }
}
