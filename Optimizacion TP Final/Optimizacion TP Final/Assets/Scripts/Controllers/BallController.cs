using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviourGameplay
{
    [SerializeField] private CustomColliderSphere _collider;
    [SerializeField] private CustomPhysics _physics;
    [SerializeField] private bool _playerBall = false;

    [SerializeField] private float _speed = 10f;

    public bool IsPlayerBall => _playerBall;
    public ICollider GetCollider => _collider;

    public override void Awake()
    {
        base.Awake();
        _collider = new CustomColliderSphere(transform);
        _physics = new CustomPhysics(transform, _collider);
        _physics.SetSpeed(_speed);
    }

    public override void ManagedUpdate()
    {
        _physics.UpdatePhysics();
    }

    public void Reflect()
    {
        _physics.Reflect(_collider.CollisionNormal);
    }

    public void ReflectWithPlayer(Vector2 playerPos)
    {
        var dir = _physics.Reflect(_collider.CollisionNormal);
        _physics.SetDirection(dir + PlayerHitFactor(playerPos));
    }

    public void DestroyBall()
    {
        GameManager.Instance.LevelManager.CheckDefeat(this);
        gameObject.SetActive(false);
    }

    public void StartMovement()
    {
        _physics.SetDirection(new Vector2(Random.Range(-1f, 1f), 1));
        transform.parent = null;
    }

    public void InitializeBall(bool isPlayer, float speed, Transform spawnPoint)
    {
        transform.parent = spawnPoint;
        transform.localPosition = Vector3.zero;
        _physics.SetDirection(Vector2.zero);
        _playerBall = isPlayer;
        _speed = speed;

        if (isPlayer == false)
        {
            StartMovement();
        }
    }

    public Vector2 PlayerHitFactor(Vector2 racketPos)
    {
        return new Vector2((transform.position.x - racketPos.x) / 5f, 0f); // 5 es el ancho del player
        
    }
}
