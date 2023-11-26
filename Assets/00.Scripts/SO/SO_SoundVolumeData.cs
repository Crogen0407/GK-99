using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SoundVolumeData", fileName = "SoundVolumeData")]
public class SO_SoundVolumeData : ScriptableObject
{
    public Transform UI_Settings { get; private set; }
    
    private TextMeshPro _msSoundVolumeText;
    private TextMeshPro _sfSoundVolumeText;
    private TextMeshPro _bgmSoundVolumeText;
    
    [SerializeField] private int _msSoundVolume = 0;
    [SerializeField] private int _sfSoundVolume = 0;
    [SerializeField] private int _bgmSoundVolume = 0;

    public void Init()
    {
        UI_Settings = GameObject.Find("UI_Settings").transform;
        
        _msSoundVolumeText = UI_Settings.Find("MSSoundVolume/Volume").GetComponent<TextMeshPro>();
        _sfSoundVolumeText = UI_Settings.Find("SFSoundVolume/Volume").GetComponent<TextMeshPro>();
        _bgmSoundVolumeText = UI_Settings.Find("BGMSoundVolume/Volume").GetComponent<TextMeshPro>();

        _msSoundVolumeText.text = $"< {MSSoundVolume.ToString()} >";
        _sfSoundVolumeText.text = $"< {SFSoundVolume.ToString()} >";
        _bgmSoundVolumeText.text = $"< {BGMSoundVolume.ToString()} >";
    }

    //0~10
    public int MSSoundVolume
    {
        get => _msSoundVolume;
        set
        {
            _msSoundVolume = value;
            _msSoundVolume = Mathf.Clamp(_msSoundVolume, 0, 10);     
            _msSoundVolumeText.text = $"< {_msSoundVolume} >";
        }
    }
    
    //0~10
    public int SFSoundVolume
    {
        get => _sfSoundVolume;
        set
        {
            _sfSoundVolume = value;
            _sfSoundVolume = Mathf.Clamp(_sfSoundVolume, 0, 10);     
            _sfSoundVolumeText.text = $"< {_sfSoundVolume} >";
        }
    }

    //0~10
    public int BGMSoundVolume
    {
        get => _bgmSoundVolume;
        set
        {
            _bgmSoundVolume = value;
            _bgmSoundVolume = Mathf.Clamp(_bgmSoundVolume, 0, 10);     
            _bgmSoundVolumeText.text = $"< {_bgmSoundVolume} >";
        }
    }
    
    
}