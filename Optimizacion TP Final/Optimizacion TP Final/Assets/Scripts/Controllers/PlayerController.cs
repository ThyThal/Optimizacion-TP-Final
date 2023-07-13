using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviourGameplay
{
    [SerializeField] private CustomColliderBox _collider;
    [SerializeField] private CustomPhysics _physics;
    private float _xMovement;
    private Vector2 _direction;

    [SerializeField] private WallController _wallLeft;
    [SerializeField] private WallController _wallRight;
    [SerializeField] private float _speed = 10f;

    [SerializeField] private bool collision = false;

    public override void ManagedUpdate()
    {
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

        collision = false;
        if (_wallRight != null)
        {
            if(_collider.CheckCollision(_wallRight.GetCollider)) collision = true;
        }

        if (_wallLeft != null)
        {
            if (_collider.CheckCollision(_wallLeft.GetCollider)) collision = true;
        }

        _physics.SetSpeed(0);
        _physics.SetDirection(Vector2.zero);

        if (!collision)
        {
            var getInput = Input.GetAxisRaw("Horizontal");
            if (getInput != 0f)
            {
                _physics.SetDirection(Vector2.right);
                _physics.SetSpeed(getInput * _speed);
                _physics.UpdatePhysics();
            }
        }
    }
}
