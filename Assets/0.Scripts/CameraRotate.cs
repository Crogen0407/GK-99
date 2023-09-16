using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private float _minRotateX;
    [SerializeField] private float _maxRotateX;
    
    private float rotateX;
    public float rotateY;
    
    void Awake()
    {
    }
    
    float clampingNumberX = 0;
    
    void Update()
    {
        rotateX += Input.GetAxis("Mouse Y") * 3;
        rotateY += Input.GetAxis("Mouse X") * 10;
        clampingNumberX = Mathf.Clamp(rotateX, _minRotateX, _maxRotateX);
        transform.eulerAngles = new Vector3(-clampingNumberX , transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
