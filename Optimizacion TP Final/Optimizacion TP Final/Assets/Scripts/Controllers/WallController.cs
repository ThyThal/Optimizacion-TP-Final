using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CustomColliderBox))]
public class WallController : MonoBehaviourGameplay
{
    [SerializeField] private CustomColliderBox _collider;
    [SerializeField] private bool _death = false;
    [SerializeField] private Mesh _mesh;

    public ICollider GetCollider => _collider;

    private BallController _ballCollisionCheck;

    public override void Awake()
    {
        base.Awake();
        _collider = new CustomColliderBox(transform);
    }

    public override void ManagedUpdate()
    {
        if (GameManager.Instance.LevelManager.FinishedGame) return;

        // Check for collision with all balls.
        for (var index = 0; index < GameManager.Instance.LevelManager.Balls.Count; index++)
        {
            _ballCollisionCheck = GameManager.Instance.LevelManager.GetBallController(index);

            // Check collision with ball.
            if (!_collider.CheckCollision(_ballCollisionCheck.GetCollider)) continue;
            
            if (_death)
            {
                _ballCollisionCheck.DestroyBall();
            }

            else
            {
                _ballCollisionCheck.Reflect();
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        if (_mesh == null)
            return;

        Gizmos.color = Color.yellow;

        if (_death == true)
            Gizmos.color = Color.red;


        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.DrawMesh(_mesh, Vector3.zero, Quaternion.identity, Vector3.one * 1);
    }
}