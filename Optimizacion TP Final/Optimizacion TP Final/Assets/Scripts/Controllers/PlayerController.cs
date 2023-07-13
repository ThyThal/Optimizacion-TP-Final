using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviourGameplay
{
    [SerializeField] private CustomColliderBox _collider;

    public override void ManagedUpdate()
    {
        // Check for colision with all balls.
        for (int index = 0; index < GameManager.Instance.LevelManager.Balls.Count; index++)
        {
            BallController ballController = GameManager.Instance.LevelManager.GetBallController(index);

            /*
             * Hay que checkear si toca los costados y si toca perder la pelota.
             */

            // Check collision with ball.
            if (_collider.CheckCollision(ballController.GetCollider))
            {
                ballController.Reflect();
            }
        }
    }
}
