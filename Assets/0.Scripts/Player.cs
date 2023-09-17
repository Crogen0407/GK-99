using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed=10;
    public bool isGround;
    
    [SerializeField] private float rotateSpeed = 50;
    private PlayerInput _playerInput;
    
    private Vector3 dir;

    private CameraRotate _cameraRotate;
    private float rotateY;

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
        rotateY -= Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;

        Vector3 vec = new Vector3(Mathf.Cos(rotateY), 0, Mathf.Sin(rotateY)).normalized;
        transform.forward = vec;
        transform.Translate( dir * moveSpeed * Time.deltaTime, Space.Self);
    }
}
