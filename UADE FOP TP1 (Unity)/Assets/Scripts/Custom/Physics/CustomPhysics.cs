using UnityEngine;

public class CustomPhysics : MonoBehaviour
{
   
    public float Mass = 1;
    public float GravityScale = 0.0f;

    [SerializeField] private Vector2 _velocity;
    [SerializeField] private Vector2 _acceleration;
    private Vector2 _gravity = new Vector2(0, -9.8f); // Ajusta la gravedad segun tus necesidades

    void FixedUpdate()
    {
        _velocity += _acceleration * Time.deltaTime;
        transform.position += (Vector3)(_velocity * Time.deltaTime + (_acceleration * 0.5f * Time.deltaTime * Time.deltaTime));
        _acceleration = Vector2.zero;   
    }

    public void ApplyForce(Vector2 force)
    {
        _acceleration += force / Mass;
    }

    public void CustomDoForce(Vector2 force)
    {
        _acceleration = force / Mass;
        _velocity = _acceleration;
    }
    
    public void CustomAddImpulse(Vector2 impulse)
    {
        // Update velocity using the impulse
        _velocity += impulse / Mass;
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
