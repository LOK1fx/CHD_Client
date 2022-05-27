using LOK1game.Player;
using LOK1game.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game.Characters.Generic
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class CharacterFootsteps : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> _clips = new List<AudioClip>();

        [Space]
        [SerializeField] private float _maxDistanceBetweenSteps = 1f;
        [SerializeField] private float _minPitch = 0.9f;
        [SerializeField] private float _maxPitch = 1.1f;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private float _maxRaycastDistance = 1f;

        [Space]
        [SerializeField] private PlayerState _state;

        private AudioSource _source;
        private Vector3 _oldPosition;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }

        private void Start()
        {
            LeaveFootstep();
        }

        private void Update()
        {
            if(!_state.OnGround) { return; }

            if(Vector3.Distance(_oldPosition, transform.position) > _maxDistanceBetweenSteps)
            {
                LeaveFootstep();
            }
        }

        private void LeaveFootstep()
        {
            if(Physics.Raycast(transform.position, -transform.up, out var hit, _maxRaycastDistance, _groundMask, QueryTriggerInteraction.Ignore))
            {
                _oldPosition = transform.position;
                transform.position = hit.point + (Vector3.up * 0.5f);

                _source.pitch = Audio.GetRandomPitch(_minPitch, _maxPitch);
                _source.PlayOneShot(Audio.GetRandomClip(_clips.ToArray()));
            }
        }
    }
}