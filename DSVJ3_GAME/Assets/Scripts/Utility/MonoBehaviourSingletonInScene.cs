using UnityEngine;

public class MonoBehaviourSingletonInScene<T> : MonoBehaviour where T : Component
{
    private static T instance;

    public static T Get()
    {
        return instance;
    }

    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}