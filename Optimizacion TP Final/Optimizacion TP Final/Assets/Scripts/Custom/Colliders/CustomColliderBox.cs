using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomColliderBox : CustomColliderBase
{
    public override void CheckCollision(ICollider other)
    {
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
            // Implement collision logic between box and sphere colliders
        }
    }

    protected override void DrawGizmo()
    {
        // Get the center position of the collider
        Vector3 center = this.transform.position;

        // Draw the wire cube representing the box collider
        Gizmos.DrawWireCube(center, transform.localScale);
    }
}
