using UnityEngine;

public class CustomColliderCircle2D : CustomColliderBase
{
    [SerializeField] public float Radius = 0.5f;
    [SerializeField] private Vector2 _collisionNormal;
    
    // Cache 
    private ICollider _otherCollider;
    // Chequeo de collisiones
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
        // Da la posicion de la colision
        var center = Transform.position;

        // Da color a la colision del circulo
        Gizmos.DrawWireSphere(center, Radius);
    }

    public void ResolveCircleCollision(CustomColliderCircle2D other)
    {
        // Calcula el vector entre los centros de los dos circulos
        Vector2 collisionVector = other.Position2D - Position2D;

        // Calcula la distancia entre los centros de los dos circulos
        float distance = collisionVector.magnitude;

        // Calcular la cantidad de superposicion

        float overlap = Radius + other.Radius - distance;
 
        // Si hay superposicion que separe los circulos

        if (overlap > 0)
        {
            collisionVector.Normalize();
            Vector2 separation = collisionVector * (overlap / 2);

            Transform.position -= new Vector3(separation.x, separation.y,0);
        }
    }
}
