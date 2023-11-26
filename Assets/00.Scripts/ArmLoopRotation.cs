using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmLoopRotation : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 10;
    [SerializeField] private Vector3 axisDirection = Vector3.up;
    void FixedUpdate()
    {
        transform.eulerAngles += axisDirection * rotateSpeed * Time.fixedDeltaTime;
    }
}
