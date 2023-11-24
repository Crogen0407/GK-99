using System;
using System.Collections;
using UnityEngine;
using Cinemachine;
public class CinemachinePOVExtension : CinemachineExtension
{
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;

    private Vector3 startRotate;
    public static CinemachinePOVExtension Instance;
    
    public float sensX;
    public float sensY;
    
    private float xRotation;
    private float yRotation;
    
    [SerializeField] private float _minRotateX = -45;
    [SerializeField] private float _maxRotateX = 60;

    
    protected void Awake()
    {
        #region  singleton
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        #endregion 
        _cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        _cinemachineBasicMultiChannelPerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cinemachineBasicMultiChannelPerlin.enabled = false;
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if (startRotate == null) startRotate = GameManager.Instance.playerMovement.transform.eulerAngles;
                transform.eulerAngles += new Vector3(0,Input.mousePosition.x, 0);
                
                float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
                float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
                yRotation += mouseX;
                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, _minRotateX, _maxRotateX);
                transform.eulerAngles = new Vector3(xRotation, yRotation, 0);
            }
        }
    }

    public void CameraExpand(float strength)
    {
        _cinemachineVirtualCamera.m_Lens.FieldOfView = strength;
    }

    public void CameraForcedRotate(float xRotate = 0, float yRotate = 0)
    {
        
        Debug.Log($"{xRotate}. {yRotate}");
        transform.eulerAngles = new Vector3(xRotation, yRotation, 0);
    }
    
    public void CameraShake(float amplitude, float frequency, float duration)
    {
        StopCoroutine(CameraShakeCoroutine());
        StartCoroutine(CameraShakeCoroutine());
        IEnumerator CameraShakeCoroutine()
        {
            float currentTime = 0;
            float percentTime = currentTime / duration;
            _cinemachineBasicMultiChannelPerlin.enabled = true;
            while (percentTime <= 1)
            {
                currentTime += Time.deltaTime;
                percentTime = currentTime / duration;
            
                _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = amplitude * percentTime;
                _cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frequency * percentTime;
                yield return null;
            }
            _cinemachineBasicMultiChannelPerlin.enabled = false;
        }
    }
    public void StartCameraShake(float amplitude, float frequency)
    {
        _cinemachineBasicMultiChannelPerlin.enabled = true;
        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = amplitude;
        _cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frequency;
    }

    public void StopCameraShake()
    {
        _cinemachineBasicMultiChannelPerlin.enabled = false;
    }
}
