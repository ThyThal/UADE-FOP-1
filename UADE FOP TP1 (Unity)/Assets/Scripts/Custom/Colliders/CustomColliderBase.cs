using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class CustomColliderBase : MonoBehaviour, ICollider
{
    [SerializeField] private Transform _transform;
    [SerializeField] private SpriteRenderer _renderer;
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
        // Check collision between circle and box
        float closestX = Mathf.Clamp(Transform.position.x, other.Transform.position.x - other.HalfScale.x, other.Transform.position.x + other.HalfScale.x);
        float closestY = Mathf.Clamp(Transform.position.y, other.Transform.position.y - other.HalfScale.y, other.Transform.position.y + other.HalfScale.y);

        Vector2 closestPoint = new Vector2(closestX, closestY);
        float distance = Vector2.Distance(Transform.position, closestPoint);

        // Consider the circle's radius in the collision check
        return distance <= self.Radius;
    }

    private void Update()
    {
        _renderer.color = GyzmoColor;
    }
}