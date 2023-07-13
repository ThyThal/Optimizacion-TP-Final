using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviourGameplay
{
    [SerializeField] private CustomColliderBox _collider;
    [SerializeField] private CustomPhysics _physics;
    private float _xMovement;
    private Vector2 _direction;

    public override void ManagedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Space)) GameManager.Instance.LevelManager.StartGame();
        
        if(!GameManager.Instance.LevelManager.isStarted) return;
        // Check for collision with all balls.
        for (var index = 0; index < GameManager.Instance.LevelManager.Balls.Count; index++)
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
        
        _direction.x = Input.GetAxis("Horizontal");
        _physics.SetDirection(_direction);

        _physics.Update();
    }

    public void StopMovement()
    {
        _physics.SetDirection(Vector2.zero);
    }
}
