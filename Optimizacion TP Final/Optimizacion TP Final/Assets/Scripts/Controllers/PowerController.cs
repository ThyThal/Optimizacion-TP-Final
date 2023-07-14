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
        _physics.SetSpeed(5);
    }

    public override void ManagedUpdate()
    {
        if (_collider.CheckCollision(GameManager.Instance.LevelManager.GetPlayer.GetCollider))
        {
            Destroy(this.gameObject);
            GameManager.Instance.LevelManager.DoPower();
        }

        else if (_collider.CheckCollision(GameManager.Instance.LevelManager.GetDeathCollider))
        {
            Destroy(this.gameObject);
        }

        else
        {
            _physics.UpdatePhysics();
        }
    }
}
