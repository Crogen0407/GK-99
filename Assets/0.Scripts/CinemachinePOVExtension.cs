using System.Collections;
using UnityEngine;
using Cinemachine;
public class CinemachinePOVExtension : CinemachineExtension
{
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;

    private Vector3 startRotate;
    public static CinemachinePOVExtension Instance;
    
    protected void Awake()
    {
        
        _cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        _cinemachineBasicMultiChannelPerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if (startRotate == null) startRotate = GameManager.Instance.playerMovement.transform.eulerAngles;
                transform.eulerAngles = transform.eulerAngles + new Vector3(0,Input.mousePosition.x, 0);
            }
        }
    }

    public void CameraShake(float amplitude, float duration)
    {
        float currentTime = 0;
        float percentTime = currentTime / duration;
        IEnumerator CameraShakeCoroutine()
        {
            while (percentTime <= 1)
            {
                currentTime += Time.deltaTime;
                percentTime = currentTime / duration;
            
                _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = amplitude;
                yield return null;
            }
        }
    }
}
