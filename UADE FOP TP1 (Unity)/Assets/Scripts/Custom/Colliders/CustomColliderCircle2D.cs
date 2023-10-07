using UnityEngine;

public class CustomColliderCircle2D : CustomColliderBase
{
    [SerializeField] public float Radius = 0.5f;
    [SerializeField] private Vector2 _collisionNormal;
    
    // Cache other collider.
    private ICollider _otherCollider;
    
    public override bool CheckCollision(ICollider other)
    {
        _otherCollider = other;
        
        switch (_otherCollider)
        {
            case CustomColliderBox2D boxCollider:
                if (!CollisionCircleBox(this, boxCollider))
                {
                    return false;
                }

                if (boxCollider.GetComponent<CustomMonoBehaviour>().Static == true) { boxCollider.GyzmoColor = Color.yellow; }
                else { boxCollider.GyzmoColor = Color.grey; }                
                GyzmoColor = Color.cyan;

                return true;

            case CustomColliderCircle2D otherColliderCircle:
                if (!CollisionCircleCircle(this, otherColliderCircle)) return false;

                if (otherColliderCircle.GetComponent<CustomMonoBehaviour>().Static == true) { otherColliderCircle.GyzmoColor = Color.yellow; }
                else { otherColliderCircle.GyzmoColor = Color.cyan; }
                GyzmoColor = Color.cyan;

                ResolveCircleCollision(otherColliderCircle);

                return true;
            
            default:
                GyzmoColor = Color.red;
                return false;
        }
    }

    protected override void DrawGizmo()
    {
        // Get the center position of the collider
        var center = Transform.position;

        // Draw the wire sphere representing the sphere collider
        Gizmos.DrawWireSphere(center, Radius);
    }

    public void ResolveCircleCollision(CustomColliderCircle2D other)
    {
        // Calculate the vector between the centers of the two circles
        Vector2 collisionVector = other.Position2D - Position2D;
    
        // Calculate the distance between the centers of the two circles
        float distance = collisionVector.magnitude;
    
        // Calculate the amount of overlap
        float overlap = Radius + other.Radius - distance;
    
        // If there's overlap, move the circles apart
        if (overlap > 0)
        {
            collisionVector.Normalize();
            Vector2 separation = collisionVector * (overlap / 2);

            Transform.position -= new Vector3(separation.x, separation.y,0);
        }
    }
}
