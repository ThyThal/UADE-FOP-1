using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCollision : MonoBehaviour
{
    public SpriteRenderer _spriteRenderer;

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = new Vector3(worldPosition.x, worldPosition.y, 0f);
        
        CheckCollision();
    }

    private void CheckCollision()
    {
        var collide = false;
        foreach (var item in GameManager.Instance.CustomMonoBehaviours)
        {
            ICollider collider = item.CustomCollider;
            var collision = false;

            switch (collider)
            {
                case CustomColliderBox2D boxCollider:
                    collision = PointRectangleCollision(boxCollider);
                    if (collision) collide = collision;
                    continue;

                case CustomColliderCircle2D circleCollider:
                    collision = PointCircleCollision(circleCollider);
                    if (collision) collide = collision;
                    continue;

                default:
                    continue;
            }
        }

        if (collide)
        {
            _spriteRenderer.color = Color.grey;
        }
        else
        {
            _spriteRenderer.color = Color.white;
        }
    }
    
    private bool PointRectangleCollision(CustomColliderBox2D box)
    {
        float rectLeft = box.transform.position.x - box.HalfScale.x;
        float rectRight = box.transform.position.x + box.HalfScale.x;
        float rectTop = box.transform.position.y + box.HalfScale.y;
        float rectBottom = box.transform.position.y - box.HalfScale.y;

        if (transform.position.x >= rectLeft && transform.position.x <= rectRight && transform.position.y >= rectBottom && transform.position.y <= rectTop)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool PointCircleCollision(CustomColliderCircle2D circle)
    {
        float deltaX = transform.position.x - circle.transform.position.x;
        float deltaY = transform.position.y - circle.transform.position.y;
        float distance = Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY);

        if (distance <= circle.Radius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}


