using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearController : MonoBehaviour
{
    private void FixedUpdate()
    {
        Collider[] col = Physics.OverlapSphere(transform.position, 10, LayerMask.GetMask("Enemy"));
        if (col.Length == 0)
        {
            UIManager.Instance.OnGameClear();
        }
    }

    private void OnGUI()
    {
        Gizmos.DrawSphere(transform.position, 10);
    }
}
