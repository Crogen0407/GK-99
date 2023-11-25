using System;
using System.Collections;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour, ILife
{
    private HealthSystem _healthSystem;
    
    
    
    //Controllers
    private ScreenEffectController _screenEffectController;
    private CinemachinePOVExtension _cinemachinePovExtension;
    private void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();
    }

    private void OnEnable()
    {
        _healthSystem.Dead += Dead;
        _healthSystem.Damaged += Damaged;
    }

    private void OnDisable()
    {
        _healthSystem.Dead -= Dead;
        _healthSystem.Damaged -= Damaged;
    }

    public void Dead()
    {
        
    }

    public void Damaged()
    {
        _cinemachinePovExtension.CameraShake(5, 10, 1);
        if (_healthSystem.Hp == 1)
        {   
            _screenEffectController.SetBool("Noising", true);
        }
    }
}
