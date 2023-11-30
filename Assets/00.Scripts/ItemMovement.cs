using System;
using UnityEngine;
using UnityEngine.Tweening;

public class ItemMovement : MonoBehaviour
{
    public int itemCount = 0;
    private Transform _playerTransform;
    [SerializeField] private float _speed;
    
    //Managements
    private PoolManager _poolManager;
    private GameManager _gameManager;
    private ScoreManager _scoreManager;
    
    private void Start()
    {
        _poolManager = PoolManager.Instance;
        _gameManager = GameManager.Instance;
        _scoreManager = ScoreManager.Instance;
        _playerTransform = _gameManager.playerMovement.transform;
    }

    private void FixedUpdate()
    {
        if (_playerTransform != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _playerTransform.position, _speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, _playerTransform.position) < 0.2f)
            {
                _scoreManager.Score += itemCount;
                _poolManager.Push("LightningBall", gameObject);
            }
        }
    }
}
