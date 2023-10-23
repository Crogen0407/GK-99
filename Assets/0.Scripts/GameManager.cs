using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public Volume Volume;
    
    public static GameManager Instance;
    public PlayerMovement playerMovement;
    public bool isUIMode;
    
    private void Awake()
    {
     
        
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
       
    }

    private void Start()
    {
        // Volume Settings
        Bloom bloom = new Bloom();
        Volume.profile.TryGet<Bloom>(out bloom);
    }


    private void OnMouseClick(InputValue value)
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
