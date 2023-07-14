using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CustomColliderBox : CustomColliderBase
{
    [SerializeField] private Transform _transform;

    // Cache other collider.
    private ICollider _other;

    public CustomColliderBox(Transform transform)
    {
        _transform = transform;
    }

    // Check Collision with Others.
    public override bool CheckCollision(ICollider other)
    {
        _other = other;


        if (_other is CustomColliderBox otherColliderBox)
        {
            if (CheckCollisionWithBox(otherColliderBox))
            {
                ResolveCollision(otherColliderBox);
                return true;
            }
        }

        else if (_other is CustomColliderSphere otherColliderSphere)
        {
            // Check if is not colliding with the sphere, and return.
            if (!CheckCollisionWithSphere(otherColliderSphere)) return false;

            // If is colliding with sphere continue.
            CalculateCollisionNormal(otherColliderSphere);
            ResolveCollision(otherColliderSphere);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Check if Box Collider is Overlapping.
    /// </summary>
    /// <param name="otherColliderBox">Collider of the other object</param>
    /// <returns></returns>
    private bool CheckCollisionWithBox(CustomColliderBox otherColliderBox)
    {
        // Calculate the bounds of each box collider
        Bounds boundsA = new Bounds(_transform.localPosition, _transform.localScale);
        Bounds boundsB = new Bounds(otherColliderBox._transform.localPosition, otherColliderBox._transform.localScale);

        // Check if the bounds overlap
        if (boundsA.Intersects(boundsB))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Check if Sphere Colliders is Overlapping.
    /// </summary>
    /// <param name="sphereCollider">Collider of the other object</param>
    /// <returns></returns>
    private bool CheckCollisionWithSphere(CustomColliderSphere sphereCollider)
    {
        if (sphereCollider == null) return false;

        Vector2 closestPoint = Vector2.Max(_transform.position - _transform.localScale * 0.5f,
                               Vector2.Min(sphereCollider._transform.position, _transform.position + _transform.localScale * 0.5f));

        float distance = Vector2.Distance(closestPoint, sphereCollider._transform.position);

        return distance <= sphereCollider.Radius;
    }

    /// <summary>
    /// Calculate the normal for Sphere Reflection.
    /// </summary>
    private void CalculateCollisionNormal(CustomColliderSphere sphereCollider)
    {
        sphereCollider.CollisionNormal = CalculateBoxSphereCollisionNormal(this, sphereCollider);
    }

    /// <summary>
    /// Calculates the normal between a Box and a Sphere.
    /// </summary>
    /// <returns>Normal of the Collision Point</returns>
    private Vector2 CalculateBoxSphereCollisionNormal(CustomColliderBox boxCollider, CustomColliderSphere sphereCollider)
    {
        Vector2 sphereCenter = sphereCollider._transform.position;
        Vector2 closestPoint = GetClosestPointOnBox(boxCollider, sphereCenter);
        Vector2 collisionNormal = sphereCenter - closestPoint;
        collisionNormal.Normalize();

        return collisionNormal;
    }

    private Vector2 GetClosestPointOnBox(CustomColliderBox boxCollider, Vector2 point)
    {
        Vector2 boxCenter = boxCollider._transform.position;
        Vector2 boxSize = _transform.localScale;
        Vector2 halfExtents = boxSize * 0.5f;
        Vector2 direction = point - boxCenter;
        Vector2 clampedDirection = new Vector2(
            Mathf.Clamp(direction.x, -halfExtents.x, halfExtents.x),
            Mathf.Clamp(direction.y, -halfExtents.y, halfExtents.y)
        );

        Vector2 closestPoint = boxCenter + clampedDirection;

        return closestPoint;
    }

    private void ResolveCollision(CustomColliderBox otherColliderBox)
    {
        Vector2 collisionNormal = _transform.position - otherColliderBox._transform.position;
        float penetrationDepth = Mathf.Abs(_transform.position.x - otherColliderBox._transform.position.x) - (_transform.localScale.x + otherColliderBox._transform.localScale.x) / 2;

        float separationDistance = penetrationDepth + 0.001f;
        Vector3 correction = new Vector3(separationDistance * collisionNormal.x, 0, 0);

        _transform.position -= correction;
        //otherColliderBox._transform.position -= correction;
    }

    private void ResolveCollision(CustomColliderSphere otherColliderSphere)
    {
        Vector2 collisionNormal = _transform.position - otherColliderSphere._transform.position;
        float penetrationDepth = Mathf.Abs(_transform.position.x - otherColliderSphere._transform.position.x) - (_transform.localScale.x + otherColliderSphere.Radius) / 2;

        float separationDistance = penetrationDepth + 0.001f;
        Vector3 correction = new Vector3(separationDistance * collisionNormal.x, separationDistance * collisionNormal.y, 0);

        //_transform.position += correction;
        //otherColliderSphere._transform.position -= correction;
    }

    protected override void DrawGizmo()
    {
        // Get the center position of the collider
        Vector2 center = this._transform.position;

        // Draw the wire cube representing the box collider
        Gizmos.DrawWireCube(center, _transform.localScale);
    }
}
