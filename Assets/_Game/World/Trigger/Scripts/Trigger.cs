using UnityEngine;
using UnityEngine.Events;

namespace LOK1game
{
    [RequireComponent(typeof(Collider))]
    public class Trigger : MonoBehaviour
    {
        public UnityEvent<Player.Player> OnTriggerEnterEvent;
        public UnityEvent<Player.Player> OnTriggerExitEvent;

        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();

            if (!_collider.isTrigger)
            {
                Debug.LogWarning($"{name} collider is not trigger!!!");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player.Player>(out var player))
            {
                OnTriggerEnterEvent?.Invoke(player);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Player.Player>(out var player))
            {
                OnTriggerEnterEvent?.Invoke(player);
            }
        }
    }
}