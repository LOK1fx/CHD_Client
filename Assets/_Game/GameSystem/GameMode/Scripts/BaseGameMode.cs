using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LOK1game.Weapon;

public enum EGameModeState
{
    Started,
    Starting,
    Ending,
    Ended
}

[Serializable]
public abstract class BaseGameMode : IGameMode
{
    public EGameModeState State { get; protected set; }
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

        GameObject.DontDestroyOnLoad(gameObject);
    }

    protected IEnumerator DestroyAllGameModeObjects()
    {
        foreach (var obj in GameModeSpawnedObjects)
        {
            GameModeSpawnedObjects.Remove(obj);
            GameObject.Destroy(obj);

            yield return new WaitForEndOfFrame();
        }
    }
}