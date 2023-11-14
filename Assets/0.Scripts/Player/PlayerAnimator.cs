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
    
    private PlayerMovement _playerMovement;
    private Animator _animator;
    
    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = transform.Find("Model").Find("Arm").GetComponent<Animator>();
        _playerMovement.ChangeAnimationAction += ChangeAnimation;
        _playerMovement.ChangeAnimationGameOverAction += ChangeAnimationGameOver;
    }

    private void ChangeAnimation(CurrentAnimation currentAnimation, bool parameter)
    {
        _animator.SetBool(currentAnimation.ToString(), parameter);
    }
    
    private void ChangeAnimationGameOver()
    {
        _animator.SetTrigger("GameOver");
    }
    
    void Update()
    {
        
    }
}
