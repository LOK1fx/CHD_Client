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
    None,
    Default,
    CrystalCapture,
    PVE,
}
namespace LOK1game.Game
{

    [Serializable]
    public abstract class BaseGameMode : MonoBehaviour, IGameMode
    {
        public EGameModeState State { get; protected set; }
        public List<GameObject> GameModeSpawnedObjects { get; private set; }

        public EGameModeId Id => _id;
        public GameObject UiPrefab => _uiPrefab;
        public GameObject CameraPrefab => _cameraPrefab;
        public GameObject PlayerPrefab => _playerPrefab;
        public GameObject PlayerController => _playerController;

        [SerializeField] private EGameModeId _id = EGameModeId.None;
        [SerializeField] private GameObject _uiPrefab;
        [SerializeField] private GameObject _cameraPrefab;
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _playerController;

        private bool _isGameModeObjectListInitialized;


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

            DontDestroyOnLoad(gameObject);
        }

        protected IEnumerator DestroyAllGameModeObjects()
        {
            foreach (var obj in GameModeSpawnedObjects)
            {
                Destroy(obj);

                yield return new WaitForEndOfFrame();
            }
        }
    }
}