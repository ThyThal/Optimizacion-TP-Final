using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviourGameplay
{
    [SerializeField] private CustomColliderSphere _collider;
    [SerializeField] private CustomPhysics _physics;

    public ICollider GetCollider => _collider;

    public override void ManagedUpdate()
    {
        _physics.Update();
    }

    public void Reflect()
    {
        _physics.Reflect(_collider.Normal);
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
