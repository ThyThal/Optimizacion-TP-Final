using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomColliderBox : CustomColliderBase
{
    [SerializeField] private Transform _transform;

    public override bool CheckCollision(ICollider other)
    {
        Debug.Log("Collider Box: Checking for Collisions...");

        // Box Collisions for Player with Walls.
        if (other is CustomBoxCollider boxCollider)
        {
            // Check collision between two box colliders
            // Implement collision logic for box colliders
        }

        // Sphere Collision for Player and Bricks with Ball.
        else if (other is CustomColliderSphere sphereCollider)
        {
            // Check collision between a box collider and a sphere collider
            if (CheckBoxSphereCollision(this, sphereCollider))
            {
                // Handle collision between the box collider and the sphere collider
                Debug.Log("Box-Sphere collision detected!");
                CalculateCollisionNormal(this, sphereCollider);
                return true;
            }
        }

        return false;
    }

    private bool CheckBoxSphereCollision(CustomColliderBox boxCollider, CustomColliderSphere sphereCollider)
    {
        Vector3 closestPoint = Vector3.Max(boxCollider.transform.position - _transform.localScale * 0.5f,
                               Vector3.Min(sphereCollider.transform.position, boxCollider.transform.position + _transform.localScale * 0.5f));

        float distance = Vector3.Distance(closestPoint, sphereCollider.transform.position);

        return distance <= sphereCollider.radius;
    }

    protected override void DrawGizmo()
    {
        // Get the center position of the collider
        Vector3 center = this.transform.position;

        // Draw the wire cube representing the box collider
        Gizmos.DrawWireCube(center, transform.localScale);
    }

    private void CalculateCollisionNormal(CustomColliderBox boxCollider, CustomColliderSphere sphereCollider)
    {
        Vector3 collisionNormal = Vector3.zero;

        if (boxCollider is CustomColliderBox colliderBox && sphereCollider is CustomColliderSphere colliderSphere)
        {
            // Calculate the collision normal between two box colliders
            collisionNormal = CalculateBoxSphereCollisionNormal(boxCollider, sphereCollider);
        }

        sphereCollider.normal = collisionNormal;
    }

    private Vector3 CalculateBoxSphereCollisionNormal(CustomColliderBox boxCollider, CustomColliderSphere sphereCollider)
    {
        // Calculate the center of the sphere
        Vector3 sphereCenter = sphereCollider.transform.position;

        // Calculate the closest point on the box collider's surface to the sphere's center
        Vector3 closestPoint = GetClosestPointOnBox(boxCollider, sphereCenter);

        // Calculate the collision normal by subtracting the closest point from the sphere's center
        Vector3 collisionNormal = sphereCenter - closestPoint;

        // Normalize the collision normal
        collisionNormal.Normalize();

        return collisionNormal;
    }

    private Vector3 GetClosestPointOnBox(CustomColliderBox boxCollider, Vector3 point)
    {
        // Calculate the center and size of the box collider
        Vector3 boxCenter = boxCollider.transform.position;
        Vector3 boxSize = _transform.localScale;

        // Calculate the half extents of the box collider
        Vector3 halfExtents = boxSize * 0.5f;

        // Calculate the direction from the box's center to the point
        Vector3 direction = point - boxCenter;

        // Clamp the direction vector to the box's extents
        Vector3 clampedDirection = new Vector3(
            Mathf.Clamp(direction.x, -halfExtents.x, halfExtents.x),
            Mathf.Clamp(direction.y, -halfExtents.y, halfExtents.y),
            Mathf.Clamp(direction.z, -halfExtents.z, halfExtents.z)
        );

        // Calculate the closest point on the box's surface
        Vector3 closestPoint = boxCenter + clampedDirection;

        return closestPoint;
    }
}
