using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private SO_SoundVolumeData _soundVolumeData;
    [SerializeField] private TextAsset _data;
    void Awake()
    {
        
    }

    private void OnEnable()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Start()
    {
        LoadSoundVolume();
    }
    
    public void LoadSoundVolume()
    {
        _audioMixer.SetFloat("Master", Mathf.Log10((_soundVolumeData.MSSoundVolume == 0 ? 0.0001f : _soundVolumeData.MSSoundVolume)/10f) * 20);
        _audioMixer.SetFloat("BGM", Mathf.Log10((_soundVolumeData.BGMSoundVolume == 0 ? 0.0001f : _soundVolumeData.BGMSoundVolume)/10f) * 20);
        _audioMixer.SetFloat("SFX", Mathf.Log10((_soundVolumeData.SFSoundVolume == 0 ? 0.0001f : _soundVolumeData.SFSoundVolume)/10f) * 20);
    }

    void Update()
    {
        
    }
}
