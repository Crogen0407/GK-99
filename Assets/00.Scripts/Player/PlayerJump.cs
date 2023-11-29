using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour, IConsoleText
{
    private bool jumpSpeedUp;
    private float groundDrag = 4;

    private float _playerHeight;
    private float _playerOffset;
    
    [Header("Ground Check")] 
    [SerializeField] private LayerMask whatIsGround;
    public float jumpForce=10;
    public bool Jumping { get; private set; }
    private bool isGrounded;
    
    //Managements
    private GameManager _gameManager;
    private CinemachinePOVExtension _cinemachinePovExtension;

    //Controllers
    private ConsoleTextController _consoleTextController;
    private EffectController _effectController;
    
    //Components
    private Rigidbody _rigidbody;
    private CapsuleCollider _collider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        
        _playerHeight = _collider.height;
        _playerOffset = _collider.center.y;
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        
        _consoleTextController = _gameManager.consoleTextController;
        _effectController = _gameManager.effectController;
        
        _cinemachinePovExtension = CinemachinePOVExtension.Instance;
        
        isGrounded = true;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            _rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);

            
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isGrounded)
        {
            _gameManager.cameraShakeController.Shake(5);
            
            _effectController.CreateEffect("LandingEffect", transform.position - new Vector3(0, 1, 0),
                Quaternion.identity, 2);
        }
    }

    private void JumpCheck()
    {
        //ground check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, (_playerHeight + _playerOffset) * 0.5f + 0.2f, whatIsGround);
        if (isGrounded)
        {
            _rigidbody.drag = groundDrag;
            Jumping = false;
        }
        else
        {
            _rigidbody.drag = 0;
            Jumping = true;
        }
    }
    
    private void FixedUpdate()
    {
        JumpCheck();
        ConsoleTextUpdate();
    }

    public void ConsoleTextUpdate()
    {
        _consoleTextController.JUMP = Jumping;
    }
}
