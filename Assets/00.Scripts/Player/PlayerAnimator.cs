using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public enum CurrentAnimation
{
    Attack,
    Direction,
    Ready,
    Guard,
    Default
}

public class PlayerAnimator : MonoBehaviour
{
    private PlayerAttack _playerAttack;
    private Animator _animator;
    
    void Awake()
    {
        _playerAttack = GetComponentInParent<PlayerAttack>();
        _animator = GetComponent<Animator>();
        _playerAttack.ChangeAnimationAction += ChangeAnimation;
        _playerAttack.ChangeAnimationGameOverAction += ChangeAnimationGameOver;
        _playerAttack.AttackCountAction += ChangeAttackCount;
    }

    private void ChangeAnimation(CurrentAnimation currentAnimation, bool parameter)
    {
        int hashCode = Animator.StringToHash(currentAnimation.ToString());
        if (_animator.IsInTransition(0) == false)
        {
            _animator.SetBool(CurrentAnimation.Default.ToString(), false);
            _animator.SetBool(hashCode, parameter);
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
