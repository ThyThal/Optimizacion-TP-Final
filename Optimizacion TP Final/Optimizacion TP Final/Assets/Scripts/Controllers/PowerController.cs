using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerController : MonoBehaviourGameplay
{
    [SerializeField] private CustomColliderBox _collider;
    [SerializeField] private CustomPhysics _physics;

    public override void Awake()
    {
        base.Awake();
        _collider = new CustomColliderBox(transform);
        _physics = new CustomPhysics(transform, _collider);
        _physics.SetDirection(Vector2.down);
    }
}
