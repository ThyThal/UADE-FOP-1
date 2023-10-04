using System;
using UnityEngine;

public class CustomMonoBehaviour : MonoBehaviour
{
    [SerializeField] private CustomColliderBase _customCollider;
    [SerializeField] private bool _static = false;
    public CustomColliderBase CustomCollider => _customCollider;
    public bool Static => _static;


    public virtual void Start()
    {
        GameManager.Instance.CustomMonoBehaviours.Add(this);
    }

    public virtual void OnDestroy()
    {
        GameManager.Instance.CustomMonoBehaviours.Remove(this);
    }
}