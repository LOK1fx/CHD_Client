using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance
    {
        get => _instance;

        private set
        {
            if (_instance == null)
            {
                _instance = value;
            }
            else if (_instance != value)
            {
                Debug.LogWarning($"{nameof(T)} instane already exist!");

                Destroy(value);

                Debug.Log($"Duplicate of {nameof(T)} has been destroyed.");
            }
        }
    }

    private static T _instance;

    protected virtual void Awake()
    {
        Instance = this as T;
    }

    protected virtual void OnApplicationQuit()
    {
        _instance = null;

        Destroy(gameObject);
    }
}