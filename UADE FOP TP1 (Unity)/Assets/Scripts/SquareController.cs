using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CustomColliderBox2D))]
public class SquareController : MonoBehaviour
{
    [SerializeField] private CustomColliderBase _customCollider;
    [SerializeField] private CustomColliderBase _otherCollider;
    public impulseforce fisicas;
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _customCollider.GyzmoColor = Color.blue;
        _customCollider.CheckCollision(_otherCollider);

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 direction = new Vector2(horizontalInput, verticalInput);

        if (Input.GetKeyDown(KeyCode.D))
        {
            fisicas.CustomAddForce(Vector2.right);
            Debug.Log("aca");
        }

        if(direction.x != 0 || direction.y != 0)
        {
            fisicas.CustomAddForce(direction.normalized * speed);
            Debug.Log("2");
        }
    }
}
