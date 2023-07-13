using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomColliderBase : MonoBehaviourGameplay, ICollider
{
    // Common properties and methods for all colliders
    public abstract void CheckCollision(ICollider other);

    protected abstract void DrawGizmo();

    // Gizmo color for the collider
    protected Color gizmoColor = Color.yellow;

    protected virtual void OnDrawGizmos()
    {
        // Set the gizmo color
        Gizmos.color = gizmoColor;

        // Draw the collider shape gizmo
        DrawGizmo();
    }
}