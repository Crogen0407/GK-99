using Unity.Profiling;
using UnityEngine;

public interface IConsoleText
{
    public void ConsoleTextUpdate();
}

public enum Condition
{
    GOOD,
    BAD,
    DANGEROUS
}

public enum Attack
{
    JAP,
    HOOK,
    UPPERCUT
}

public class ConsoleTextController : MonoBehaviour
{
    [SerializeField] private Condition condition;
    [SerializeField] private Attack current_attack_mode;
    [SerializeField] private string machine_ver;
    [SerializeField] private string process_loader_ver;
    [SerializeField] private string user_code;
    [SerializeField] private string software_ver;
    [SerializeField] private float time;
    [SerializeField] private Vector3 position;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private bool dash;
    [SerializeField] private bool jump;
    [SerializeField] private float signal;

    public Condition CONDITION
    {
        get => condition;
        set
        {
            condition = value;
            Write();
        }
    }
    public Attack CURRENT_ATTACK_MODE
    {
        get => current_attack_mode;
        set
        {
            current_attack_mode = value;
            Write();
        }
    }
    public string MACHINE_VER
    {
        get => machine_ver;
        set
        {
            machine_ver = value;
            Write();
        }
    }
    public string PROCESS_LOADER_VER
    {
        get => process_loader_ver;
        set
        {
            process_loader_ver = value;
            Write();
        }
    }
    public string USER_CODE
    {
        get => user_code;
        set
        {
            user_code = value;
            Write();
        }
    }
    public string SOFTWARE_VER
    {
        get => software_ver;
        set
        {
            software_ver = value;
            Write();
        }
    }
    public float TIME
    {
        get => time;
        set
        {
            time = value;
            Write();
        }
    }
    public Vector3 POSITION
    {
        get => position;
        set
        {
            position = value;
            Write();
        }
    }
    public Vector3 ROTATION
    {
        get => rotation;
        set
        {
            rotation = value;
            Write();
        }
    }
    public Vector3 VELOCITY
    {
        get => velocity;
        set
        {
            velocity = value;
            Write();
        }
    }
    public bool DASH
    {
        get => dash;
        set
        {
            dash = value;
            Write();
        }
    }
    public bool JUMP
    {
        get => jump;
        set
        {
            jump = value;
            Write();
        }
    }
    public float SIGNAL
    {
        get => signal;
        set
        {
            signal = value;
            Write();
        }
    }
    
    private ProfilerRecorder _profilerRecorder;
    [SerializeField] [TextArea(minLines: 3, maxLines : 25)] private string _sampleOutputText;

    //Managements
    private UIManager _uiManager;
    
    private void Start()
    {
        Time.timeScale = 1;
        _uiManager = UIManager.Instance;
    }

    private void Write()
    {
        _uiManager.WriteConsoleText
            ($"<SIZE=40>CONDITION : {condition} </SIZE>" +
            "\n" +
            $" \nCURRENT ATTACK MODE : {current_attack_mode}" +
            "\n" +
            $"\nMACHINE VER : {machine_ver}\nPROCESS LOADER VER : {process_loader_ver}" +
            "\n" +
            $"\nUSER CODE : {user_code}" +
            $"\nSOFTWARE VER : {software_ver}" +
            "\nTIME" +
            $"\n[{((int)time/360).ToString("00")}:{((int)time/60).ToString("00")}:{((int)time%60 + time - (int)time).ToString("00.00")}]" +
            "\nPOSITION " +
            $"\n[{ConvertVector3ToString(position)}]" +
            "\nROTATION " +
            $"\n[{ConvertVector3ToString(RotateOptimization(rotation))}]" +
            "\nVELOCITY" +
            $"\n[{ConvertVector3ToString(velocity)}]" +
            $"\nDASH : {dash}" +
            $"\nJUMP : {jump}" +
            $"\nSIGNAL : {signal}");
    }

    private Vector3 RotateOptimization(Vector3 rotate)
    {
        return new Vector3(rotate.x % 360, rotate.y % 360, rotate.z % 360);
    }
    
    private string ConvertVector3ToString(Vector3 vec)
    {
        vec.x = Mathf.Floor(vec.x * 10) / 10;
        vec.y = Mathf.Floor(vec.y * 10) / 10;
        vec.z = Mathf.Floor(vec.z * 10) / 10;
        return $"{vec.x},{vec.y},{vec.z}";
    }
}

