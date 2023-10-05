using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detecciondecolisionpuntoyrectangulo : MonoBehaviour
{
    public Vector2 rectanglePosition; // Posición del rectángulo
    public Vector2 rectangleSize;     // Tamaño del rectángulo
    public Vector2 point;             // Posición del punto a verificar
    public SpriteRenderer spriteRenderer;
    public CustomColliderBox2D cubito;
    // Función para detectar la colisión entre un punto y un rectángulo
    bool PointRectCollision(CustomColliderBox2D cubito)
    {

        float rectLeft = cubito.transform.position.x - cubito.HalfScale.x;
        float rectRight = cubito.transform.position.x + cubito.HalfScale.x;
        float rectTop = cubito.transform.position.y + cubito.HalfScale.y;
        float rectBottom = cubito.transform.position.y - cubito.HalfScale.y;

        if (point.x >= rectLeft && point.x <= rectRight && point.y >= rectBottom && point.y <= rectTop)
        {
            spriteRenderer.color = Color.grey;
            return true; // El punto está dentro del rectángulo
        }
        else
        {
            spriteRenderer.color = Color.white;
            return false; // El punto está fuera del rectángulo
        }
    }

    void Update()
    {
        bool collision = PointRectCollision(cubito);

        if (collision)
        {
            Debug.Log("El punto colisiona con el rectángulo.");
        }
        else
        {
            Debug.Log("El punto no colisiona con el rectángulo.");
        }
    }
}

}
