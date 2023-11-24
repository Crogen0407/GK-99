using Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.ScreenCursorRaycasting;


public class UIEvent : MonoBehaviour
{
    public SO_SoundVolumeData SoundVolumeData;
    private CinemachineVirtualCamera _vir01;
    private CinemachineVirtualCamera _vir02;
    private RaycastHit _clickTarget;

    private void Awake()
    {
        SoundVolumeData.Init();
        _vir01 = GameObject.Find("Virtual Camera_01").GetComponent<CinemachineVirtualCamera>();
        _vir02 = GameObject.Find("Virtual Camera_02").GetComponent<CinemachineVirtualCamera>();
        
        _vir01.MoveToTopOfPrioritySubqueue();
    }

    private void OnMouseClick()
    {
        _clickTarget = ScreenCursorRaycasting.CursorDirection();
        SoundVolumeSettingsButtonClick(_clickTarget);
        SoundVolumeSetting(_clickTarget);
        BackToTitleScene(_clickTarget);
        GameQuit(_clickTarget);
        GameStart(_clickTarget);
        
    }

    /// <summary>
    /// Return SoundVolume Setting Value.  true : Volume Up | false : Volume Down
    /// </summary>
    /// <returns></returns>
    private void SoundVolumeSetting(RaycastHit hit)
    {
        int volumeCount = 0;
        if (hit.transform != null && hit.transform.tag == "SoundVolumeSettingUI")
        {
            if (hit.transform.GetComponent<BoxCollider>().center.x > 0)
            {
                volumeCount = 1;
            }
            else if(hit.transform.GetComponent<BoxCollider>().center.x < 0)
            {
                volumeCount = -1;
            }
            
            switch (hit.transform.parent.parent.name)
            {
                case "MSSoundVolume" :
                    SoundVolumeData.MSSoundVolume += volumeCount; 
                    break;
                case "SFSoundVolume" :
                    SoundVolumeData.SFSoundVolume += volumeCount; 
                    break;
                case "BGMSoundVolume" : 
                    SoundVolumeData.BGMSoundVolume += volumeCount; 
                    break;
            }
        }
    }
    
    private void SoundVolumeSettingsButtonClick(RaycastHit hit)
    {
        if (hit.transform != null && hit.transform.name == "SettingsButton")
        {
            _vir02.MoveToTopOfPrioritySubqueue();
        }
    }

    private void BackToTitleScene(RaycastHit hit)
    {
        if (hit.transform != null && hit.transform.name == "BackToTitleScene")
        {
            _vir01.MoveToTopOfPrioritySubqueue();
        }
    }

    private void GameStart(RaycastHit hit)
    {
        if (hit.transform != null && hit.transform.name == "Start")
        {
            SceneLoader.LoadScene("GameScene_0");
        }
    }
    
    private void GameQuit(RaycastHit hit)
    {
        if (hit.transform != null && hit.transform.name == "Quit")
        {
            Application.Quit();
        }
    }
}
