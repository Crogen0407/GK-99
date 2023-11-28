using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;

public class GameManager : MonoSingleton<GameManager>, IConsoleText
{
    public Volume Volume;
    public float timer;
    
    //Player
    public HealthSystem playerHealthSystem;
    public PlayerMovement playerMovement;
    public PlayerAttack PlayerAttack;
    
    //Managements
    
    //Controllers
    public ConsoleTextController consoleTextController;
    public EffectController effectController;
    public ScreenEffectController screenEffectController;
    
    public bool isUIMode;
    private bool gameOver;

    public bool GameOver
    {
        get => gameOver;
        set
        {
            gameOver = value;
            if (gameOver == true)
            {
                if(playerHealthSystem.Hp > 0)
                {
                    //Succeed GameClear
                }
                else
                {
                    //fail GameClear
                    PlayerAttack.ChangeAnimationGameOverAction?.Invoke();
                }
                
            }
        }
    }
    
    private void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        PlayerAttack = playerMovement.GetComponent<PlayerAttack>();
        playerHealthSystem = playerMovement.transform.GetComponent<HealthSystem>();
        
        consoleTextController = FindObjectOfType<ConsoleTextController>();
        effectController = FindObjectOfType<EffectController>();
        screenEffectController = FindObjectOfType<ScreenEffectController>();
    }

    private void Start()
    {
        // Volume Settings
        //Bloom bloom = new Bloom();
        //Volume.profile.TryGet<Bloom>(out bloom);
        
        Application.targetFrameRate = 60;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    //MouseSetting
    private void OnMouseClick(InputValue value)
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PoolManager.Instance.Pop("Enemy", Vector3.zero, Quaternion.identity);
        }
        ConsoleTextUpdate();
        timer = Time.time;
    }

    public void ConsoleTextUpdate()
    {
        consoleTextController.SIGNAL = Mathf.Floor(1.0f / Time.deltaTime * 10) / 10;
        consoleTextController.TIME = Mathf.Floor(timer * 100) / 100;
    }

    public void TimeSlow(float timeScale, float duration)
    {
        StopCoroutine(TimeSlowCoroutine(timeScale, duration));
        StartCoroutine(TimeSlowCoroutine(timeScale, duration));
    }

    private IEnumerator TimeSlowCoroutine(float timeScale, float duration)
    {
        Time.timeScale = timeScale;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1;
    }
}