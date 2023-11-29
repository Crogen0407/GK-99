using System;
using System.Collections;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour, ILife
{
    private HealthSystem _healthSystem;
    
    //Managements
    private GameManager _gameManager;
    
    //Controllers
    private ConsoleTextController _consoleTextController;
    private ScreenEffectController _screenEffectController;
    private CinemachinePOVExtension _cinemachinePovExtension;
    
    private void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.Dead += Dead;
        _healthSystem.Damaged += Damaged;
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _cinemachinePovExtension = CinemachinePOVExtension.Instance;
        _consoleTextController = _gameManager.consoleTextController;
        _screenEffectController = _gameManager.screenEffectController;
    }

    private void OnEnable()
    {
        if (_healthSystem != null && _healthSystem.Dead == null)
        {
            _healthSystem.Dead += Dead;
            _healthSystem.Damaged += Damaged;
        }
    }

    private void OnDisable()
    {
        _healthSystem.Dead -= Dead;
        _healthSystem.Damaged -= Damaged;
    }

    public void Dead()
    {
        
    }

    private void Damaged()
    {
        _consoleTextController.CONDITION = _healthSystem.condition;
        _gameManager.cameraShakeController.Shake(10);
        if (_healthSystem.Hp == 1)
        {   
            _screenEffectController.SetBool("Noising", true);
        }
    }
}
