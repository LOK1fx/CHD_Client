using System.Collections;
using UnityEngine;
using LOK1game.Tools;

namespace LOK1game.Testers
{
    [RequireComponent(typeof(Player.Player))]
    public class PlayerDeathTester : MonoBehaviour
    {
        [SerializeField] private float _deathLength = 5f;

        [Space]
        [SerializeField] private GameObject _playerDeathCameraPrefab;

        private Player.Player _player;

        private bool _isDead;

        private void Awake()
        {
            _player = GetComponent<Player.Player>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (!_isDead)
                {
                    Coroutines.StartRoutine(DeathRoutine());
                }
            }
        }

        private IEnumerator DeathRoutine()
        {
            _isDead = true;

            var camera = Instantiate(_playerDeathCameraPrefab, transform.position, _player.PlayerMovement.DirectionTransform.rotation);

            gameObject.SetActive(false);

            yield return new WaitForSeconds(_deathLength);

            gameObject.SetActive(true);

            _isDead = false;

            Destroy(camera);
        }
    }
}