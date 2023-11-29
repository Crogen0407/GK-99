using System;
using UnityEngine;

public class ScoreManager : MonoSingleton<ScoreManager>
{
    private int _score;
    private UIManager _uiManager;

    private void Start()
    {
        _uiManager = UIManager.Instance;
    }

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            _uiManager.WriteScoreText($"Score : {_score.ToString("0000")}");
        }
    }
}
