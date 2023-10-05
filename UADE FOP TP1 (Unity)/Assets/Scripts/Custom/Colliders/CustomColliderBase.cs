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
        Gizmos.color = GyzmoColor;
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

    protected bool CollisionCircleCircle(CustomColliderCircle2D self, CustomColliderCircle2D other)
    {
        return Vector2.Distance(self.Transform.position, other.Transform.position) < self.Radius + other.Radius;
    }


    protected bool CollisionCircleBox(CustomColliderCircle2D self, CustomColliderBox2D other)
    {
        float closestX = Mathf.Clamp(Transform.position.x, other.Transform.position.x - other.HalfScale.x, other.Transform.position.x + other.HalfScale.x);
        float closestY = Mathf.Clamp(Transform.position.y, other.Transform.position.y - other.HalfScale.y, other.Transform.position.y + other.HalfScale.y);

        Vector2 closestPoint = new Vector2(closestX, closestY);
        float distance = Vector2.Distance(Transform.position, closestPoint);
        
        return distance <= self.Radius;
    }

    private void Update()
    {
        _renderer.color = GyzmoColor;
    }
}