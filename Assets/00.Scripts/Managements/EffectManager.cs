using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private PoolManager _poolManager;

    private void Start()
    {
        _poolManager = PoolManager.Instance;
    }
    
    /// <summary>
    /// PoolBase에 이펙트 프리펩을 추가한 후, PoolBase의 pairs의 요소인 prefabTypeName값으로 effectName를 할당하도록 해야함.
    /// </summary>
    /// <param name="effectName"></param>
    /// <returns></returns>
    public GameObject CreateEffect(Vector3 pos, string effectName)
    {
        return _poolManager.Pop(effectName, pos, quaternion.identity);
    }
}
