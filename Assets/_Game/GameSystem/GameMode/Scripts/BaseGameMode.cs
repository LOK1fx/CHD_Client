using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGameMode : IGameMode
{
    public List<GameObject> GameModeSpawnedObjects { get; private set; }

    private bool _isGameModeObjectListInitialized;

    public GameObject UiPrefab;
    public GameObject CameraPrefab;
    public GameObject PlayerPrefab;
    public GameObject PlayerController;

    public abstract IEnumerator OnEnd();

    public abstract IEnumerator OnStart();

    protected void RegisterGameModeObject(GameObject gameObject)
    {
        if(!_isGameModeObjectListInitialized)
        {
            GameModeSpawnedObjects = new List<GameObject>();

            _isGameModeObjectListInitialized = true;
        }

        GameModeSpawnedObjects.Add(gameObject);

        Object.DontDestroyOnLoad(gameObject);
    }

    protected IEnumerator DestroyAllGameModeObjects()
    {
        foreach (var obj in GameModeSpawnedObjects)
        {
            GameModeSpawnedObjects.Remove(obj);
            Object.Destroy(obj);

            yield return new WaitForEndOfFrame();
        }
    }
}