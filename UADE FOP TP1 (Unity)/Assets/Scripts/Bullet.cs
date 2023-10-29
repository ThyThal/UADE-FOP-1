using System.Collections;
using UnityEngine;

public class Bullet : CustomMonoBehaviour
{
    [SerializeField] private CustomPhysics _customPhysics;
    [SerializeField] private float _speed = 10;

    private void Awake()
    {
        if (_customPhysics == null) { _customPhysics = GetComponent<CustomPhysics>(); }
    }

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
            _customPhysics.ConstantForce(Vector2.up * _speed);
        }
    }

    public void Reset(Vector3 position)
    {
        transform.position = position;
        Spawn();
    }
}
