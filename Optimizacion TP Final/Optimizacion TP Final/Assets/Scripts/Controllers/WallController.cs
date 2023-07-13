using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CustomColliderBox))]
public class WallController : MonoBehaviourGameplay
{
    [SerializeField] private CustomColliderBox _collider;
    [SerializeField] private bool _death = false;

    public override void ManagedUpdate()
    {
        // Check for colision with all balls.
        for (int index = 0; index < GameManager.Instance.LevelManager.Balls.Count; index++)
        {
            BallController ballController = GameManager.Instance.LevelManager.GetBallController(index);

            // Check collision with ball.
            if (_collider.CheckCollision(ballController.GetCollider))
            {
                if (_death)
                {
                    ballController.DestroyBall();
                }

                else
                {
                    ballController.Reflect();
                }
            }
        }
    }
}