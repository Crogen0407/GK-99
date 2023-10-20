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
    
    //Components
    private Rigidbody _rigidbody;
    private HealthSystem _healthSystem;
    
    //Camera Rotate
    public Camera _mainCamera;
    public float sensX;
    public float sensY;

    private float xRotation;
    private float yRotation;
    
    [SerializeField] private float _minRotateX;
    [SerializeField] private float _maxRotateX;
    
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
    }

    public void Dead()
    {
        
    }

    public void Revival()
    {
        
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

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, _minRotateX, _maxRotateX);
        _mainCamera.transform.eulerAngles = new Vector3(xRotation, yRotation, 0);
        
        moveDirection = new Vector3(orientation.forward.x,0,orientation.forward.z) * verticalInput + orientation.right * horizontalInput;
        model.forward = orientation.forward;
    }

    private void FixedUpdate()
    {
        Vector3 vec = moveDirection.normalized * moveSpeed;
        _rigidbody.velocity = new Vector3(vec.x, _rigidbody.velocity.y, vec.z);
        
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
}

