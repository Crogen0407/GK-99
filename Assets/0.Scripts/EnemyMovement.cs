using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed=10;

    [Header("View")] 
    [SerializeField] private bool DebugMode;

    [SerializeField] [Range(0, 360)] private float _viewAngle;
    [SerializeField] float _viewRadius = 1f;
    private List<Collider> hitTargetList;
    [SerializeField] LayerMask _targetMask;
    [SerializeField] LayerMask _obstacleMask;
    private Vector3 _moveDirection;
    
    private GameManager _gameManager;
    private PlayerMovement _playerMovement;

    
    void Start()
    {
        _gameManager = GameManager.Instance;
        _playerMovement = _gameManager.playerMovement;
        hitTargetList = new List<Collider>();
    }

    void Update()
    {
        print(transform.forward);
        CheckCollider();
    }

    private void FixedUpdate()
    {
        throw new NotImplementedException();
    }

    private void CheckCollider()
    {
        hitTargetList.Clear();
        Collider[] target = Physics.OverlapSphere(_moveDirection, _viewRadius, _targetMask);

        if (target.Length == 0) return;
        Vector3 targetDir = Vector3.zero;
        foreach (Collider coll in target)
        {
            print("df");
            Vector3 targetVec = coll.transform.position;
            targetDir = (targetVec - _moveDirection).normalized;
            targetDir.y = 0;
            float targetAngle = Mathf.Rad2Deg * Mathf.Atan2((targetVec - transform.position).z, (targetVec - transform.position).x) - 90 + transform.eulerAngles.y;
            if (Mathf.Abs(targetAngle) <= _viewAngle * 0.5f)
            {
                Debug.DrawRay(transform.position, targetDir * Vector3.Distance(coll.transform.position, transform.position), Color.green);
                hitTargetList.Add(coll);
            }
        }

        //가장 가까운 물체로의 방향 구하기
        Collider minDistanceCollider = hitTargetList[0];
        Vector3 mainTargetDir = Vector3.zero;

        foreach (Collider hitTarget in hitTargetList)
        {
            if (Vector3.Distance(transform.position,minDistanceCollider.transform.position)
                >= Vector3.Distance(transform.position,hitTarget.transform.position))
            {
                minDistanceCollider = hitTarget;
                mainTargetDir = (minDistanceCollider.transform.position - transform.position).normalized;
            }
        }
        Debug.DrawRay(transform.position, mainTargetDir * Vector3.Distance(minDistanceCollider.transform.position, transform.position), Color.red);

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
