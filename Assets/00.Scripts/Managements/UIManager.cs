using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    private Transform _canvasTransform;
    private TextMeshProUGUI _consoleText;

    private Transform _commentTransform;
    private TextMeshProUGUI _commentText;

    private void Awake()
    {
        _canvasTransform = FindObjectOfType<Canvas>().transform;
        _consoleText = _canvasTransform.Find("ConsoleText").GetComponent<TextMeshProUGUI>();
        _commentTransform = _canvasTransform.Find("Comment");
        _commentText = _commentTransform.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    public void WriteConsoleText(string text)
    {
        _consoleText.text = text;
    }

    public void WriteCommentText(string text)
    {
        
    }
}
