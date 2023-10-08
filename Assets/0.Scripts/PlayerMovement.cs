using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
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

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
        model = transform.Find("Model");
    }

    void Start()
    {
        _gameManager = GameManager.Instance;
    }
    

    private void OnMove(InputValue value)
    {
        Vector2 vec = value.Get<Vector2>();
        horizontalInput = vec.x;
        verticalInput = vec.y;
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
        
        //limit velocity if need
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            _rigidbody.velocity = new Vector3(limitedVel.x, _rigidbody.velocity.y, limitedVel.z);
        }
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
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        model.forward = orientation.forward;
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

    private void FixedUpdate()
    {
        _rigidbody.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        SpeedControl();
    }
}

