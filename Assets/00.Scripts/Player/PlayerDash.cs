using System;
using System.Collections;
using UnityEngine;
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
        _cinemachinePovExtension = CinemachinePOVExtension.Instance;
        
        _consoleTextController = ConsoleTextController.Instance;
        _screenEffectController = ScreenEffectController.Instance;

    }

    private IEnumerator EndDash()
    {
        yield return null;
        dashing = false;
        _screenEffectController.SetBool("BlurEffect", false);
        _cinemachinePovExtension.CameraExpand(60);
    }
    
    private void OnDash()
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
            Debug.DrawRay(_rigidbody.position, rayDirection, Color.green, .1f);
            if (_currentdelayTimer > dashDelay)
            {
                if (rayDirection.magnitude > Vector3.Distance(hit.point, transform.position))
                {
                    _currentdelayTimer = 0;
                    dashing = true;
                    Tweening.Instance.DOMove(_rigidbody,  hit.point - rayDirection * 0.01f, 0.3f, EndDash(), EasingType.EaseOutSine);
                    _screenEffectController.SetBool("BlurEffect", true);
                    _cinemachinePovExtension.CameraExpand(58);
                    return;
                }
                
                _currentdelayTimer = 0;
                dashing = true;
                Tweening.Instance.DOMove(_rigidbody, dashEndPoint, 0.3f, EndDash(), EasingType.EaseOutSine);
                _screenEffectController.SetBool("BlurEffect", true);
                _cinemachinePovExtension.CameraExpand(58);
            }
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
