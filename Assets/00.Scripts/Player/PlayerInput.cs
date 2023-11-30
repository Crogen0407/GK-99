using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private CustomInput _input;

    private PlayerMovement _playerMovement;
    private PlayerDash _playerDash;
    private PlayerAttack _playerAttack;
    private PlayerJump _playerJump;
    private PlayerAnimator _playerAnimator;
    
    private void Awake()
    {
        _input = new CustomInput();

        _playerMovement = GetComponent<PlayerMovement>();
        _playerDash = GetComponent<PlayerDash>();
        _playerAttack = GetComponent<PlayerAttack>();
        _playerJump = GetComponent<PlayerJump>();
        _playerAnimator = transform.Find("Model/Arm").GetComponent<PlayerAnimator>();

    }

    private void OnEnable()
    {
        _input.Enable();

        _input.Player.Move.performed += _playerMovement.OnMove;
        _input.Player.Move.canceled += _playerMovement.OnMoveCancel;
        
        _input.Player.Dash.performed += _playerDash.OnDash;
        _input.Player.Attack.performed += _playerAnimator.GetCurrentAnimatorStateInformation;
        _input.Player.Attack.performed += _playerAttack.OnAttack;
        _input.Player.SwitchAttackMode.performed += _playerAttack.OnSwitchAttackMode;
        _input.Player.Jump.performed += _playerJump.OnJump;
    }

    private void OnDisable()
    {
        _input.Disable();
        
        _input.Player.Move.performed -= _playerMovement.OnMove;
        _input.Player.Move.canceled -= _playerMovement.OnMoveCancel;

        _input.Player.Dash.performed -= _playerDash.OnDash;
        _input.Player.Attack.performed -= _playerAnimator.GetCurrentAnimatorStateInformation;
        _input.Player.Attack.performed -= _playerAttack.OnAttack;
        _input.Player.SwitchAttackMode.performed -= _playerAttack.OnSwitchAttackMode;
        _input.Player.Jump.performed -= _playerJump.OnJump;
    }
}
