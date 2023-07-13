using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviourGameplay
{
    [SerializeField] private CustomColliderSphere _collider;

    public ICollider GetCollider => _collider;
}
