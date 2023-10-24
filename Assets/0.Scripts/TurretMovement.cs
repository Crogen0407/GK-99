using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Vector3 _mainBodyTargetDirection;
    private Vector3 _subBodyTargetDirection;
    
    private Transform _mainBody;
    private Transform _subBody;

    private GameManager _gameManager;
    
    void Start()
    {
        _gameManager = GameManager.Instance;

        _mainBody = transform.Find("Main");
        _subBody = _mainBody.Find("Sub");
    }

    private void Update()
    {
        _mainBodyTargetDirection = _target.position - _mainBody.position;
        _subBodyTargetDirection = _target.position - _subBody.position;
    }

    void FixedUpdate()
    {
        Rotate();
    }

    private void Rotate()
    {
        Vector3 mainBodyRotate
            = new Vector3(-90, Mathf.Rad2Deg * Mathf.Atan2(_mainBodyTargetDirection.x, _mainBodyTargetDirection.z), 0);
        
        Vector3 subBodyRotate
            = new Vector3(Mathf.Rad2Deg * Mathf.Atan2(-_subBodyTargetDirection.y, Vector3.Distance(_target.position, _subBody.position)), 0, 0);

        _mainBody.eulerAngles = mainBodyRotate;
        _subBody.localEulerAngles = subBodyRotate;
    }
}
