using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameUIManager _uiManager;
    [SerializeField] private BricksManager _bricksManager;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform ballSpawnPoint;
    [SerializeField] private PlayerController player;
    [SerializeField] private List<BallController> _balls;
    [SerializeField] private CustomColliderBox _colliderDeath;
    [SerializeField] private ObjectPool _ballsPool;
    public bool isStarted;

    private bool _finishedGame = false;

    private int _lives;
    private int _destroyed;


    public bool FinishedGame => _finishedGame;
    public CustomColliderBox GetDeathCollider => _colliderDeath;
    public List<BallController> Balls => _balls;
    public PlayerController GetPlayer => player;
    public BallController GetBallController(int index) => _balls[index];

    private void Awake()
    {
        GameManager.Instance.LevelManager = this;
        _ballsPool.InitializePool();
    }

    private void Start()
    {
        _lives = 3;
        _uiManager.UpdateBricks(0, _bricksManager.TotalBricks);;
    }
    

    public void SpawnPlayerBall()
    {
        if(isStarted) return;
        isStarted = true;
    }

    public void CheckDefeat(BallController lostBall)
    {
        _balls.Remove(lostBall);
        
        if (_balls.Count == 0 && _lives >= 1)
        {
            LoseLife();

            if (_lives <= 0)
            {
                LoseGame();
            }
        }
    }

    public void LoseGame()
    {
        _finishedGame = true;
        _uiManager.ShowLose();
    }

    private void LoseLife()
    {
        _lives--;
        _uiManager.UpdateLives(_lives);
        player.SpawnNewBall();
    }

    public void DoPower(int amount = 2)
    {
        for (int i = 0; i < amount; i++)
        {
            var ball = _ballsPool.GetObject().GetComponent<BallController>();
            ball.InitializeBall(false, 15, ballSpawnPoint);
            _balls.Add(ball);
        }
    }

    public BallController GetPlayerBall()
    {
        var ball = _ballsPool.GetObject().GetComponent<BallController>();
        ball.InitializeBall(true, 10, ballSpawnPoint);
        _balls.Add(ball);
        return ball;
    }

    public void DestroyedBrick()
    {
        _destroyed++;
        _uiManager.UpdateBricks(_destroyed, _bricksManager.TotalBricks);
        CheckWin(_destroyed);

    }

    public void CheckWin(int bricksDestroyed)
    {
        if (bricksDestroyed >= _bricksManager.TotalBricks)
        {
            _finishedGame = true;
            _uiManager.ShowWin();
        }
    }
}
