using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Profiling;
using UnityEngine;

public enum Condition
{
    GOOD,
    BAD,
    DANGEROUS
}

public enum Attack
{
    Jap,
    Hook,
    Uppercut
}

public class ConsoleTextController : MonoSingleton<ConsoleTextController>
{
    [SerializeField] private Condition condition;
    [SerializeField] private Attack current_attack_mode;
    [SerializeField] private string machine_ver;
    [SerializeField] private string process_loader_ver;
    [SerializeField] private string user_code;
    [SerializeField] private string software_ver;
    [SerializeField] private float time;
    [SerializeField] private Vector3 position;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private bool dash;
    [SerializeField] private bool jump;
    [SerializeField] private float signal;
    [SerializeField] private Vector3 operator_by_body;
    
    private TextMeshProUGUI _consoleText;
    private ProfilerRecorder _profilerRecorder;
    [SerializeField] [TextArea(minLines: 3, maxLines : 25)] private string _sampleOutputText;
    
    public void Write(Condition condition)
    {
        this.condition = condition;
        
        
        _consoleText.text =
            $"<SIZE=40>CONDITION : {condition.ToString()} </SIZE>\n \nCURRENT ATTACK MODE : JAP\n\nMACHINE VER : V93.2\nPROCESS LOADER VER : V23.6\n\nUSER CODE : \nSOFTWARE VER : V12.9\n\n00:00:00.00\nPOSITION \n[0,0,0]\nROTATION \n[0,0,0]\nVELOCITY\n[0,0,0]\nDASH : \nJUMP : \nSIGNAL : \nOPERATOR BY BODY :\n\n\n\n\n";
    }
}
