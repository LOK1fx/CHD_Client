using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EGameModeState : ushort
{
    Starting = 1,
    Started,
    Ending,
    Ended,
}

public enum EGameModeId : ushort
{
    Default,
    CrystalCapture,
    PVE,
}
namespace LOK1game.Game
{

    [Serializable]
    public abstract class BaseGameMode : IGameMode
    {
        public EGameModeState State { get; protected set; }
        public List<GameObject> GameModeSpawnedObjects { get; private set; }

        private bool _isGameModeObjectListInitialized;
        protected EGameModeId _id;

        public GameObject UiPrefab;
        public GameObject CameraPrefab;
        public GameObject PlayerPrefab;
        public GameObject PlayerController;

        public EGameModeId Id => _id;
        public abstract IEnumerator OnEnd();
        public abstract IEnumerator OnStart();

        protected void RegisterGameModeObject(GameObject gameObject)
        {
            if (!_isGameModeObjectListInitialized)
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
                GameObject.Destroy(obj);

                yield return new WaitForEndOfFrame();
            }
        }
    }
}