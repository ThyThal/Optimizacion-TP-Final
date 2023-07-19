using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviourGameplay
{
    [SerializeField] private GameUIManager _uiManager;
    [SerializeField] private BricksManager _bricksManager;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform ballSpawnPoint;
    [SerializeField] private PlayerController player;
    [SerializeField] private List<BallController> _balls;
    [SerializeField] private CustomColliderBox _colliderDeath;
    [SerializeField] private ObjectPool _ballsPool;
    [SerializeField] private float gameTime = 0f;

    public bool isStarted;

    private bool _finishedGame = false;

    private int _lives;
    private int _destroyed;


    public bool FinishedGame => _finishedGame;
    public CustomColliderBox GetDeathCollider => _colliderDeath;
    public List<BallController> Balls => _balls;
    public PlayerController GetPlayer => player;
    public BallController GetBallController(int index) => _balls[index];

    public override void Awake()
    {
        base.Awake();
        GameManager.Instance.LevelManager = this;
        _ballsPool.InitializePool();
    }

    private void Start()
    {
        _lives = 3;
        _uiManager.UpdateBricks(0, _bricksManager.TotalBricks);;
    }

    public override void ManagedUpdate()
    {
        if (FinishedGame) return;
        gameTime += CustomUpdateManager.Instance.CustomUpdateGameplay.GetDeltaTime;
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

    public string GetFormattedTime()
    {
        int hours = Mathf.FloorToInt(gameTime / 3600);
        int minutes = Mathf.FloorToInt((gameTime % 3600) / 60);
        int seconds = Mathf.FloorToInt(gameTime % 60);

        string formattedTime = $"{hours}h {minutes}m {seconds}s";
        return formattedTime;
    }
}
