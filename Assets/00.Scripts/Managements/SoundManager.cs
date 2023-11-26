using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private SO_SoundVolumeData _soundVolumeData;
    
    void Awake()
    {
        
    }

    private void Start()
    {
        LoadSoundVolume();
    }

    private void LoadSoundVolume()
    {
        _audioMixer.SetFloat("Master", Mathf.Log10(_soundVolumeData.MSSoundVolume/10f) * 20);
        _audioMixer.SetFloat("BGM", Mathf.Log10(_soundVolumeData.BGMSoundVolume/10f) * 20);
        _audioMixer.SetFloat("SFX", Mathf.Log10(_soundVolumeData.SFSoundVolume/10f) * 20);
    }

    void Update()
    {
        
    }
}
