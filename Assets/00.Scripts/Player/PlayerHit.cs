using System;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    //Components
    private HealthSystem _healthSystem;

    private void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("EnemyBullet"))
        {
            _healthSystem.Hp--;
        }
    }
}
