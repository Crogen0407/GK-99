using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    private Transform _canvasTransform;
    private TextMeshProUGUI _consoleText;
    private TextMeshProUGUI _scoreText;

    private GameObject _gameClearPanel;
    private GameObject _commentPanel;
    private TextMeshProUGUI _commentText;
    private GameObject _gameOverPanel;
    
    private void Awake()
    {
        _canvasTransform = FindObjectOfType<Canvas>().transform;
        _gameClearPanel = _canvasTransform.Find("GameClearPanel").gameObject;
        _commentPanel = _canvasTransform.Find("Comment").gameObject;
        
        _consoleText = _canvasTransform.Find("ConsoleText").GetComponent<TextMeshProUGUI>();
        _commentText = _commentPanel.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        _scoreText = _canvasTransform.Find("ScoreText").GetComponent<TextMeshProUGUI>();

        _gameOverPanel = _canvasTransform.Find("GameOverPanel").gameObject;
    }

    private int _selectCount = 0;

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
                _commentText.transform.Find("R").GetComponent<TextMeshProUGUI>().text = "R";
            }
            else if(_selectCount == 1)
            {
                _commentText.transform.Find("Y").GetComponent<TextMeshProUGUI>().text = "Y<";
                _commentText.transform.Find("N").GetComponent<TextMeshProUGUI>().text = "N";
                _commentText.transform.Find("R").GetComponent<TextMeshProUGUI>().text = "R";
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

    public void OnGameClear()
    {
        _gameClearPanel.SetActive(true);
        _gameClearPanel.transform.Find("Text").GetComponent<TextMeshProUGUI>().text =
            $"<size=100>데모 버전은 여기까지 입니다</size>\n\n \n플레이해주셔서 감사합니다.\n\n본편은 2월 2일 steam에서 만나요.\n\nSCORE : {ScoreManager.Instance.Score.ToString("0000")}";
        Time.timeScale = 0;
    }
    
    public void OnGameOver()
    {
        _gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
