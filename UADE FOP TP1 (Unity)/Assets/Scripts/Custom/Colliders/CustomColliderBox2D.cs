using UnityEngine;

public class CustomColliderBox2D : CustomColliderBase
{
    // Cache
    private ICollider _otherCollider;

    // Chequeo de colisiones con los otros
    public override bool CheckCollision(ICollider other)
    {
        _otherCollider = other;

        switch (_otherCollider)
        {
            case CustomColliderBox2D otherColliderBox:
                if (!CollisionBoxBox(this, otherColliderBox))
                {
                    return false;
                }

                return true;
            
            case CustomColliderCircle2D otherColliderSphere:
                if (!CheckCollisionWithSphere(otherColliderSphere))
                {
                    return false;
                }

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

        Vector2 closestPoint = Vector2.Max(Position2D - HalfScale,
                               Vector2.Min(circle2DCollider.Position2D, Position2D + HalfScale));

        float distance = Vector2.Distance(closestPoint, circle2DCollider.Position2D);

        return distance <= circle2DCollider.Radius;
    }

    protected override void DrawGizmo()
    {
        // Da la pocision central de la colision
        Vector2 center = Transform.position;

        // Da color a la colision del cubo
        Gizmos.DrawWireCube(center, Transform.localScale);
    }
}
