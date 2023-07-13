using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviourGameplay
{
    [SerializeField] private CustomColliderSphere _collider;
    [SerializeField] private CustomPhysics _physics;

    public ICollider GetCollider => _collider;


    public override void ManagedUpdate()
    {
        _physics.UpdatePhysics();
    }

    public void Reflect()
    {
        _physics.Reflect(_collider.Normal);
    }

    public void DestroyBall()
    {
        Destroy(this.gameObject);
    }
}
