using UnityEngine;
using TMPro;

namespace LOK1game.New.Networking.UI
{
    public class NetworkHudStatusBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _playerNickname;
        [SerializeField] private Transform _playerHealthBar;

        private void Start()
        {
            NetworkPlayer.OnSpawned += OnPlayerSpawned;
            NetworkPlayer.OnDestroyed += OnPlayerDestroyed;
        }

        private void OnPlayerSpawned(ushort id)
        {
            Debug.Log($"Client id - {NetworkManager.Instance.Client.Id}");

            var clientPlayer = NetworkPlayer.List[NetworkManager.Instance.Client.Id];

            if (id != clientPlayer.Id) { return; }

            clientPlayer.OnHealthChanged += SetHealthBarValue;

            _playerNickname.text = clientPlayer.Username.ToString();
        }
        private void OnPlayerDestroyed(ushort id)
        {
            var clientPlayer = NetworkPlayer.List[NetworkManager.Instance.Client.Id];

            if(id != clientPlayer.Id) { return; }

            clientPlayer.OnHealthChanged -= SetHealthBarValue;
        }

        private void SetHealthBarValue(int value)
        {
            _playerHealthBar.localScale = new Vector3(value * 0.01f, 1f, 1f);
        }
    }
}