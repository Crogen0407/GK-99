using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.Tweening;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IDead
{
    [Header("Movement")]
    public float moveSpeed=10;
    public float jumpForce=10;
    public float dashForce = 20;
    
    private bool jumpSpeedUp;
    
    public float groundDrag;

    private float horizontalInput;
    private float verticalInput;
    
    public float jumpCooldown;
    public float dashDelay= 5;
    private float _currentdelayTimer;
    private bool readyTojump;
    private bool _dashing;
    private Vector3 dashEndPoint;

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
    private ScreenEffectController _screenEffectController;
    
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
        
        _currentdelayTimer = dashDelay;
        
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
        _screenEffectController = ScreenEffectController.Instance;
        
        ResetJump();
        JumpCheck();
        OnJump();
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

    private IEnumerator EndDash()
    {
        yield return null;
        _dashing = false;
        _screenEffectController.SetBool("BlurEffect", false);
        _cinemachinePovExtension.CameraExpand(60);
    }
    
    #region InputSystem

        //Set Move Direction
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

        private void OnAttackStateChange()
        {
            
        }

        private void OnAttack()
        {
            
        }
        
        private void OnDash()
        {
            if (new Vector3(moveDirection.normalized.x, 0, moveDirection.normalized.z).magnitude > 0.1f)
            {
                dashEndPoint = new Vector3((_rigidbody.position + orientation.forward * dashForce).x, _rigidbody.position.y, (_rigidbody.position + orientation.forward * dashForce).z) ;
                if (_currentdelayTimer > dashDelay)
                {
                    _currentdelayTimer = 0;
                    _dashing = true;
                    Tweening.Instance.DOMove(_rigidbody,  dashEndPoint, 0.3f, EndDash(), EasingType.EaseOutSine);
                    _screenEffectController.SetBool("BlurEffect", true);
                    _cinemachinePovExtension.CameraExpand(58);
                }
            }
        }

    #endregion

    private void OnDrawGizmos()
    {
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
        if (_dashing == true) return;
        Vector3 vec = moveDirection.normalized * moveSpeed;

        _rigidbody.velocity = new Vector3(vec.x, _rigidbody.velocity.y, vec.z);
        if (vec.magnitude >= 0.1f)
        {
            _cinemachinePovExtension.StartCameraShake(2f, 0.06f);
        }
        else
        {
            _cinemachinePovExtension.StopCameraShake();
        }
    }

    private void JumpCheck()
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
        _currentdelayTimer += Time.deltaTime;
    }
    
    private void FixedUpdate()
    {
        Move();
        JumpCheck();
    }
}

