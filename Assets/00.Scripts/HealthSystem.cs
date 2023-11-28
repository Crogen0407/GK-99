using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public Condition _condition;

    private EnemyState _enemyState;
    
    public Action Dead;
    public Action Damaged;
    
    [SerializeField] private  int hp = 3;

    private void Awake()
    {
        _condition = Condition.GOOD;
        _enemyState = GetComponent<EnemyState>();
    }

    public int Hp
    {
        get => hp;
        set
        {
            if (hp > value)
            {
                Damaged?.Invoke();
            }
            hp = value;
            
            if (hp > 0)
            {
                switch (hp)
                {
                    case 3 : _condition = Condition.GOOD; break;
                    case 2 : _condition = Condition.BAD; break;
                    case 1 : _condition = Condition.DANGEROUS; break;
                }
            }
            if (hp <= 0)
            {
                Dead?.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_enemyState != null)
        {
            if (other.transform.CompareTag("PlayerAttack"))
            {
                Hp--;
            }
        }
    }
}


public interface ILife
{
    public void Dead();
}
