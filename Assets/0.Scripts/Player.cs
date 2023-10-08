using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed=10;
    private PlayerInput _playerInput;
    
    private Vector3 dir;

    private CameraRotate _cameraRotate;
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _cameraRotate = transform.Find("Main Camera").GetComponent<CameraRotate>();
    }

    void Start()
    {
        
    }

    private void OnMove(InputValue value)
    {
        Vector2 vec = value.Get<Vector2>();
        dir = new Vector3(vec.x, 0, vec.y);
    }
    

    private void Update()
    {
        transform.localPosition += dir * moveSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0,_cameraRotate.rotateY,0);
    }
}
