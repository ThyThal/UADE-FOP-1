using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class CustomColliderCircle2D : CustomColliderBase
{
    [SerializeField] public float Radius = 0.5f;
    [SerializeField] public Vector2 CollisionNormal;
    
    // Cache other collider.
    private ICollider _otherCollider;
    
    public override bool CheckCollision(ICollider other)
    {
        if (other is CustomColliderBox2D boxCollider)
        {
            // Check collision between a sphere collider and a box collider
            // Implement collision logic between sphere and box colliders
        }

        else if (other is CustomColliderCircle2D sphereCollider)
        {
            if (!CollisionCircleCircle(this, sphereCollider)) return false;
        }

        return false;
    }

    protected override void DrawGizmo()
    {
        // Get the center position of the collider
        var center = Transform.position;

        // Draw the wire sphere representing the sphere collider
        Gizmos.DrawWireSphere(center, Radius);
    }
}
