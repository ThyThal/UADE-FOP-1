using UnityEngine;

public class CustomColliderBox2D : CustomColliderBase
{
    // Cache other collider.
    private ICollider _otherCollider;

    // Check Collision with Others.
    public override bool CheckCollision(ICollider other)
    {
        _otherCollider = other;

        switch (_otherCollider)
        {
            // Other Collider Box.
            case CustomColliderBox2D otherColliderBox:
                if (!CollisionBoxBox(this, otherColliderBox)) return false;
                
                GyzmoColor = Color.cyan;
                //otherColliderBox.GyzmoColor = Color.green;
                
                ResolveBoxCollision(otherColliderBox);
                return true;
            
            // Other Collider Sphere.
            case CustomColliderCircle2D otherColliderSphere:
                if (!CheckCollisionWithSphere(otherColliderSphere)) return false;

                //ResolveCollision(otherColliderSphere);
                return true;

            default:
                GyzmoColor = Color.red;
                return false;
        }
    }

    /// <summary>
    /// Check if Sphere Colliders is Overlapping.
    /// </summary>
    /// <param name="circle2DCollider">Collider of the other object</param>
    /// <returns></returns>
    private bool CheckCollisionWithSphere(CustomColliderCircle2D circle2DCollider)
    {
        // Lazy computation
        if (circle2DCollider == null) return false;

        Vector2 closestPoint = Vector2.Max(Transform.position - Transform.localScale * 0.5f,
                               Vector2.Min(circle2DCollider.Transform.position, Transform.position + Transform.localScale * 0.5f));

        float distance = Vector2.Distance(closestPoint, circle2DCollider.Transform.position);

        return distance <= circle2DCollider.Radius;
    }
    
    private Vector2 GetClosestPointOnBox(CustomColliderBox2D box2DCollider, Vector2 point)
    {
        Vector2 boxCenter = box2DCollider.Transform.position;
        Vector2 boxSize = Transform.localScale;
        Vector2 halfExtents = boxSize * 0.5f;
        Vector2 direction = point - boxCenter;
        Vector2 clampedDirection = new Vector2(
            Mathf.Clamp(direction.x, -halfExtents.x, halfExtents.x),
            Mathf.Clamp(direction.y, -halfExtents.y, halfExtents.y)
        );

        Vector2 closestPoint = boxCenter + clampedDirection;

        return closestPoint;
    }

    private void ResolveBoxCollision(CustomColliderBox2D otherColliderBox2D)
    {
        Vector2 collisionNormal = Transform.position - otherColliderBox2D.Transform.position;
        float penetrationDepth = Mathf.Abs(Transform.position.x - otherColliderBox2D.Transform.position.x) - (Transform.localScale.x + otherColliderBox2D.Transform.localScale.x) / 2;

        float separationDistance = penetrationDepth + 0.001f;
        Vector3 correction = new Vector3(separationDistance * collisionNormal.x, 0, 0);

        Transform.position -= correction;
    }

    protected override void DrawGizmo()
    {
        // Get the center position of the collider
        Vector2 center = Transform.position;

        // Draw the wire cube representing the box collider
        Gizmos.DrawWireCube(center, Transform.localScale);
    }
}
