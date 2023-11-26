using System;
using UnityEngine;

public class EnemyState : MonoBehaviour, ILife
{
    private Rigidbody _rigidbody;
    private HealthSystem _healthSystem;
    
    //Managements
    private GameManager _gameManager;
    private PoolManager _poolManager;
    
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
        Debug.Log("쥭어셩");
        _rigidbody.AddForce(damagedDirection * -10, ForceMode.Impulse);
    }

    public void Dead()
    {
        _poolManager.Push("FireSpirit", gameObject);
    }
}
