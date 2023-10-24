using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField] private float duration;
    private float curTime;
    private float percentTime;
    private Vector3 curVector;
    void Awake()
    {
        playerTransform = GameObject.Find("Player").transform;
        transform.position = playerTransform.position;
        curTime = 0;
        percentTime = 1;
        curVector = transform.position;
    }

    void Update()
    {
        Move();
    }
    
    private void Move()
    {
        curTime += Time.deltaTime;
        percentTime = curTime / duration;
        if (percentTime > 1)
        {
            curTime = 0;
            curVector = transform.position;
        }

        transform.position = Vector3.Lerp(curVector, playerTransform.position, percentTime);
    }
}
