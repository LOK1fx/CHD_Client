using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LOK1game.New.Networking;

namespace LOK1game.UI
{
    public class HudStatusBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _playerNickname;
        [SerializeField] private Transform _playerHealthBar;

        private void Start()
        {
            New.Networking.NetworkPlayer.OnDestroyed += OnPlayerDestroyed;
            New.Networking.NetworkPlayer.OnSpawned += OnPlayerSpawned;
        }

        private void OnPlayerSpawned(int id)
        {
            var clientPlayer = New.Networking.NetworkPlayer.List[NetworkManager.Instance.Client.Id];

            clientPlayer.OnHealthChanged += SetHealthBarValue;

            _playerNickname.text = clientPlayer.Username.ToString();
        }
        private void OnPlayerDestroyed(int id)
        {
            var clientPlayer = New.Networking.NetworkPlayer.List[NetworkManager.Instance.Client.Id];

            clientPlayer.OnHealthChanged -= SetHealthBarValue;
        }

        private void SetHealthBarValue(int value)
        {
            _playerHealthBar.localScale = new Vector3(value * 0.01f, 1f, 1f);
        }
    }
}