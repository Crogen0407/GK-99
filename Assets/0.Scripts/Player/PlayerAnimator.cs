using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public enum CurrentAnimation
{
    Direction,
    Hook,
    Ready,
    Guard,
    Uppercut,
    Jap
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
    }

    private void ChangeAnimation(CurrentAnimation currentAnimation, bool parameter)
    {
        _animator.SetBool(currentAnimation.ToString(), parameter);
    }
    
    public void ChangeStateToTrueAnimation(string currentAnimation)
    {
        _animator.SetBool(currentAnimation, true);
    }
    
    public void ChangeStateToFalseAnimation(string currentAnimation)
    {
        _animator.SetBool(currentAnimation, false);
    }
    
    private void ChangeAnimationGameOver()
    {
        _animator.SetTrigger("GameOver");
    }
}
