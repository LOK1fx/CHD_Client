using LOK1game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerArmsAnimations : Actor
    {
        private const string TRIGGER_JUMP = "Jump";
        private const string TRIGGER_DROP_WEAPON = "Drop";
        private const string TRIGGER_CLIMB = "Climb";
        private const string TRIGGER_OPEN_DOOR = "OpenDoor";

        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerWeapon _playerWeapon;
        [SerializeField] private PlayerVaulting _playerVaulting;

        [SerializeField] private Transform _animationCameraBone;
        [SerializeField] private Transform _animationCamera;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            SubscribeToEvents();
        }

        private void OnEnable()
        {
            SubscribeToEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        private void Update()
        {
            if (_animationCameraBone != null)
            {
                var rot = _animationCameraBone.localRotation;

                _animationCamera.localRotation = new Quaternion(rot.x, rot.z, rot.y, rot.w) * Quaternion.Euler(-90f, 0f, 0f);
            }
        }

        protected override void SubscribeToEvents()
        {
            if (_playerMovement && _playerWeapon && _playerVaulting)
            {
                _playerMovement.OnJump += OnJump;
                _playerMovement.OnLand += OnLand;

                _playerVaulting.OnVault += OnVault;

                _playerWeapon.OnKick += OnKick;
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            if (_playerMovement && _playerWeapon && _playerVaulting)
            {
                _playerMovement.OnJump -= OnJump;
                _playerMovement.OnLand -= OnLand;

                _playerVaulting.OnVault -= OnVault;

                _playerWeapon.OnKick -= OnKick;
            }
        }

        private void OnKick()
        {
            _animator.SetTrigger(TRIGGER_OPEN_DOOR);
        }

        private void OnVault(float height)
        {
            if(height > 1f)
            {
                _animator.SetTrigger(TRIGGER_CLIMB);
            }
        }

        private void OnLand()
        {
            
        }

        private void OnJump()
        {
            
        }
    }
}