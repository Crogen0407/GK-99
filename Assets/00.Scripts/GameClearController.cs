using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearController : MonoBehaviour
{
    private void FixedUpdate()
    {
        Collider[] col = Physics.OverlapSphere(transform.position, 15, LayerMask.GetMask("Enemy"));
        Debug.Log(col.Length);
        if (col.Length == 0)
        {
            UIManager.Instance.OnGameClear();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 15);
    }
}
