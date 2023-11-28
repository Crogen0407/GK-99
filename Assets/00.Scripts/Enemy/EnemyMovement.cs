using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [Header("View")] 
    [SerializeField] private Vector3 Offset;
    public SO_EnemyData enemyData;
    [SerializeField] private bool DebugMode;
    private float _viewAngle;
    float _viewRadius = 1f;
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstacleMask;
    private Vector3 _moveDirection;

    private bool _move;

    //Components
    private NavMeshAgent _agent;
    private EnemyAnimator _enemyAnimator;
    private Rigidbody _rigidbody;
    private EnemyAttack _enemyAttack;
    
    //Managements
    private GameManager _gameManager;

    public bool Move
    {
        get => _move;
        set
        {
            _move = value;
            
        }
    }
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
        _enemyAttack = GetComponent<EnemyAttack>();
        _enemyAnimator = transform.Find("FireSpirit").GetComponent<EnemyAnimator>();
    }

    void Start()
    {
        _gameManager = GameManager.Instance;
        //_animator = transform.Find("Model").GetComponent<Animator>();
        _agent.speed = enemyData.moveSpeed;
        _agent.angularSpeed = enemyData.rotateSpeed;
        _agent.stoppingDistance = enemyData.attackDistance;
        _viewAngle = enemyData.viewAngle;
        _viewRadius = enemyData.viewRadius;
    }

    private void FixedUpdate()
    {
        // if (_rigidbody.velocity.x>0.3f || _rigidbody.velocity.y>0.3f || _rigidbody.velocity.z>0.3f)
        // {
        //     _animator.SetBool("IsMove", true);
        //     if (_agent.speed >= enemyData.moveSpeed)
        //     {
        //         if (_agent.speed >= enemyData.moveSpeed * 2)
        //         {
        //             _animator.SetInteger("SpeedState", 1);
        //         }
        //         else
        //         {
        //             _animator.SetInteger("SpeedState", 0);
        //         }
        //     }
        // }
        // else
        // {
        //     _animator.SetBool("IsMove", false);
        // }
        CheckCollider();
    }

    private void CheckCollider()
    {
        Collider[] target = Physics.OverlapSphere(_moveDirection, _viewRadius, _targetMask);

        if (target.Length == 0) return;
        Vector3 targetDir = Vector3.zero;
        
        foreach (Collider coll in target)
        {
            Vector3 targetVec = coll.transform.position;
            targetDir = (targetVec - _moveDirection).normalized;
            float targetAngle = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(transform.forward, targetDir));
            if (targetAngle  <= _viewAngle * 0.5f)
            {
                _enemyAttack.playerToMyDistance = Vector3.Distance(transform.position, coll.transform.position);
                float distance = Vector3.Distance(coll.transform.position, transform.position);
                Debug.DrawRay(_moveDirection, targetDir * _enemyAttack.playerToMyDistance, Color.green);
                if (!Physics.Raycast(_moveDirection, targetDir * distance, distance, _obstacleMask))
                {
                    _enemyAnimator.GetCurrentAnimatorStateInformation();
                    if (_enemyAttack.playerToMyDistance < 10)
                    {
                        _agent.SetDestination(transform.position);
                        _enemyAttack.OnAttack();
                        transform.forward = targetDir;
                    }
                    else
                    {
                        _agent.SetDestination(coll.transform.position);
                    }
                }
            }
        }
    }
   
    private void OnDrawGizmos()
    {
        if (DebugMode)
        {
            _moveDirection = transform.position + Offset;
            Gizmos.DrawWireSphere(_moveDirection, _viewRadius);

            Vector3 rightDir = AngleToDir(transform.eulerAngles.y + _viewAngle * 0.5f);
            Vector3 leftDir = AngleToDir(transform.eulerAngles.y - _viewAngle * 0.5f);;
            Vector3 looktDir = transform.forward;
            
            Debug.DrawRay(transform.position, rightDir * _viewRadius, Color.magenta);
            Debug.DrawRay(transform.position, leftDir * _viewRadius, Color.magenta);
            Debug.DrawRay(transform.position, looktDir * _viewRadius, Color.green);
        }
    }

    private Vector3 AngleToDir(float angle)
    {
        float radAngle = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radAngle), 0, Mathf.Cos(radAngle));
    }
}
