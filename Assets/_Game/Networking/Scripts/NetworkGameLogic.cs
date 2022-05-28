using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game.New.Networking
{
    public enum EServerLevelId : ushort
    {
        TestLevel = 1,
        ParkourLevel = 2,
    }

    public class NetworkGameLogic : Singleton<NetworkGameLogic>
    {
        public GameObject WorldPlayerPrefab => _worldPlayerPrefab;
        public GameObject LocalPlayerPrefab => _localPlayerPrefab;
        public EServerLevelId Level => _level;

        [Header("Prefabs")]
        [SerializeField] private GameObject _worldPlayerPrefab;
        [SerializeField] private GameObject _localPlayerPrefab;
        [SerializeField] private EServerLevelId _level;

        public void SetLevel(ushort levelId)
        {
            _level = (EServerLevelId)levelId;

            Debug.Log($"ServerLevel seted to {levelId} ({((EServerLevelId)levelId).ToString()})");

            LevelManager.LoadLevel(LevelManager.GetLevelData(levelId));
        }
    }
}