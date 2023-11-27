using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "SO/EnemyData")]
public class SO_EnemyData : ScriptableObject
{
    public float moveSpeed;
    public float rotateSpeed;
    public float viewAngle;
    public float viewRadius;
    public float attackDistance;
}
