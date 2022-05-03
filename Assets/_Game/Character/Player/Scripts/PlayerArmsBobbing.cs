using UnityEngine;
using WeaponBobbing;

namespace LOK1game
{
    [RequireComponent(typeof(Player.Player))]
    public class PlayerArmsBobbing : Actor, IPawn
    {
        private Player.Player _player;

        [Header("Arms bobing")]
        [SerializeField] private float _armsOffsetOnActions = 0.05f;
        [SerializeField] private float _armsOffsetOnStartMovement = 0.05f;

        [SerializeField] private ArmsOffset _armsOffset;
        [SerializeField] private WeaponBobCSGO _armsBob;

        [SerializeField] float _crouchArmsBobMultiplier;
        [SerializeField] float _airArmsBobMultiplier;

        private float _defaultArmsBobMultiplier;

        private void Start()
        {
            _player = GetComponent<Player.Player>();

            _defaultArmsBobMultiplier = _armsBob.bobMultiplier;

            SubscribeToEvents();

            GetComponent<PlayerInputManager>().PawnInputs.Add(this);
        }

        private void OnEnable()
        {
            if(_player != null)
            {
                SubscribeToEvents();
            }
        }

        private void OnDisable()
        {
            if (_player != null)
            {
                UnsubscribeFromEvents();
            }
        }

        public void OnInput(object sender)
        {
            if (_player.PlayerState.OnGround)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    AddArmsMovementOffset(Vector3.back);
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    AddArmsMovementOffset(Vector3.right);
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    AddArmsMovementOffset(Vector3.forward);
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    AddArmsMovementOffset(Vector3.left);
                }
            }
        }

        private void AddArmsMovementOffset(Vector3 direction)
        {
            _armsOffset.AddTemp(direction * _armsOffsetOnStartMovement);
            _armsOffset.AddTemp(Vector3.down * _armsOffsetOnStartMovement);
        }

        private void OnJump()
        {
            _armsOffset.AddTemp(Vector3.up * _armsOffsetOnActions);
            _armsBob.bobMultiplier = _airArmsBobMultiplier;
        }

        private void OnLand()
        {
            _armsOffset.AddTemp(Vector3.down * _armsOffsetOnActions);

            if (_player.PlayerState.IsCrouching)
            {
                _armsBob.bobMultiplier = _crouchArmsBobMultiplier;
            }
            else
            {
                _armsBob.bobMultiplier = _defaultArmsBobMultiplier;
            }
        }

        private void OnStartCrouch()
        {
            _armsOffset.AddTemp(Vector3.down * _armsOffsetOnActions);
            _armsBob.bobMultiplier = _crouchArmsBobMultiplier;
        }

        private void OnStopCrouch()
        {
            _armsOffset.AddTemp(Vector3.up * _armsOffsetOnActions);

            if (_player.PlayerState.OnGround)
            {
                _armsBob.bobMultiplier = _defaultArmsBobMultiplier;
            }
            else
            {
                _armsBob.bobMultiplier = _airArmsBobMultiplier;
            }
        }

        protected override void SubscribeToEvents()
        {
            var playerMovement = _player.PlayerMovement;

            playerMovement.OnJump += OnJump;
            playerMovement.OnLand += OnLand;
            playerMovement.OnStartCrouch += OnStartCrouch;
            playerMovement.OnStopCrouch += OnStopCrouch;
        }

        protected override void UnsubscribeFromEvents()
        {
            var playerMovement = _player.PlayerMovement;

            playerMovement.OnJump -= OnJump;
            playerMovement.OnLand -= OnLand;
            playerMovement.OnStartCrouch -= OnStartCrouch;
            playerMovement.OnStopCrouch -= OnStopCrouch;
        }

        public void OnPocces(PlayerControllerBase sender)
        {
            throw new System.NotImplementedException();
        }
    }
}