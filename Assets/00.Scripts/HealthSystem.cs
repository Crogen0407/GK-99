using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public Condition _condition;
    
    public Action Dead;
    public Action Damaged;
    [SerializeField] private  int hp = 100;

    private void Awake()
    {
        _condition = Condition.GOOD;
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
}

public interface ILife
{
    public void Dead();
}
