using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detecciondecolisionpuntoyrectangulo : MonoBehaviour
{
    public Vector2 rectanglePosition; // Posici�n del rect�ngulo
    public Vector2 rectangleSize;     // Tama�o del rect�ngulo
    public Vector2 point;             // Posici�n del punto a verificar
    public SpriteRenderer spriteRenderer;
    public CustomColliderBox2D cubito;
    // Funci�n para detectar la colisi�n entre un punto y un rect�ngulo
    bool PointRectCollision(CustomColliderBox2D cubito)
    {

        float rectLeft = cubito.transform.position.x - cubito.HalfScale.x;
        float rectRight = cubito.transform.position.x + cubito.HalfScale.x;
        float rectTop = cubito.transform.position.y + cubito.HalfScale.y;
        float rectBottom = cubito.transform.position.y - cubito.HalfScale.y;

        if (point.x >= rectLeft && point.x <= rectRight && point.y >= rectBottom && point.y <= rectTop)
        {
            spriteRenderer.color = Color.grey;
            return true; // El punto est� dentro del rect�ngulo
        }
        else
        {
            spriteRenderer.color = Color.white;
            return false; // El punto est� fuera del rect�ngulo
        }
    }

    void Update()
    {
        bool collision = PointRectCollision(cubito);

        if (collision)
        {
            Debug.Log("El punto colisiona con el rect�ngulo.");
        }
        else
        {
            Debug.Log("El punto no colisiona con el rect�ngulo.");
        }
    }
}

}
