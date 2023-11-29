using System;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //Managements
    private GameManager _gameManager;
    private PoolManager _poolManager;    

    //Controllers
    private EffectController _effectController;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _poolManager = PoolManager.Instance;
        
        _effectController = _gameManager.effectController;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Enemy") == false)
        {
            _effectController.CreateEffect("EnemyAttackHitEffect", transform.position, Quaternion.identity, 1.5f);
            _poolManager.Push("EnemyBullet", gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Enemy") == false)
        {
            _effectController.CreateEffect("EnemyAttackHitEffect", transform.position, Quaternion.identity, 1.5f);
            _poolManager.Push("EnemyBullet", gameObject);
        }
    }
}
