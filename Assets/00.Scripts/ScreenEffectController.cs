using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenEffectController : MonoSingleton<ScreenEffectController>
{
    public Material material;

    public void SetBool(string name, bool value)
    {
        name = name[0] == '_' ? name[0] + name.Substring(1) : "_"+name;
        material.SetInt(name, Convert.ToInt32(value));
    }

    
}
