using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tweening;

public class EnemyState : MonoBehaviour, ILife
{
    private Rigidbody _rigidbody;
    private HealthSystem _healthSystem;
    private AudioSource _audioSource;


    private CinemachinePOVExtension _cinemachinePovExtension;

    //Managements
    private GameManager _gameManager;
    private PoolManager _poolManager;
    private EffectController _effectController;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _healthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        _cinemachinePovExtension = CinemachinePOVExtension.Instance;
        _gameManager = GameManager.Instance;
        _poolManager = PoolManager.Instance;
        _effectController = _gameManager.effectController;
        _healthSystem.Dead += Dead;
    }

    public void Damaged(Vector3 damagedDirection)
    {
        Tweening.Instance.DOMove(_rigidbody, -damagedDirection + _rigidbody.position , 0.5f, EasingType.EaseInBack);
    }

    public void Dead()
    {
        PoolManager.Instance.Pop("LightningBall", transform.position, Quaternion.identity);
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5, gameObject.layer);
        if (colliders.Length == 0)
        {
            _gameManager.TimeSlow(0.1f, 0.5f);
        }
        _effectController.CreateEffect("EnemyDieEffect", transform.position + Vector3.up * 1.5f, quaternion.identity, 2);
        _poolManager.Push("FireSpirit", gameObject);
    }
}
