using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomColliderSphere : CustomColliderBase
{
    public float radius = 0.5f;

    public override bool CheckCollision(ICollider other)
    {
        if (other is CustomBoxCollider boxCollider)
        {
            // Check collision between a sphere collider and a box collider
            // Implement collision logic between sphere and box colliders
        }

        else if (other is CustomColliderSphere sphereCollider)
        {
            // Check collision between two sphere colliders
            // Implement collision logic for sphere colliders
        }

        return false;
    }

    protected override void DrawGizmo()
    {
        // Get the center position of the collider
        Vector3 center = transform.position;

        // Draw the wire sphere representing the sphere collider
        Gizmos.DrawWireSphere(center, radius);
    }
}
