using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointcolision : MonoBehaviour
{
    public CustomColliderBase basecircle;
    public CustomColliderCircle2D circle2D;
    public CustomColliderBox2D cuadrado;

    private void Update()
    {
        pintcircle(circle2D);
        Squeret(cuadrado);
    }

    private void Squeret(CustomColliderBox2D cuadrado)
    {
        
    }

    private void pintcircle(CustomColliderCircle2D circle)
    {
        
    }

    
}


