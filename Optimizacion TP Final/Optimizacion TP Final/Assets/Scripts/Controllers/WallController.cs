using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CustomColliderBox))]
public class WallController : MonoBehaviourGameplay
{
    [SerializeField] private CustomColliderBox _collider;
    [SerializeField] private bool _death = false;
    public Mesh mesh;

    public override void ManagedUpdate()
    {
        // Check for colision with all balls.
        for (int index = 0; index < GameManager.Instance.LevelManager.Balls.Count; index++)
        {
            BallController ballController = GameManager.Instance.LevelManager.GetBallController(index);

            // Check collision with ball.
            if (_collider.CheckCollision(ballController.GetCollider))
            {
                if (_death)
                {
                    ballController.DestroyBall();
                }

                else
                {
                    ballController.Reflect();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (mesh == null)
            return;

        Gizmos.color = Color.yellow;

        if (_death == true)
            Gizmos.color = Color.red;


        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.DrawMesh(mesh, Vector3.zero, Quaternion.identity, Vector3.one * 1);
    }
}