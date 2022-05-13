using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RiptideNetworking;
using LOK1game.Tools;
using System;
using LOK1game.Game.Events;

namespace LOK1game.New.Networking
{
    public class NetworkPlayer : MonoBehaviour, IDamagable
    {
        #region Events

        public event Action<int> OnHealthChanged;
        public static event Action<int> OnSpawned;
        public static event Action<int> OnDestroyed;

        #endregion

        public static Dictionary<ushort, NetworkPlayer> List = new Dictionary<ushort, NetworkPlayer>();

        public ushort Id { get; private set; }
        public string Username { get; private set; }
        public bool IsLocal { get; private set; }
        public int Hp { get; private set; }

        [SerializeField] private Transform _camTransform;
        [SerializeField] private GameObject _visuals;

        private Interpolator _interpolator;
        private NetworkPlayerController _playerController;
        private Player.Player _player;


        private void Awake()
        {
            _interpolator = GetComponent<Interpolator>();
            _playerController = GetComponent<NetworkPlayerController>();
        }

        private void Start()
        {
            Hp = 100;
        }

        public static void Spawn(ushort id, string username, Vector3 position, bool respawn)
        {
            if(respawn)
            {
                if (List.ContainsKey(id))
                {
                    return;
                }
            }

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

            DontDestroyOnLoad(player.gameObject);

            var name = $"Player {id} ({(string.IsNullOrEmpty(username) ? $"Guest {id}" : username)})";

            player.name = name;
            player.Id = id;
            player.Username = username;

            if (player.IsLocal)
            {
                player._player = player.gameObject.GetComponent<Player.Player>();
            }

            List.Add(id, player);

            OnSpawned?.Invoke(id);
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

        //local
        public void TakeDamage(Damage damage)
        {
            var message = Message.Create(MessageSendMode.reliable, (ushort)EClientToServerId.HitPlayer);

            message.AddUShort(Id);
            message.AddInt(damage.Value);

            NetworkManager.Instance.Client.Send(message);
        }

        private void Death(ushort killer)
        {
            if(!IsLocal)
            {
                OnDestroyed?.Invoke(Id);
                List.Remove(Id);
                Destroy(gameObject);
            }
            else
            {
                Coroutines.StartRoutine(_player.DeathRoutine());
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
            Spawn(message.GetUShort(), message.GetString(), message.GetVector3(), message.GetBool());
        }

        [MessageHandler((ushort)EServerToClientId.PlayerMovement)]
        private static void PlayerMovement(Message message)
        {
            if(List.TryGetValue(message.GetUShort(), out var player))
            {
                player.Move(message.GetUShort(), message.GetBool(), message.GetVector3(), message.GetVector3(), message.GetVector3());
            }
        }

        [MessageHandler((ushort)EServerToClientId.PlayerHealth)]
        private static void PlayerHealth(Message message)
        {
            var id = message.GetUShort();
            var health = message.GetInt();

            List[id].Hp = health;
            List[id].OnHealthChanged?.Invoke(health);
        }

        [MessageHandler((ushort)EServerToClientId.PlayerHited)]
        private static void PlayerHit(Message message)
        {
            var id = message.GetUShort();
            var damage = new Damage(message.GetInt());

            List[id].Hp -= damage.Value;

            var evt = Events.OnPlayerHit;

            evt.PlayerId = id;
            evt.Crit = false;

            EventManager.Broadcast(evt);
        }

        [MessageHandler((ushort)EServerToClientId.PlayerDeath)]
        private static void PlayerDeath(Message message)
        {
            var id = message.GetUShort();
            var killer = message.GetUShort();

            List[id].Death(killer);
        }

        #endregion
    }
}