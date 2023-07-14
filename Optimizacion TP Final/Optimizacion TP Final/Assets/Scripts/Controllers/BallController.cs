using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviourGameplay
{
    [SerializeField] private CustomColliderSphere _collider;
    [SerializeField] private CustomPhysics _physics;

    [SerializeField] private float speed = 10f;

    public ICollider GetCollider => _collider;

    public override void Awake()
    {
        base.Awake();
        _collider = new CustomColliderSphere(transform);
        _physics = new CustomPhysics(transform, _collider);
        _physics.SetSpeed(speed);
    }

    public override void ManagedUpdate()
    {
        _physics.UpdatePhysics();
    }

    public void Reflect()
    {
        _physics.Reflect(_collider.CollisionNormal);
    }

    public void DestroyBall()
    {
        GameManager.Instance.LevelManager.CheckDefeat(this);
        gameObject.SetActive(false);
    }

    public void StartMovement()
    {
        _physics.SetDirection(new Vector2(Random.value, 1));
        transform.parent = null;
    }
}
