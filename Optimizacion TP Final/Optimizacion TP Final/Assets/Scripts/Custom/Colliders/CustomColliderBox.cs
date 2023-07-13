using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomColliderBox : CustomColliderBase
{
    [SerializeField] private Transform _transform;

    public override bool CheckCollision(ICollider other)
    {
        // Box Collisions for Player with Walls.
        if (other is CustomColliderBox boxCollider)
        {
            Debug.Log("Checking Box Collisions...");
            return CheckBoxBoxCollision(boxCollider);
        }

        // Sphere Collision for Player and Bricks with Ball.
        else if (other is CustomColliderSphere sphereCollider)
        {
            // Check collision between a box collider and a sphere collider
            if (!CheckBoxSphereCollision(sphereCollider)) return false;
            
            // Handle collision between the box collider and the sphere collider
            CalculateCollisionNormal(this, sphereCollider);
            return true;
        }

        return false;
    }

    private bool CheckBoxBoxCollision(CustomColliderBox other)
    {
        // Calculate the bounds of each box collider
        Bounds boundsA = new Bounds(_transform.localPosition, _transform.localScale);
        Bounds boundsB = new Bounds(other.transform.localPosition, other.transform.localScale);

        // Check if the bounds overlap
        if (boundsA.Intersects(boundsB))
        {
            Debug.Log("CAJOTA");
            return true;
        }

        Debug.Log("CAJan't");
        return false;
    }
    
    private bool CheckBoxSphereCollision(CustomColliderSphere sphereCollider)
    {
        if (sphereCollider == null) return false;

        Vector2 closestPoint = Vector2.Max(_transform.position - _transform.localScale * 0.5f,
                               Vector2.Min(sphereCollider.transform.position, _transform.position + _transform.localScale * 0.5f));

        float distance = Vector2.Distance(closestPoint, sphereCollider.transform.position);

        return distance <= sphereCollider.Radius;
    }

    protected override void DrawGizmo()
    {
        // Get the center position of the collider
        Vector2 center = this.transform.position;

        // Draw the wire cube representing the box collider
        Gizmos.DrawWireCube(center, transform.localScale);
    }

    private void CalculateCollisionNormal(CustomColliderBox boxCollider, CustomColliderSphere sphereCollider)
    {
        Vector2 collisionNormal = Vector2.zero;

        if (boxCollider is CustomColliderBox colliderBox && sphereCollider is CustomColliderSphere colliderSphere)
        {
            // Calculate the collision normal between two box colliders
            collisionNormal = CalculateBoxSphereCollisionNormal(boxCollider, sphereCollider);
        }

        sphereCollider.Normal = collisionNormal;
    }

    private Vector2 CalculateBoxSphereCollisionNormal(CustomColliderBox boxCollider, CustomColliderSphere sphereCollider)
    {
        // Calculate the center of the sphere
        Vector2 sphereCenter = sphereCollider.transform.position;

        // Calculate the closest point on the box collider's surface to the sphere's center
        Vector2 closestPoint = GetClosestPointOnBox(boxCollider, sphereCenter);

        // Calculate the collision normal by subtracting the closest point from the sphere's center
        Vector2 collisionNormal = sphereCenter - closestPoint;

        // Normalize the collision normal
        collisionNormal.Normalize();

        return collisionNormal;
    }

    private Vector2 GetClosestPointOnBox(CustomColliderBox boxCollider, Vector2 point)
    {
        // Calculate the center and size of the box collider
        Vector2 boxCenter = boxCollider.transform.position;
        Vector2 boxSize = _transform.localScale;

        // Calculate the half extents of the box collider
        Vector2 halfExtents = boxSize * 0.5f;

        // Calculate the direction from the box's center to the point
        Vector2 direction = point - boxCenter;

        // Clamp the direction vector to the box's extents
        Vector2 clampedDirection = new Vector3(
            Mathf.Clamp(direction.x, -halfExtents.x, halfExtents.x),
            Mathf.Clamp(direction.y, -halfExtents.y, halfExtents.y)
        );

        // Calculate the closest point on the box's surface
        Vector2 closestPoint = boxCenter + clampedDirection;

        return closestPoint;
    }
}
