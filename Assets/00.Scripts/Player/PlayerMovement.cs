using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerMovement : MonoBehaviour, IConsoleText
{
    [Header("Movement")]
    public float moveSpeed=10;
    
    private float horizontalInput;
    private float verticalInput;
    
    public Transform orientation;
    public Vector3 moveDirection;
    private Transform model;
    
    private float rotateY;

    //Managements
    private GameManager _gameManager;
    private CinemachinePOVExtension _cinemachinePovExtension;
    
    //Controller
    private ConsoleTextController _consoleTextController;
    private ScreenEffectController _screenEffectController;
    
    //Components
    private Rigidbody _rigidbody;
    private HealthSystem _healthSystem;
    private PlayerDash _playerDash;
    private PlayerJump _playerJump;
    private CameraShakeController _cameraShakeController;
    
    //Action
    float clampingNumberX = 0;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _healthSystem = GetComponent<HealthSystem>();
        _playerDash = GetComponent<PlayerDash>();
        _playerJump = GetComponent<PlayerJump>();
        
        _rigidbody.freezeRotation = true;
        model = transform.Find("Model");
    }

    void Start()
    {
        _gameManager = GameManager.Instance;
        _cinemachinePovExtension = CinemachinePOVExtension.Instance;

        _cameraShakeController = _gameManager.cameraShakeController;
        _consoleTextController = _gameManager.consoleTextController;
        _screenEffectController = _gameManager.screenEffectController;

        _isWalking = false;
    }

    //Set Move Direction
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 vec = context.ReadValue<Vector2>();
        horizontalInput = vec.x;
        verticalInput = vec.y;
    }

    public void OnMoveCancel(InputAction.CallbackContext context)
    {
        horizontalInput = 0;
        verticalInput = 0;
    }
    
    private void Rotate()
    {
        moveDirection = new Vector3(orientation.forward.x,0,orientation.forward.z) * verticalInput + orientation.right * horizontalInput;
    }

    private bool _isWalking = false;
    private void Move()
    {
        if (_playerDash.dashing == true) return;
        
        Vector3 vec = moveDirection.normalized * moveSpeed;
        
        _rigidbody.velocity = new Vector3(vec.x, _rigidbody.velocity.y, vec.z);
        
        if (vec.magnitude >= 0.1f)
        {
            if (_playerJump.Jumping == true)
            {
                _cameraShakeController.EndShake();
                return; 
            }
            _cameraShakeController.Shake(Random.Range(0.4f, 1), 0.1f);
            _screenEffectController.SetBool("Breathing", false);
            _isWalking = true;
        }
        else
        {
            if (_isWalking == true)
            {
                _cameraShakeController.EndShake();
            }
            _screenEffectController.SetBool("Breathing", true);
        }
    }
    
    
    private Vector3 velocity;

    private void FixedUpdate()
    {
        Rotate();        
        Move();
        ConsoleTextUpdate();
        Vector3 vec = model.forward;
        model.forward = Vector3.SmoothDamp(vec, orientation.forward, ref velocity, 0.1f, 800);
        
    }

    public void ConsoleTextUpdate()
    {
        _consoleTextController.POSITION = transform.position;
        _consoleTextController.VELOCITY = _rigidbody.velocity;
        _consoleTextController.ROTATION = orientation.eulerAngles;
    }
}

