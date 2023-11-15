using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;

public class GameManager : MonoSingleton<GameManager>
{
    public Volume Volume;

    public HealthSystem playerHealthSystem;
    public PlayerMovement playerMovement;
    public PlayerAnimator playerAnimator;
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
                    playerAnimator.ChangeAnimationGameOver();
                }
                
            }
        }
    }
    
    private void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerAnimator = playerMovement.transform.GetComponent<PlayerAnimator>();
        playerHealthSystem = playerMovement.transform.GetComponent<HealthSystem>();
    }

    private void Start()
    {
        // Volume Settings
        //Bloom bloom = new Bloom();
        //Volume.profile.TryGet<Bloom>(out bloom);
    }


    //MouseSetting
    private void OnMouseClick(InputValue value)
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}