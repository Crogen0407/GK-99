using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraShakeController : MonoBehaviour
{
    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;

    private void Start()
    {
        _cinemachineBasicMultiChannelPerlin = CinemachinePOVExtension.Instance.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }


    public void Shake(float amplitudeGain, float frequencyGain)
    {
        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = amplitudeGain;
        _cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frequencyGain;
    }
    
    public void EndShake()
    {
        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
        _cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0;
    }

    private void Shake(float amplitudeGain, float frequencyGain, float duration)
    {
       StopAllCoroutines();
        StartCoroutine(CameraShake(amplitudeGain, frequencyGain, duration));
    }

    private IEnumerator CameraShake(float amplitudeGain, float frequencyGain, float duration)
    {
        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain += amplitudeGain;
        _cinemachineBasicMultiChannelPerlin.m_FrequencyGain += frequencyGain;
        yield return new WaitForSeconds(duration);
        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain -= amplitudeGain;
        _cinemachineBasicMultiChannelPerlin.m_FrequencyGain -= frequencyGain;
    }

}
