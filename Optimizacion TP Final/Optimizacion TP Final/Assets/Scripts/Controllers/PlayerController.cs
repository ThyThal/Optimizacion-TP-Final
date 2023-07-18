using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviourGameplay
{
    [Header("Components")]
    [SerializeField] private CustomColliderBox _collider;
    [SerializeField] private CustomPhysics _physics;

    [Header("Ball")]
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private BallController _currentBall;
    [SerializeField] private bool _waitingForShoot = false;


    [SerializeField] private WallController _wallLeft;
    [SerializeField] private WallController _wallRight;
    [SerializeField] private float _speed = 10f;

    [SerializeField] private bool wallCollisions = false;


    public bool WaitingForShoot => _waitingForShoot;
    public CustomColliderBox GetCollider => _collider;

    public override void Awake()
    {
        base.Awake();
        _collider = new CustomColliderBox(transform);
        _physics = new CustomPhysics(transform, _collider);
    }

    private void Start()
    {
        SpawnNewBall();
    }

    public override void ManagedUpdate()
    {                
        CheckInputs();
        CheckCollisions();
    }

    private void CheckCollisions()
    {
        wallCollisions = false;

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
                ballController.ReflectWithPlayer(transform.position);
            }
        }

        if (_wallRight != null)
        {
            if (_collider.CheckCollision(_wallRight.GetCollider)) wallCollisions = true;
        }

        if (_wallLeft != null)
        {
            if (_collider.CheckCollision(_wallLeft.GetCollider)) wallCollisions = true;
        }
    }

    private void CheckInputs()
    {
        if (_waitingForShoot)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Shoot();
            }
        }

        _physics.SetSpeed(0);
        _physics.SetDirection(Vector2.zero);

        if (!wallCollisions)
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

    public void StopMovement()
    {
        _physics.SetDirection(Vector2.zero);
    }

    public void SpawnNewBall()
    {
        _currentBall = GameManager.Instance.LevelManager.GetPlayerBall();
        _waitingForShoot = true;
    }

    private void Shoot()
    {
        _waitingForShoot = false;
        _currentBall.StartMovement();
        _shootingPoint.DetachChildren();
    }
}
