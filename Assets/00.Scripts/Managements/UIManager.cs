using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    private int _selectCount = 0;
    [SerializeField] private int selectCountMax;
    private Transform _canvasTransform;
    private TextMeshProUGUI _consoleText;
    private TextMeshProUGUI _scoreText;

    private GameObject _gameClearPanel;
    private GameObject _commentPanel;
    private TextMeshProUGUI _commentTextY;
    private TextMeshProUGUI _commentTextN;
    
    private TextMeshProUGUI _commentText;
    private GameObject _gameOverPanel;
    
    private void Awake()
    {
        _canvasTransform = FindObjectOfType<Canvas>().transform;
        _gameClearPanel = _canvasTransform.Find("GameClearPanel").gameObject;
        
        //CommentPanel
        _commentPanel = _canvasTransform.Find("Comment").gameObject;
        _commentText = _commentPanel.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        _commentTextY = _commentText.transform.Find("Y").GetComponent<TextMeshProUGUI>();
        _commentTextN = _commentText.transform.Find("N").GetComponent<TextMeshProUGUI>();
        
        
        _consoleText = _canvasTransform.Find("ConsoleText").GetComponent<TextMeshProUGUI>();
        _scoreText = _canvasTransform.Find("ScoreText").GetComponent<TextMeshProUGUI>();

        _gameOverPanel = _canvasTransform.Find("GameOverPanel").gameObject;
    }

    public int SelectCount
    {
        get => _selectCount;
        set
        {
            if (selectCountMax < value)
            {
                _selectCount = 0;
            }
            else if(value < 0)
            {
                _selectCount = selectCountMax;
            }
            else
            {
                _selectCount = value;
            }
            _selectCount = Mathf.Clamp(_selectCount, 0, selectCountMax);
            if (_selectCount == 0)
            {
                _commentTextY.text = "Y";
                _commentTextN.text = "N<";
            }
            else if(_selectCount == 1)
            {
                _commentTextY.text = "Y<";
                _commentTextN.text = "N";
            }
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnComment();
        }

        if (_commentPanel.activeSelf == true)
        {
            Time.timeScale = 0;
            if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.A))
            {
                SelectCount++;
            }
            else if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.D))
            {
                SelectCount--;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (SelectCount == 1)
                {
                    Time.timeScale = 1;
                    SceneLoader.LoadScene("StartScene");
                }
                else if(SelectCount == 0)
                {
                    OnComment();
                }
            }
        }
    }

    public void WriteConsoleText(string text)
    {
        _consoleText.text = text;
    }

    public void WriteScoreText(string text)
    {
        _scoreText.text = text;
    }

    private void OnComment()
    {
        bool active = _commentPanel.activeSelf;
        _commentPanel.SetActive(active == true ? false : true);
        Time.timeScale = Convert.ToInt32(active);
    }

    public void OnGameOver()
    {
        _gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
