using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenEffectController : MonoBehaviour
{
    public Material material;
    public bool BreathingOff;

    public void Awake()
    {
        material.SetInt("_Breathing", Convert.ToInt32(BreathingOff));
        material.SetInt("_Noising", Convert.ToInt32(false));
    }

    public void SetBool(string name, bool value)
    {
        name = name[0] == '_' ? name[0] + name.Substring(1) : "_"+name;
        material.SetInt(name, Convert.ToInt32(value));
    }
}
