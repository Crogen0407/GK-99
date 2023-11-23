using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IDead, IConsoleText
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
    
    //Action
    float clampingNumberX = 0;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _healthSystem = GetComponent<HealthSystem>();
        _playerDash = GetComponent<PlayerDash>();
        _playerJump = GetComponent<PlayerJump>();
        
        _rigidbody.freezeRotation = true;
        _healthSystem.Dead += Dead;
        model = transform.Find("Model");
    }

    private void OnDisable()
    {
        _healthSystem.Dead -= Dead;
    }

    void Start()
    {
        _gameManager = GameManager.Instance;
        _cinemachinePovExtension = CinemachinePOVExtension.Instance;
        
        _screenEffectController = ScreenEffectController.Instance;
        _consoleTextController = ConsoleTextController.Instance;
    }

    public void Dead()
    {
        
    }

    public void Revival()
    {
        
    }

    private void OnAttackStateChange(InputValue value)
    {
        float a = value.Get<float>();
        Debug.Log(Time.realtimeSinceStartup + " : " + a);
    }

    //Set Move Direction
    private void OnMove(InputValue value)
    {
        Vector2 vec = value.Get<Vector2>();
        horizontalInput = vec.x;
        verticalInput = vec.y;
    }
    
    private void Rotate()
    {
        moveDirection = new Vector3(orientation.forward.x,0,orientation.forward.z) * verticalInput + orientation.right * horizontalInput;
    }

    private void Move()
    {
        if (_playerDash.dashing == true) return;
        Vector3 vec = moveDirection.normalized * moveSpeed;

        _rigidbody.velocity = new Vector3(vec.x, _rigidbody.velocity.y, vec.z);
        if (vec.magnitude >= 0.1f)
        {
            if (_playerJump.Jumping == true)
            {
                _cinemachinePovExtension.StartCameraShake(1.6f, 0.01f);
                return;
            }
            _cinemachinePovExtension.StartCameraShake(2f, 0.06f);
            _screenEffectController.SetBool("Breathing", false);
        }
        else
        {
            _cinemachinePovExtension.StopCameraShake();
            _screenEffectController.SetBool("Breathing", true);
        }
    }
    
    private Vector3 velocity;

    private void FixedUpdate()
    {
        Rotate();        
        Move();
        consoleTextUpdate();
        Vector3 vec = model.forward;
        model.forward = Vector3.SmoothDamp(vec, orientation.forward, ref velocity, 0.1f, 500);
        
    }

    public void consoleTextUpdate()
    {
        _consoleTextController.POSITION = transform.position;
        _consoleTextController.ROTATION = transform.eulerAngles;
    }
}

