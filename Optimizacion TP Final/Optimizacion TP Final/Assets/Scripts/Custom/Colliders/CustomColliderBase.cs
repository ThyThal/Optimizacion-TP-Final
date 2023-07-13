using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomColliderBase : MonoBehaviourGameplay, ICollider
{
    // Common properties and methods for all colliders
    public abstract bool CheckCollision(ICollider other);

    protected abstract void DrawGizmo();

    protected virtual void OnDrawGizmos()
    {
        // Set the gizmo color
        Gizmos.color = Color.yellow;

        // Draw the collider shape gizmo
        DrawGizmo();
    }
}