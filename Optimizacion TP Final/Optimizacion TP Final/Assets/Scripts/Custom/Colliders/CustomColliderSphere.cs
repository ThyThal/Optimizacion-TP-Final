using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CustomColliderSphere : CustomColliderBase
{
    [SerializeField] public float Radius = 0.5f;
    [SerializeField] public Vector2 Normal;

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
        var center = transform.position;

        // Draw the wire sphere representing the sphere collider
        Gizmos.DrawWireSphere(center, Radius);
    }
}
