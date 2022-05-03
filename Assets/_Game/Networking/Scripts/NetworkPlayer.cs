using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RiptideNetworking;

namespace LOK1game.New.Networking
{
    public class NetworkPlayer : MonoBehaviour
    {
        public static Dictionary<ushort, NetworkPlayer> List = new Dictionary<ushort, NetworkPlayer>();

        public ushort Id { get; private set; }
        public bool IsLocal { get; private set; }

        [SerializeField] private Transform _camTransform;

        private Interpolator _interpolator;
        private NetworkPlayerController _playerController;

        private string _username;

        private void Awake()
        {
            _interpolator = GetComponent<Interpolator>();
            _playerController = GetComponent<NetworkPlayerController>();
        }

        public static void Spawn(ushort id, string username, Vector3 position)
        {
            NetworkPlayer player;

            if(id == NetworkManager.Instance.Client.Id)
            {
                player = GetPlayerPrefab(NetworkGameLogic.Instance.LocalPlayerPrefab, position);
                player.IsLocal = true;
            }
            else
            {
                player = GetPlayerPrefab(NetworkGameLogic.Instance.WorldPlayerPrefab, position);
                player.IsLocal = false;
            }

            var name = $"Player {id} ({(string.IsNullOrEmpty(username) ? $"Guest {id}" : username)})";

            player.name = name;
            player.Id = id;
            player._username = username;

            List.Add(id, player);
        }

        private static NetworkPlayer GetPlayerPrefab(GameObject prefab, Vector3 position)
        {
            return Instantiate(prefab, position, Quaternion.identity).GetComponent<NetworkPlayer>();
        }

        private void Move(ushort tick, bool isTeleport, Vector3 newPosition, Vector3 forward, Vector3 position)
        {
            
            if (!IsLocal)
            {
                _camTransform.forward = forward;
                _interpolator.NewUpdate(tick, isTeleport, newPosition);
            }
            else
            {
                var serverState = new StatePayload()
                {
                    Tick = tick,
                    Position = position
                };

                _playerController.OnServerMovementState(serverState);
            }
        }

        private void OnDestroy()
        {
            List.Remove(Id);
        }

        #region Messages

        [MessageHandler((ushort)EServerToClientId.PlayerSpawned)]
        private static void SpawnPlayer(Message message)
        {
            Spawn(message.GetUShort(), message.GetString(), message.GetVector3());
        }

        [MessageHandler((ushort)EServerToClientId.PlayerMovement)]
        private static void PlayerMovement(Message message)
        {
            if(List.TryGetValue(message.GetUShort(), out var player))
            {
                player.Move(message.GetUShort(), message.GetBool(), message.GetVector3(), message.GetVector3(), message.GetVector3());
            }
        }

        #endregion
    }
}