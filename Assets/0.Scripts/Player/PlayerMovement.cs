using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IDead
{
    [Header("Movement")]
    public float moveSpeed=10;

    public float groundDrag;

    private float horizontalInput;
    private float verticalInput;
    
    public float jumpForce;
    public float jumpCooldown;
    private bool readyTojump;
    
    [Header("Ground Check")] 
    public float playerHeight;
    public LayerMask whatIsGround;
    private bool isGrounded;
    
    public Transform orientation;
    private Vector3 moveDirection;
    private Transform model;
    
    private float rotateY;
    
    //Managements
    private GameManager _gameManager;
    private CinemachinePOVExtension _cinemachinePovExtension;
    
    //Components
    private Rigidbody _rigidbody;
    private HealthSystem _healthSystem;
    
    //Camera Rotate
    public Camera _mainCamera;

    //Action
    public Action<CurrentAnimation, bool> ChangeAnimationAction;
    public Action ChangeAnimationGameOverAction;
    
    
    float clampingNumberX = 0;
    
    private void Awake()
    {
        _mainCamera = Camera.main;
        
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
        _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.Dead += Dead;
        model = transform.Find("Model");
    }

    void Start()
    {
        _gameManager = GameManager.Instance;
        _cinemachinePovExtension = CinemachinePOVExtension.Instance;
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
        Debug.Log(a);
    }
    
    private void OnMove(InputValue value)
    {
        Vector2 vec = value.Get<Vector2>();
        horizontalInput = vec.x;
        verticalInput = vec.y;
    }

    private void OnJump()
    {
        Invoke(nameof(ResetJump), jumpCooldown);

        if (readyTojump && isGrounded)
        {
            readyTojump = false;
        
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
        
            _rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        
        }
    }

    private void ResetJump()
    {
        readyTojump = true;
    }
    
    private void Rotate()
    {
        moveDirection = new Vector3(orientation.forward.x,0,orientation.forward.z) * verticalInput + orientation.right * horizontalInput;
        model.forward = orientation.forward;
    }

    private void Move()
    {
        Vector3 vec = moveDirection.normalized * moveSpeed;
        _rigidbody.velocity = new Vector3(vec.x, _rigidbody.velocity.y, vec.z);
        if (vec.magnitude >= 0.1f)
        {
            _cinemachinePovExtension.StartCameraShake(0.2f, 10);
        }
        else
        {
            _cinemachinePovExtension.StopCameraShake();
        }
    }

    private void Jump()
    {
        //ground check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        if (isGrounded)
        {
            _rigidbody.drag = groundDrag;
        }
        else
        {
            _rigidbody.drag = 0;
        }
    }
    
    private void Update()
    {
        Rotate();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }
}

