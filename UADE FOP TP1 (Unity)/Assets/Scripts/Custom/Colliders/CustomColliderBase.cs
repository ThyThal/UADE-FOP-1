using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class CustomColliderBase : MonoBehaviour, ICollider
{
    [SerializeField] private Transform _transform;
    private Vector2 _halfLocalScale;

    public Color GyzmoColor = Color.yellow;
    
    public abstract bool CheckCollision(ICollider other);

    protected abstract void DrawGizmo();

    protected virtual void OnDrawGizmos()
    {
        // Set the gizmo color
        Gizmos.color = GyzmoColor;

        // Draw the collider shape gizmo
        DrawGizmo();
    }
    
    public Transform Transform
    {
        get { return _transform; }
        set { _transform = value; }
    }

    public Vector2 Position2D
    {
        get { return new Vector2(_transform.position.x, _transform.position.y); }
    }

    public Vector2 HalfScale => Transform.localScale / 2f;

    protected bool CollisionBoxBox(CustomColliderBox2D self, CustomColliderBox2D other)
    {
        return (self.Transform.localPosition.x - self.HalfScale.x < other.Transform.localPosition.x + other.HalfScale.x &&
                self.Transform.localPosition.x + self.HalfScale.x > other.Transform.localPosition.x - other.HalfScale.x &&
                self.Transform.localPosition.y - self.HalfScale.y < other.Transform.localPosition.y + other.HalfScale.y &&
                self.Transform.localPosition.y + self.HalfScale.y > other.Transform.localPosition.y - other.HalfScale.y );
    }
    
    /*
    protected bool CollisionBoxCircle(CustomColliderBox2D self, CustomColliderCircle2D other)
    {
        Vector2 closestPoint = Vector2.Max(self.Transform.position - self.Transform.localScale * 0.5f,
                            Vector2.Min(other.Transform.position, self.Transform.position + self.Transform.localScale * 0.5f));

        float distance = Vector2.Distance(closestPoint, other.Transform.position);

        return distance <= other.Radius;
    }
    */
    
    protected bool CollisionCircleCircle(CustomColliderCircle2D self, CustomColliderCircle2D other)
    {
        return Vector2.Distance(self.Transform.position, other.Transform.position) < self.Radius + other.Radius;
    }


    protected bool CollisionCircleBox(CustomColliderCircle2D self, CustomColliderBox2D other)
    {
        //Calculate the distance between the circle's center and the square's center
       

        float dx = transform.position.x - other.transform.position.x;
        float dy = transform.position.y - other.transform.position.y;



        // Calculate the squared distance
        float distanceSquared = (dx * dx) + (dy * dy);

        // Check if the squared distance is less than the squared sum of the radii
        float combinedRadius = self.Radius + other.transform.localScale.x / 2f; // Assuming square is oriented as a diamond
        float combinedRadiusSquared = combinedRadius * combinedRadius;

        return distanceSquared < combinedRadiusSquared;
        
    }

}