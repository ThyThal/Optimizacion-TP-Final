using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviourGameplay
{
    public Vector2Int Index;

    public event Action<BrickController> BrickDestroyedEvent;
    private List<BrickController> neighborBricks = new List<BrickController>();
    [SerializeField] private bool _breakable = false;

    [SerializeField] private CustomColliderBox _collider;


    public void SetBreakable() { _breakable = true; }


    public override void ManagedUpdate()
    {
        // Check collisions if is breakable.
        if (_breakable)
        {
            CheckBallsCollisions();
        }
    }

    // Check for collision with balls.
    private void CheckBallsCollisions()
    {
        // Check for colision with all balls.
        for (int index = 0; index < GameManager.Instance.LevelManager.Balls.Count; index++)
        {
            BallController ballController = GameManager.Instance.LevelManager.GetBallController(index);

            // Check collision with ball.
            if (_collider.CheckCollision(ballController.GetCollider))
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    // Method to add a neighbor brick.
    public void AddNeighborBrick(BrickController neighbor)
    {
        if (!neighborBricks.Contains(neighbor))
        {
            neighborBricks.Add(neighbor);
        }
    }

    // Method to remove a neighbor brick.
    public void RemoveNeighborBrick(BrickController neighbor)
    {
        neighborBricks.Remove(neighbor);
    }

    // Method to destroy the brick
    public void DestroyBrick()
    {
        // Perform any actions needed when the brick is destroyed
        _breakable = false;

        // Notify the neighboring bricks
        foreach (BrickController neighbor in neighborBricks)
        {
            neighbor.OnNeighborBrickDestroyed(this);
        }

        // Invoke the BrickDestroyedEvent
        BrickDestroyedEvent?.Invoke(this);
    }

    // Method called when a neighbor brick is destroyed
    private void OnNeighborBrickDestroyed(BrickController neighbor)
    {
        _breakable = true;
    }

    private void OnDisable()
    {
        DestroyBrick();
    }
}
