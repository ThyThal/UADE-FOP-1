using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : CustomMonoBehaviour
{
    public Fisicas fisicas;
    public float speed = 10;

    void Spawn()
    {
        StartCoroutine(WaitForDisable());
    }

    IEnumerator WaitForDisable()
    {
        yield return new WaitForSeconds (5);
        DestroyBullet();

    }

    public void DestroyBullet()
    {
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (!Static)
        {
            fisicas.CustomDoForce(Vector2.up * speed);
        }
    }

    public void Reset(Vector3 position)
    {
        transform.position = position;
        Spawn();
    }
}
