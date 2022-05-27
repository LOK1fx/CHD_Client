using UnityEngine;
using LOK1game.Game.Events;
using LOK1game.New.Networking;
using System.Collections.Generic;
using LOK1game.Tools;

namespace LOK1game.UI
{
    [RequireComponent(typeof(Animator), typeof(AudioSource))]
    public class Hitmarker : MonoBehaviour
    {
        private const string TRIGGER_ON_HIT = "Hit";
        private const string TRIGGER_ON_CRIT_HIT = "CritHit";

        [SerializeField] private float _maxRotationAngleOnHit = 5f;
        [SerializeField] private List<AudioClip> _clips = new List<AudioClip>();

        private Animator _animator;
        private AudioSource _source;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _source = GetComponent<AudioSource>();
        }

        private void Start()
        {
            EventManager.AddListener<OnPlayerHitCHD>(OnHit);
        }

        private void OnHit(OnPlayerHitCHD evt)
        {
            if(NetworkManager.Instance != null)
            {
                if (NetworkManager.Instance.Client.Id == evt.PlayerId) { return; }
            }

            var angle = Random.Range(-_maxRotationAngleOnHit, _maxRotationAngleOnHit);

            transform.localRotation = Quaternion.Euler(0f, 0f, angle);
            transform.position = Camera.main.WorldToScreenPoint(evt.HitPosition);


            _animator.SetTrigger(TRIGGER_ON_HIT);
            _source.pitch = Audio.GetRandomPitch(0.9f, 1.1f);
            _source.PlayOneShot(Audio.GetRandomClip(_clips.ToArray()));
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener<OnPlayerHitCHD>(OnHit);
        }
    }
}