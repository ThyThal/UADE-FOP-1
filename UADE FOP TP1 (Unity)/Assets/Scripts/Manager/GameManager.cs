using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Pool _pool;
    [SerializeField] private List<CustomMonoBehaviour> _customMonoBehaviours = new();

    public List<CustomMonoBehaviour> CustomMonoBehaviours => _customMonoBehaviours;

    // Game Manager Instance
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void CheckCollisions(CustomColliderBase myCollider)
    {
        for (int i = CustomMonoBehaviours.Count - 1; i >= 0; i--)
        {            
            var otherObject = CustomMonoBehaviours[i];
            if (otherObject.gameObject == myCollider.gameObject) continue;

            if (otherObject.isActiveAndEnabled)
            {
                if (otherObject != myCollider.gameObject)
                {
                    if (myCollider.CheckCollision(otherObject.CustomCollider));
                }
            }
        }
    }
}
