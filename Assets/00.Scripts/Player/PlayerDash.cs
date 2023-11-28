using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tweening;

public class PlayerDash : MonoBehaviour, IConsoleText
{
    public float dashForce = 20;
    public bool dashing;
    public float dashDelay= 5;

    private float _currentdelayTimer;
    private Vector3 dashEndPoint;
    
    //Managements
    private GameManager _gameManager;
    private CinemachinePOVExtension _cinemachinePovExtension;
    private ScreenEffectController _screenEffectController;
    
    //Controllers
    private ConsoleTextController _consoleTextController;
    private EffectController _effectController;
    
    //Components
    private Rigidbody _rigidbody;
    private PlayerMovement _playerMovement;
    
    private void Awake()
    {
        _currentdelayTimer = dashDelay;
        
        _rigidbody = GetComponent<Rigidbody>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _effectController = _gameManager.effectController;
        _cinemachinePovExtension = CinemachinePOVExtension.Instance;
        
        _consoleTextController = _gameManager.consoleTextController;
        _screenEffectController = _gameManager.screenEffectController;

    }
    
    public void OnDash(InputAction.CallbackContext context)
    {
        Vector3 moveDirection = new Vector3(
            x : _playerMovement.moveDirection.x, 
            y :  0,
            z : _playerMovement.moveDirection.z
            );
        
        Vector3 rayDirection = new Vector3(
            x : (_playerMovement.orientation.forward * dashForce).x,
            y : 0,
            z : (_playerMovement.orientation.forward * dashForce).z
            );
        
        if (moveDirection.sqrMagnitude > 0.1f)
        {
            dashEndPoint = new Vector3(
                x : (_rigidbody.position.x + rayDirection.x), 
                y : _rigidbody.position.y, 
                z : (_rigidbody.position.z + rayDirection.z)
                );
            RaycastHit hit;
            Physics.Raycast(_rigidbody.position, rayDirection, out hit);
            if (_currentdelayTimer > dashDelay)
            {
                if (rayDirection.magnitude > Vector3.Distance(hit.point, transform.position))
                {
                    _currentdelayTimer = 0;
                    dashing = true;
                    Tweening.Instance.DOMove(_rigidbody,  hit.point - rayDirection.normalized * 0.5f, 0.4f, EndDash(hit.point, true), EasingType.EaseOutExpo);
                    _screenEffectController.SetBool("BlurEffect", true);
                    _cinemachinePovExtension.CameraExpand(58);
                    return;
                }
                
                _currentdelayTimer = 0;
                dashing = true;
                Tweening.Instance.DOMove(_rigidbody, dashEndPoint, 0.4f, EndDash(Vector3.zero), EasingType.EaseOutCubic);
                _screenEffectController.SetBool("BlurEffect", true);
                _cinemachinePovExtension.CameraExpand(58);
            }
        }
    }

    private IEnumerator EndDash(Vector3 hitWallPosition, bool isHitWall = false)
    {
        yield return null;
        dashing = false;
        _screenEffectController.SetBool("BlurEffect", false);
        _cinemachinePovExtension.CameraExpand(60);
        if (isHitWall == true)
        {
            Vector3 directionVector = new Vector3(90, 0,
                Camera.main.transform.eulerAngles.y - 180);
            _effectController.CreateEffect("LandingEffect", hitWallPosition, Quaternion.Euler(directionVector), 2);
        }
    }
    
    private void Update()
    {
        _currentdelayTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        ConsoleTextUpdate();
    }

    public void ConsoleTextUpdate()
    {
        _consoleTextController.DASH = dashing;
    }
}
