using UnityEngine;

public class CustomPhysics : MonoBehaviour
{
    public float Mass = 1;
    public float GravityScale = 0.0f;

    [SerializeField] private float _friction = 0.1f; // Adjust the friction coefficient as needed
    [SerializeField] private Vector2 _frictionForce;
    [SerializeField] private Vector2 _velocity; // Current Object Direction
    [SerializeField] private Vector2 _acceleration; // Dessired Object Direction
    private Vector2 _gravity = new Vector2(0, -9.8f); // Adjust gravity as needed

    void FixedUpdate()
    {
        // Calculate friction force
        _frictionForce = -_velocity.normalized * _friction;

        // Update acceleration using gravity and friction
        _acceleration -= _gravity * GravityScale - _frictionForce;

        // Update velocity using acceleration and time
        _velocity += _acceleration * Time.deltaTime;

        // Update position using velocity and time
        transform.position += (Vector3)(_velocity * Time.deltaTime);

        // Reset acceleration for the next frame
        _acceleration = Vector2.zero;
    }

    public void AddForce(Vector2 force)
    {
        _acceleration += force / Mass;
    }

    public void ConstantForce(Vector2 force)
    {
        _acceleration = force / Mass;
        _velocity = _acceleration;
    }

    public void StopVelocity()
    {
        _velocity = Vector2.zero;
    }

    public void StopAcceleration()
    {
        _acceleration = Vector2.zero;
    }
}
