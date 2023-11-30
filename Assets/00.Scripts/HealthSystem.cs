using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public Condition condition;
    public Vector3 damagedDirection;
    
    private EnemyState _enemyState;
    private PlayerAttack _playerAttack;
    
    public Action Dead;
    public Action Damaged;
    
    [SerializeField] private int hp = 3;

    private void Awake()
    {
        _enemyState = GetComponent<EnemyState>();
    }

    private void Start()
    {
        _playerAttack = FindObjectOfType<PlayerAttack>();
    }

    public int Hp
    {
        get => hp;
        set
        {
            int currentHp = hp;
            hp = value;
            if (hp > 0)
            {
                switch (hp)
                {
                    case 3 : condition = Condition.GOOD; break;
                    case 2 : condition = Condition.BAD; break;
                    case 1 : condition = Condition.DANGEROUS; break;
                }
            }
            if (currentHp > hp)
            {
                Damaged?.Invoke();
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
            damagedDirection = other.transform.position - transform.position;
            if (other.transform.CompareTag("PlayerAttack"))
            {
                switch (_playerAttack.currentAttackMode)
                {
                    case Attack.JAP :
                        Hp--;   
                        break;
                    case Attack.HOOK :
                        Hp-=3;   
                        break;
                    case Attack.UPPERCUT :
                        Hp-=4;   
                        break;
                }
            }
        }
    }
}


public interface ILife
{
    public void Dead();
}
