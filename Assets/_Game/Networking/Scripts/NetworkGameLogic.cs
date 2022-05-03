using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game.New.Networking
{
    public class NetworkGameLogic : MonoBehaviour
    {
        private static NetworkGameLogic _instance;

        public static NetworkGameLogic Instance
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
                    Debug.LogWarning($"{nameof(NetworkGameLogic)} instane already exist!");

                    Destroy(value);

                    Debug.Log($"Duplicate of {nameof(NetworkGameLogic)} has been destroyed.");
                }
            }
        }

        public GameObject WorldPlayerPrefab => _worldPlayerPrefab;
        public GameObject LocalPlayerPrefab => _localPlayerPrefab;

        [Header("Prefabs")]
        [SerializeField] private GameObject _worldPlayerPrefab;
        [SerializeField] private GameObject _localPlayerPrefab;

        private void Awake()
        {
            Instance = this;
        }
    }
}