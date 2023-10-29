using UnityEngine;

public abstract class CustomColliderBase : MonoBehaviour, ICollider
{
    public Color GyzmoColor = Color.yellow;

    [SerializeField] private Transform _transform;
    [SerializeField] private SpriteRenderer _renderer;

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
    // Colision entre cubo y cubo
    protected bool CollisionBoxBox(CustomColliderBox2D self, CustomColliderBox2D other)
    {
        return (self.Position2D.x - self.HalfScale.x < other.Position2D.x + other.HalfScale.x &&
                self.Position2D.x + self.HalfScale.x > other.Position2D.x - other.HalfScale.x &&
                self.Position2D.y - self.HalfScale.y < other.Position2D.y + other.HalfScale.y &&
                self.Position2D.y + self.HalfScale.y > other.Position2D.y - other.HalfScale.y );
    }

    protected bool CollisionCircleCircle(CustomColliderCircle2D self, CustomColliderCircle2D other)
    {
        return Vector2.Distance(self.Position2D, other.Position2D) < self.Radius + other.Radius;
    }

    // Colision entre cubo y circulo
    protected bool CollisionCircleBox(CustomColliderCircle2D self, CustomColliderBox2D other)
    {
        float closestX = Mathf.Clamp(Position2D.x, other.Position2D.x - other.HalfScale.x, other.Position2D.x + other.HalfScale.x);
        float closestY = Mathf.Clamp(Position2D.y, other.Position2D.y - other.HalfScale.y, other.Position2D.y + other.HalfScale.y);

        Vector2 closestPoint = new Vector2(closestX, closestY);
        float distance = Vector2.Distance(Position2D, closestPoint);
        
        return distance <= self.Radius;
    }

    private void Update()
    {
        _renderer.color = GyzmoColor;
    }
}