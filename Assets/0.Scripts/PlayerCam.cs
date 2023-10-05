using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    private float xRotation;
    private float yRotation;
    
    [SerializeField] private float _minRotateX;
    [SerializeField] private float _maxRotateX;
    
    float clampingNumberX = 0;

    private void Awake()
    {
        Application.targetFrameRate = 85;
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        yRotation += mouseX;
        orientation.eulerAngles = new Vector3(0,yRotation, 0);


        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, _minRotateX, _maxRotateX);
        transform.eulerAngles = new Vector3(xRotation, yRotation, 0);
    }
}
