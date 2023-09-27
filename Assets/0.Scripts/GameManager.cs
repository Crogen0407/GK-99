using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isUIMode;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    private void OnMouseClick(InputValue value)
    {
        Debug.Log("dd");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
}
