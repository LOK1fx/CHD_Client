using UnityEngine;
using LOK1game.Game.Events;
using LOK1game.New.Networking;

namespace LOK1game.UI
{
    [RequireComponent(typeof(Animator))]
    public class Hitmarker : MonoBehaviour
    {
        private const string TRIGGER_ON_HIT = "Hit";
        private const string TRIGGER_ON_CRIT_HIT = "CritHit";

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            EventManager.AddListener<OnPlayerHitCHD>(OnHit);
        }

        private void OnHit(OnPlayerHitCHD evt)
        {
            if(NetworkManager.Instance.Client.Id == evt.PlayerId) { return; }

            if (evt.Crit)
            {
                _animator.SetTrigger(TRIGGER_ON_CRIT_HIT);
            }
            else
            {
                _animator.SetTrigger(TRIGGER_ON_HIT);
            }
        }
    }
}