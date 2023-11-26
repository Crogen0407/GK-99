using System;
using Unity.Mathematics;
using UnityEngine;

public class EnemyState : MonoBehaviour, ILife
{
    private Rigidbody _rigidbody;
    private HealthSystem _healthSystem;
    
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
        _gameManager = GameManager.Instance;
        _poolManager = PoolManager.Instance;
        _healthSystem.Dead += Dead;
    }

    public void Damaged(Vector3 damagedDirection)
    {
        _rigidbody.AddForce(damagedDirection * -10, ForceMode.Impulse);
    }

    public void Dead()
    {
        _effectController.CreateEffect("EnemyDieEffect", transform.position, quaternion.identity, 1);
        _poolManager.Push("FireSpirit", gameObject);
    }
}
