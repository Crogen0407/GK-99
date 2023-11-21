using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.ScreenCursorRaycasting;

public class UIEvent : MonoBehaviour
{
    private CinemachineVirtualCamera _vir01;
    private CinemachineVirtualCamera _vir02;

    private void Awake()
    {
        _vir01 = GameObject.Find("Virtual Camera_01").GetComponent<CinemachineVirtualCamera>();
        _vir02 = GameObject.Find("Virtual Camera_02").GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ScreenCursorRaycasting.CursorDirection();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _vir01.MoveToTopOfPrioritySubqueue();

            
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _vir02.MoveToTopOfPrioritySubqueue();
        }
    }
}
