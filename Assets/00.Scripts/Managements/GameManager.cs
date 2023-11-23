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

    //Player
    public HealthSystem playerHealthSystem;
    public PlayerMovement playerMovement;
    public PlayerAttack PlayerAttack;
    
    //Controllers
    public ConsoleTextController _consoleTextController;
    
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
    }

    private void Start()
    {
        // Volume Settings
        //Bloom bloom = new Bloom();
        //Volume.profile.TryGet<Bloom>(out bloom);
        _consoleTextController = ConsoleTextController.Instance;
        
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
    }

    private void FixedUpdate()
    {
        consoleTextUpdate();
    }

    public void consoleTextUpdate()
    {
        _consoleTextController.SIGNAL = 1.0f / Time.deltaTime;
    }
}