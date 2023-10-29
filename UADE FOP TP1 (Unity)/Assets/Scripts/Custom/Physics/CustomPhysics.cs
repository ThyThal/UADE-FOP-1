using UnityEngine;

public class CustomPhysics : MonoBehaviour
{
    public float Mass = 1;
    public float GravityScale = 0.0f;

    [SerializeField] private float _friction = 0.1f; // Ajuste la friccion segun sea necesario
    [SerializeField] private Vector2 _frictionForce;
    [SerializeField] private Vector2 _velocity; // Direccion del objetivo actual
    [SerializeField] private Vector2 _acceleration; // Dirreccion deseada
    private Vector2 _gravity = new Vector2(0, -9.8f); // Ajustae la gravedad segun sea necesario

    void FixedUpdate()
    {
        // Calcular la fuerza de friccion
        _frictionForce = -_velocity.normalized * _friction;

        // Actualiza la aceleracion usando la gravedad y la friccion 
        _acceleration -= _gravity * GravityScale - _frictionForce;

        // Actualiza la velocidad usando aceleracion y tiempo
        _velocity += _acceleration * Time.deltaTime;

        // Actualiza la posicion usando velocidad y tiempo
        transform.position += (Vector3)(_velocity * Time.deltaTime);

        // Resetea la aceleracion 
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
