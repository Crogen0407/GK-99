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

<<<<<<< HEAD
    //Camera Rotate
    public float sensX;
    public float sensY;
    
    private float xRotation;
    private float yRotation;
    
    [SerializeField] private float _minRotateX;
    [SerializeField] private float _maxRotateX;
    
    float clampingNumberX = 0;

    private Camera _mainCamera;
    
=======
>>>>>>> parent of fb63e91 (타일링 노가다중...)
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
<<<<<<< HEAD
       
    }

    private void FixedUpdate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        yRotation += mouseX;
        _mainCamera.transform.eulerAngles = new Vector3(0,yRotation, 0);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, _minRotateX, _maxRotateX);
        transform.eulerAngles = new Vector3(xRotation, yRotation, 0);
        
=======
>>>>>>> parent of fb63e91 (타일링 노가다중...)
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
        
        Vector3 vec = moveDirection.normalized * moveSpeed;
        _rigidbody.velocity = new Vector3(vec.x, _rigidbody.velocity.y, vec.z);
        
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        SpeedControl();
    }
}

