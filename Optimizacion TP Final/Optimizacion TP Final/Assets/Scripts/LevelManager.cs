using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform ballSpawnPoint;
    [SerializeField] private PlayerController player;
    [SerializeField] private List<BallController> _balls;
    [SerializeField] private CustomColliderBox _colliderDeath;
    public bool isStarted;
    private float _lives;

    public CustomColliderBox GetDeathCollider => _colliderDeath;
    public List<BallController> Balls => _balls;
    public PlayerController GetPlayer => player;
    public BallController GetBallController(int index) => _balls[index];

    private void Start()
    {
        _lives = 3;
        SpawnBall();
    }
    

    public void StartGame()
    {
        if(isStarted) return;
        isStarted = true;
        _balls[0].StartMovement();
    }
    
    public void SpawnBall()
    {
        GameObject newBall = Instantiate(ballPrefab, ballSpawnPoint.position, quaternion.identity);
        _balls.Add(newBall.GetComponent<BallController>());
    }
    public void CheckDefeat(BallController lostBall)
    {
        _balls.Remove(lostBall);
        
        if (_balls.Count == 0)
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        isStarted = false;
        _lives--;
        player.StopMovement();
        if (_lives > 0)
        {
            SpawnBall();
        }
        else
        {
            LoseGame();
        }
    }

    public void LoseGame()
    {
        // Pantalla de derrota
    }

    public void DoPower()
    {
        CustomLogger.Log("[TODO]: Spawn Extra Balls.");
    }
}
