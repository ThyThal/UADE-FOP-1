using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomColliderBase : MonoBehaviour, ICollider
{
    [SerializeField] private Transform _transform;
    
    public abstract bool CheckCollision(ICollider other);

    protected abstract void DrawGizmo();

    protected virtual void OnDrawGizmos()
    {
        // Set the gizmo color
        Gizmos.color = Color.yellow;

        // Draw the collider shape gizmo
        DrawGizmo();
    }
    
    public Transform Transform
    {
        get { return _transform; }
        set { _transform = value; }
    }
}