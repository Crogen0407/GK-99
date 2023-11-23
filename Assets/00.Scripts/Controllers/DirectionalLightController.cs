using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalLightController : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float speed = 1;
    
    void FixedUpdate()
    {
        _transform.eulerAngles += new Vector3(0, Time.fixedDeltaTime * speed, 0);
    }
}
