using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CustomColliderCircle2D))]
[RequireComponent(typeof(Fisicas))]
public class CircleController : CustomMonoBehaviour
{
    public Fisicas fisicas;
    public bool player = false;
    public float speed = 10f;
    public bool mruv = false;

    // Update is called once per frame
    void Update()
    {
        CustomCollider.GyzmoColor = Color.blue;
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
            if (mruv) fisicas.CustomAddForce(direction.normalized * speed);
            else fisicas.CustomDoForce(direction.normalized * speed);
        }
    }
}
