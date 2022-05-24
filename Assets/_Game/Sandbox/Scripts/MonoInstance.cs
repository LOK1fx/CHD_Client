using UnityEngine;

public class MonoInstance<T> where T : MonoBehaviour
{
    public T Instance
    {
        get => Instance;

        private set
        {
            if (Instance == null)
            {
                Instance = value;
            }
            else if (Instance != value)
            {
                Debug.LogWarning($"{nameof(T)} instane already exist!");

                GameObject.Destroy(value);

                Debug.Log($"Duplicate of {nameof(T)} has been destroyed.");
            }
        }
    }
}