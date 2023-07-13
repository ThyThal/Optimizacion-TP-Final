using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPhysics : MonoBehaviourGameplay
{
    [SerializeField] private CustomColliderBase _collider;
    [SerializeField] private Vector2 _direction = Vector2.up;
    [SerializeField] private float _speed = 1;

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    public void Reflect(Vector2 collisionNormal)
    {
        Vector2 incidentDirection = _direction;
        Vector2 reflectionDirection = Vector2.Reflect(incidentDirection, collisionNormal);
        _direction = reflectionDirection;
    }

    public void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }
}
