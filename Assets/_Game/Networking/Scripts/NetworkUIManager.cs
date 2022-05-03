using RiptideNetworking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace LOK1game.New.Networking
{
    public class NetworkUIManager : MonoBehaviour
    {
        private static NetworkUIManager _instance;

        public static NetworkUIManager Instance
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
                    Debug.LogWarning($"{nameof(NetworkUIManager)} instane already exist!");

                    Destroy(value);

                    Debug.Log($"Duplicate of {nameof(NetworkUIManager)} has been destroyed.");
                }
            }
        }

        [SerializeField] private Text _pingText;

        [Header("Connect")]
        [SerializeField] private GameObject _fieldsPanel;
        [SerializeField] private GameObject _connectingPanel;
        [SerializeField] private InputField _ipField;
        [SerializeField] private InputField _usernameField;

        [Space]
        [SerializeField] private Text _logText;

        private Stopwatch _pingWatch;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            NetworkManager.Instance.Client.Connected += OnConnected;
            NetworkManager.Instance.Client.Disconnected += OnDisconnected;

            _ipField.text = Constants.Network.LOCAL_HOST;
            _pingWatch = new Stopwatch();
        }

        public void ConnectClicked()
        {
            _usernameField.interactable = false;
            _ipField.interactable = false;
            _fieldsPanel.SetActive(false);
            _connectingPanel.SetActive(true);

            if(!string.IsNullOrEmpty(_ipField.text))
            {
                NetworkManager.Instance.SetIp(_ipField.text);
            }
            else
            {
                _logText.text = "IPField cannot be empty!";
            }
            
            NetworkManager.Instance.Connect();
        }

        private void OnConnected(object sender, System.EventArgs e)
        {
            _fieldsPanel.SetActive(false);
            _connectingPanel.SetActive(false);

            StartCoroutine(PingRoutine());
        }

        private void OnDisconnected(object sender, System.EventArgs e)
        {
            _fieldsPanel.SetActive(true);
            _connectingPanel.SetActive(true);
        }

        public void BackToMain()
        {
            _usernameField.interactable = true;
            _ipField.interactable = true;
            _fieldsPanel.SetActive(true);
            _connectingPanel.SetActive(false);
        }

        public void SendName()
        {
            var message = Message.Create(MessageSendMode.reliable, (ushort)EClientToServerId.Name);

            message.AddString(_usernameField.text);

            NetworkManager.Instance.Client.Send(message);
        }

        [MessageHandler((ushort)EServerToClientId.Pong)]
        private static void Pong(Message message)
        {
            Instance._pingWatch.Stop();

            Instance._pingText.text = $"Ping: {Instance._pingWatch.ElapsedMilliseconds}ms";
        }

        private IEnumerator PingRoutine()
        {
            while(true)
            {
                var message = Message.Create(MessageSendMode.reliable, (ushort)EClientToServerId.Ping);

                message.AddString("It's just a ping, bro");

                _pingWatch.Restart();

                NetworkManager.Instance.Client.Send(message);

                yield return new WaitForSeconds(3f);
            }
        }
    }
}