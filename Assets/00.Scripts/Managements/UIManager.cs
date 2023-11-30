using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    private int _selectCount = 0;
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
        _commentTextY = _commentText.transform.Find("Y").GetComponent<TextMeshProUGUI>();
        _commentTextN = _commentText.transform.Find("N").GetComponent<TextMeshProUGUI>();
        
        
        _consoleText = _canvasTransform.Find("ConsoleText").GetComponent<TextMeshProUGUI>();
        _commentText = _commentPanel.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        _scoreText = _canvasTransform.Find("ScoreText").GetComponent<TextMeshProUGUI>();

        _gameOverPanel = _canvasTransform.Find("GameOverPanel").gameObject;
    }


    public int SelectCount
    {
        get => _selectCount;
        set
        {
            if (2 < value)
            {
                _selectCount = 0;
            }
            else if(value < 0)
            {
                _selectCount = 2;
            }
            else
            {
                _selectCount = value;
            }
            _selectCount = Mathf.Clamp(_selectCount, 0, 2);
            if (_selectCount == 0)
            {
                _commentText.transform.Find("Y").GetComponent<TextMeshProUGUI>().text = "Y";
                _commentText.transform.Find("N").GetComponent<TextMeshProUGUI>().text = "N<";
            }
            else if(_selectCount == 1)
            {
                _commentText.transform.Find("Y").GetComponent<TextMeshProUGUI>().text = "Y<";
                _commentText.transform.Find("N").GetComponent<TextMeshProUGUI>().text = "N";
            }
            else
            {
                _commentText.transform.Find("Y").GetComponent<TextMeshProUGUI>().text = "Y";
                _commentText.transform.Find("N").GetComponent<TextMeshProUGUI>().text = "N";
                _commentText.transform.Find("R").GetComponent<TextMeshProUGUI>().text = "R<";
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
                    _commentPanel.SetActive(false);
                    Time.timeScale = 1;
                }
                else if(SelectCount == 2)
                {
                    Time.timeScale = 1;
                    SceneLoader.LoadScene("GameScene_0");
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
        if (_commentPanel.activeSelf == false)
        {
            _commentPanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            _commentPanel.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void OnGameOver()
    {
        _gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
