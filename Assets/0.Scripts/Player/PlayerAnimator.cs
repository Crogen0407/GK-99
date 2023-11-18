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
        _playerAttack = GetComponent<PlayerAttack>();
        _animator = transform.Find("Model").Find("Arm").GetComponent<Animator>();
        _playerAttack.ChangeAnimationAction += ChangeAnimation;
        _playerAttack.ChangeAnimationGameOverAction += ChangeAnimationGameOver;
    }

    private void ChangeAnimation(CurrentAnimation currentAnimation, bool parameter)
    {
        _animator.SetBool(currentAnimation.ToString(), parameter);
    }

    public void DD(string name)
    {
        Debug.Log(name);
    }
    
    public void ChangeAnimation(string currentAnimation, bool parameter)
    {
        Debug.Log(parameter);
        _animator.SetBool(currentAnimation, parameter);
    }
    
    private void ChangeAnimationGameOver()
    {
        _animator.SetTrigger("GameOver");
    }
}
