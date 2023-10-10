using UnityEngine;

[RequireComponent(typeof(CustomColliderCircle2D))]
[RequireComponent(typeof(CustomPhysics))]
public class CircleController : CustomMonoBehaviour
{
    public bool Player = false;
    public bool MRUV = false;
    public bool Stop = false;

    [SerializeField] private CustomPhysics _customPhysics;
    [SerializeField] private float _speed = 10f;

    private void Awake()
    {
        if (_customPhysics == null) { _customPhysics = GetComponent<CustomPhysics>(); }
    }

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

        if (!Player) return;
        
        if (direction == Vector2.zero && Stop) 
        {
            _customPhysics.StopVelocity();
            _customPhysics.StopAcceleration();
        }

        else if (direction.x != 0 || direction.y != 0)
        {
            if (MRUV) _customPhysics.ApplyForce(direction.normalized * _speed);
            else _customPhysics.CustomDoForce(direction.normalized * _speed);
        }
    }
}
