using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Movimientonave : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private float moviemientoHorizontal = 0f;

    [SerializeField] private float velocidadDeMovimiento;
    [SerializeField] private float suavizadoDeMovimiento;

    private Vector3 velocidad = Vector3.zero;
    private bool mirandoDerecha = true;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moviemientoHorizontal = Input.GetAxisRaw("Horizontal") * velocidadDeMovimiento;
    }

    private void FixedUpdate()
    {
        //aca se nueve xd
        Mover(moviemientoHorizontal * Time.fixedDeltaTime);
    }

    private void Mover(float mover)
    {
        Vector3 velocidadObjetivo = new Vector2(mover, rb2D.velocity.y);
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, velocidadObjetivo, ref velocidad, suavizadoDeMovimiento);

        if (mover > 0 && !mirandoDerecha)
        {
            //aca va a girar xd
            Girar();
        }

        else if(mover < 0 && mirandoDerecha)
        {
            //gira de nuevo
            Girar();
        }
    }

    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;  
    }
}
