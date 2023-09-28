using System;
using UnityEngine;

public class CustomMonoBehaviour : MonoBehaviour
{
    [SerializeField] private CustomColliderBase _customCollider;

    public CustomColliderBase CustomCollider => _customCollider;

    public virtual void Start()
    {
        GameManager.Instance.CustomMonoBehaviours.Add(this);
    }

    public virtual void OnDestroy()
    {
        GameManager.Instance.CustomMonoBehaviours.Remove(this);
    }
}