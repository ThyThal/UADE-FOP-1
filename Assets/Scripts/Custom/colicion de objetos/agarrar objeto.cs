using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgarrarObjeto : MonoBehaviour
{
    private GameObject objetoAgarrado;
    private Vector2 posicionInicialObjeto;

    void Update()
    {
        if (objetoAgarrado == null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 1f);

                if (hit.collider != null)
                {
                    objetoAgarrado = hit.collider.gameObject;
                    posicionInicialObjeto = objetoAgarrado.transform.position;
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                objetoAgarrado = null;
            }
            objetoAgarrado.transform.position = new Vector2(transform.position.x, transform.position.y + 1f);
        }
    }
}


