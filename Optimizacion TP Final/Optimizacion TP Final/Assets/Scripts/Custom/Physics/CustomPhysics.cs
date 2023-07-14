using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CustomPhysics
{
    [SerializeField] private Transform _transform;
    [SerializeField] private CustomColliderBase _collider;
    [SerializeField] private Vector2 _direction = Vector2.zero;
    [SerializeField] private float _speed = 1;

    public CustomPhysics(Transform transform, CustomColliderBase collider)
    {
        _transform = transform;
        _collider = collider;
    }

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
        var reflectionDirection = Vector2.Reflect(_direction, collisionNormal);
        _direction = reflectionDirection;
    }

    public void UpdatePhysics()
    {

        var last = CustomUpdateManager.Instance.CustomUpdateGameplay.PreviousUpdate;
        var current = CustomUpdateManager.Instance.CustomUpdateGameplay.LastUpdate;
        var delta = current - last;

        _transform.Translate(_direction * _speed * delta);
    }
}
