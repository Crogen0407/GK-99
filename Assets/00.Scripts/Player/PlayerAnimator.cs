using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimator : MonoBehaviour
{
    private PlayerAttack _playerAttack;
    public Animator _animator;
    
    void Awake()
    {
        _playerAttack = GetComponentInParent<PlayerAttack>();
        _animator = GetComponent<Animator>();
        _playerAttack.ChangeAnimationAction += ChangeAnimation;
        _playerAttack.ChangeAnimationGameOverAction += ChangeAnimationGameOver;
        _playerAttack.AttackCountAction += ChangeAttackCount;
    }

    public void GetCurrentAnimatorStateInformation(InputAction.CallbackContext context)
    {
        _playerAttack.currentAnimatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
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

    // private void FixedUpdate()
    // {
    //     _playerAttack.isAttacking = _animator.GetBool("Attack");
    // }
}
