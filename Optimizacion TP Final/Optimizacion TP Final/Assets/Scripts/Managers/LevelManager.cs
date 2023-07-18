using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private CanvasLevel canvasLevel;
    [SerializeField] private BricksManager _bricksManager;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform ballSpawnPoint;
    [SerializeField] private PlayerController player;
    [SerializeField] private List<BallController> _balls;
    [SerializeField] private CustomColliderBox _colliderDeath;
    [SerializeField] private ObjectPool _ballsPool;
    public bool isStarted;

    private int _lives;
    private int _destroyed;

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
        canvasLevel.UpdateBricks(0, _bricksManager.TotalBricks);;
    }
    

    public void SpawnPlayerBall()
    {
        if(isStarted) return;
        isStarted = true;
    }
    /*
    public void SpawnBall()
    {
        GameObject newBall = _ballsPool.GetObject();
        newBall.transform.position = ballSpawnPoint.position;
        _balls.Add(newBall.GetComponent<BallController>());
    }*/
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

    /*
    public void RestartGame()
    {
        isStarted = false;
        _lives--;
        player.StopMovement();
        if (_lives > 0)
        {
            //SpawnBall();
        }
        else
        {
            LoseGame();
        }
    }*/

    public void LoseGame()
    {
        canvasLevel.Defeat();
        //SceneManager.LoadScene("Menu");
    }

    private void LoseLife()
    {
        //_uiManager.UpdateLives(_lives);
        canvasLevel.LostLife();
        _lives--;
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
        canvasLevel.UpdateBricks(_destroyed, _bricksManager.TotalBricks);

        CheckWin(_destroyed);

    }

    public void CheckWin(int bricksDestroyed)
    {
        if (bricksDestroyed == _bricksManager.TotalBricks)
        {
            canvasLevel.Win();
        }
    }
}
