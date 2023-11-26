using System.IO;
using UnityEngine;

public class GameSetting
{
    public float soundLevel_Master;
    public float soundLevel_BGM;
    public float soundLevel_SFX;

    public GameSetting()
    {
        soundLevel_Master = 0;
        soundLevel_BGM = 0;
        soundLevel_SFX = 0;
    }

    public GameSetting(float master, float BGM, float SFX)
    {
        soundLevel_Master = master;
        soundLevel_BGM = BGM;
        soundLevel_SFX = SFX;
    }
} 


public class DataManager : MonoBehaviour
{
    private static string GameSettingPath;
    
    public static GameSetting GetGameSetting()
    {
        if (GameSettingPath == null)
        {
            GameSettingPath = Path.Combine(Application.dataPath, "99_DataBase/SaveData.json");
        }
        GameSetting gameSetting = new GameSetting();
        if (!File.Exists(GameSettingPath))
        {
            SaveGameSetting(gameSetting);
        }
        else
        {
            string loadJson = File.ReadAllText(GameSettingPath);
            gameSetting = JsonUtility.FromJson<GameSetting>(loadJson);

            if (gameSetting != null)
            {
                
            }
        }

        return gameSetting;
    }
    
    public static void SaveGameSetting(GameSetting gameSetting)
    {
        string json = JsonUtility.ToJson(gameSetting, true);
        if (GameSettingPath == null)
        {
            GameSettingPath = Path.Combine(Application.dataPath, "99_DataBase/SaveData.json");
        }

        File.WriteAllText(GameSettingPath, json);
        Debug.Log("<color='blue'> ▶ GameSetting Json Saved!</color>");
    }
}
