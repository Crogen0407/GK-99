using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [Header("View")] 
    public SO_EnemyData enemyData;
    [SerializeField] private bool DebugMode;
    private float _viewAngle;
    float _viewRadius = 1f;
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstacleMask;
    private Vector3 _moveDirection;

    //Components
    private NavMeshAgent agent;
    
    //Managements
    private GameManager _gameManager;
    
    void Start()
    {
        _gameManager = GameManager.Instance;
        agent = GetComponent<NavMeshAgent>();
        
        agent.speed = enemyData.moveSpeed;
        agent.stoppingDistance = enemyData.attackDistance;
        _viewAngle = enemyData.viewAngle;
        _viewRadius = enemyData.viewRadius;
    }

    private void FixedUpdate()
    {
        CheckCollider();
    }

    private void CheckCollider()
    {
        Collider[] target = Physics.OverlapSphere(_moveDirection, _viewRadius, _targetMask);

        if (target.Length == 0) return;
        Vector3 targetDir = Vector3.positiveInfinity;
        
        foreach (Collider coll in target)
        {
            Vector3 targetVec = coll.transform.position;
            targetDir = (targetVec - _moveDirection).normalized;
            targetDir.y = 0;
            float targetAngle = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(transform.forward, targetDir));
            print(targetAngle);
            if (targetAngle  <= _viewAngle * 0.5f)
            {
                float distance = Vector3.Distance(coll.transform.position, transform.position);
                Debug.DrawRay(transform.position, targetDir * Vector3.Distance(coll.transform.position, transform.position), Color.green);
                if (!Physics.Raycast(transform.position, targetDir * distance, distance, _obstacleMask))
                {
                    agent.SetDestination(coll.transform.position);
                }
            }
        }
    }
   
    private void OnDrawGizmos()
    {
        if (DebugMode)
        {
            _moveDirection = transform.position;
            Gizmos.DrawWireSphere(_moveDirection, _viewRadius);

            Vector3 rightDir = AngleToDir(transform.eulerAngles.y + _viewAngle * 0.5f);
            Vector3 leftDir = AngleToDir(transform.eulerAngles.y - _viewAngle * 0.5f);;
            Vector3 looktDir = transform.forward;
            
            Debug.DrawRay(transform.position, rightDir * _viewRadius, Color.blue);
            Debug.DrawRay(transform.position, leftDir * _viewRadius, Color.blue);
            Debug.DrawRay(transform.position, looktDir * _viewRadius, Color.cyan);
        }
    }

    private Vector3 AngleToDir(float angle)
    {
        float radAngle = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radAngle), 0, Mathf.Cos(radAngle));
    }
}
