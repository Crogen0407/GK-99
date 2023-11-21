using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public Action Dead;
    [SerializeField] private  int hp = 100;

    public int Hp
    {
        get => hp;
        set
        {
            hp = value;
            if (hp <= 0)
            {
                Dead?.Invoke();
            }
        }
    }
}

public interface IDead
{
    public void Dead();

    public void Revival();
}
