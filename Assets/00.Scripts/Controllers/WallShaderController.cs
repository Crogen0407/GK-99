using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallShaderController : MonoBehaviour
{
    private Renderer _renderer;
    
    void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.SetInt("_Seed", Random.Range(0, 1000));
    }
}
