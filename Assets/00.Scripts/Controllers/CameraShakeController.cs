using System;
using Cinemachine;
using UnityEngine;

public class CameraShakeController : MonoBehaviour
{
    private CinemachineImpulseSource _cameraCinemachine;

    private void Awake()
    {
        _cameraCinemachine = GetComponent<CinemachineImpulseSource>();
    }

    public void Shake(float power)
    {
        _cameraCinemachine.GenerateImpulse(power);
    }
}
