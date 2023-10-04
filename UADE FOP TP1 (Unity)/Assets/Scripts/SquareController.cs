using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CustomColliderBox2D))]
[RequireComponent(typeof(Fisicas))]
public class SquareController : CustomMonoBehaviour
{
    public Fisicas fisicas;
    public bool player = false;
    public float speed = 10f;

    // Update is called once per frame
    void Update()
    {
        //CustomCollider.GyzmoColor = Color.blue;
        if (!Static)
        {
            GameManager.Instance.CheckCollisions(CustomCollider);
        }

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(horizontalInput, verticalInput);

        if (!player) return;

        if (direction == Vector2.zero) 
        { 
            fisicas.Stop(); 
        }

        else if (direction.x != 0 || direction.y != 0)
        {
            fisicas.CustomAddForce(direction.normalized * speed);

        }
    }
}
