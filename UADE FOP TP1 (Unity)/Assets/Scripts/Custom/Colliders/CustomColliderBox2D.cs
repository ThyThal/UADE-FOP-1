using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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
                
                ResolveCollision(otherColliderBox);
                return true;
            
            // Other Collider Sphere.
            case CustomColliderCircle2D otherColliderSphere:
                if (!CheckCollisionWithSphere(otherColliderSphere)) return false;

                //ResolveCollision(otherColliderSphere);
                return true;

            default:
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

    /// <summary>
    /// Calculate the normal for Sphere Reflection.
    /// </summary>
    private void CalculateCollisionNormal(CustomColliderCircle2D circle2DCollider)
    {
        circle2DCollider.CollisionNormal = CalculateBoxSphereCollisionNormal(this, circle2DCollider);
    }

    /// <summary>
    /// Calculates the normal between a Box and a Sphere.
    /// </summary>
    /// <returns>Normal of the Collision Point</returns>
    private Vector2 CalculateBoxSphereCollisionNormal(CustomColliderBox2D box2DCollider, CustomColliderCircle2D circle2DCollider)
    {
        Vector2 sphereCenter = circle2DCollider.Transform.position;
        Vector2 closestPoint = GetClosestPointOnBox(box2DCollider, sphereCenter);
        Vector2 collisionNormal = sphereCenter - closestPoint;
        collisionNormal.Normalize();

        return collisionNormal;
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

    private void ResolveCollision(CustomColliderBox2D otherColliderBox2D)
    {
        Vector2 collisionNormal = Transform.position - otherColliderBox2D.Transform.position;
        float penetrationDepth = Mathf.Abs(Transform.position.x - otherColliderBox2D.Transform.position.x) - (Transform.localScale.x + otherColliderBox2D.Transform.localScale.x) / 2;

        float separationDistance = penetrationDepth + 0.001f;
        Vector3 correction = new Vector3(separationDistance * collisionNormal.x, 0, 0);

        Transform.position -= correction;
        //otherColliderBox._transform.position -= correction;
    }

    private void ResolveCollision(CustomColliderCircle2D otherColliderCircle2D)
    {
        /*Vector2 collisionNormal = _transform.position - otherColliderSphere._transform.position;
        float penetrationDepth = Mathf.Abs(_transform.position.x - otherColliderSphere._transform.position.x) - (_transform.localScale.x + otherColliderSphere.Radius) / 2;

        float separationDistance = penetrationDepth + 0.001f;
        Vector3 correction = new Vector3(separationDistance * collisionNormal.x, separationDistance * collisionNormal.y, 0);

        _transform.position += correction;
        otherColliderSphere._transform.position -= correction;
        */
    }

    protected override void DrawGizmo()
    {
        // Get the center position of the collider
        Vector2 center = Transform.position;

        // Draw the wire cube representing the box collider
        Gizmos.DrawWireCube(center, Transform.localScale);
    }
}
