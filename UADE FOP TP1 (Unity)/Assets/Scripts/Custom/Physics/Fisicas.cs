using UnityEngine;

public class Fisicas : MonoBehaviour
{
   
    public float mass = 1;
    public Vector2 velocity;
    //public int a�adirFuerza;

    public float gravityScale = 0.0f;

    private Vector2 gravity = new Vector2(0, -5.8f); // Ajusta la gravedad seg�n tus necesidades

    void FixedUpdate()
    {
        // Update the position based on the velocity
        transform.position += (Vector3)velocity * Time.fixedDeltaTime;        
    }   

    public void CustomAddForce(Vector2 force)
    {
        // Calculate acceleration using F = ma
        Vector2 acceleration = force / mass;
        // Update velocity using the calculated acceleration
        velocity += acceleration * Time.fixedDeltaTime;
        
    }

    public void CustomDoForce(Vector2 force)
    {
        Vector2 acceleration = force / mass;
        velocity = acceleration;
    }
    
    public void CustomAddImpulse(Vector2 impulse)
    {
        // Update velocity using the impulse
        velocity += impulse / mass;
    }

    public void Stop()
    {
        velocity = Vector2.zero;
    }
}
