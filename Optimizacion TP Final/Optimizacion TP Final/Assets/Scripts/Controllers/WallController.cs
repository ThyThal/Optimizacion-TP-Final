using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviourGameplay
{
    [SerializeField] private CustomColliderBox _collider;

    public override void ManagedUpdate()
    {
        // Check for colision with all balls.
        for (int index = 0; index < GameManager.Instance.LevelManager.Balls.Count - 1; index++)
        {
            // Check collision with ball.
            _collider.CheckCollision(GameManager.Instance.LevelManager.GetCollider(index));
        }
    }
}