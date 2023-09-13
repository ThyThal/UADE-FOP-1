using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CustomColliderBox2D))]
public class SquareController : MonoBehaviour
{
    [SerializeField] private CustomColliderBase _customCollider;
    [SerializeField] private CustomColliderBase _otherCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _customCollider.GyzmoColor = Color.blue;
        _customCollider.CheckCollision(_otherCollider);
    }
}
