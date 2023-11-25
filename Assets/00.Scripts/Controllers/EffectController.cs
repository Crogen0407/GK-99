using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EffectController : MonoBehaviour
{
    private PoolManager _poolManager;
    [SerializeField] private Transform _platform;
    [SerializeField] private List<string> groundCheckableEffectNames;    
    private void Start()
    {
        _poolManager = PoolManager.Instance;
    }

    public GameObject CreateEffect(string effectName, Vector3 vec, Quaternion rot, float duration)
    {
        GameObject effectObj = _poolManager.Pop(effectName, vec, rot);;
        foreach (var name in groundCheckableEffectNames)
        {
            if (name == effectName)
            {
                effectObj.transform.Find("Collision").GetComponent<ParticleSystem>().collision.SetPlane(0, _platform);
            }
        }
        StartCoroutine(EffectDie(effectName, effectObj, duration));
        
        return effectObj;
    }

    private IEnumerator EffectDie(string effectName, GameObject dieObject, float duration)
    {
        yield return new WaitForSeconds(duration);
        _poolManager.Push(effectName, dieObject);
    }
}
