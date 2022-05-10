using LOK1game.Tools;
using System;
using System.Collections;
using UnityEngine;

namespace LOK1game.Player
{
    [RequireComponent(typeof(PlayerCamera), typeof(PlayerWeapon), typeof(PlayerMovement))]
    public class Player : Pawn, IDamagable
    {
        #region Events

        public event Action<int> OnTakeDamage;

        #endregion

        public PlayerCamera PlayerCamera { get; private set; }
        public PlayerWeapon PlayerWeapon { get; private set; }
        public PlayerMovement PlayerMovement { get; private set; }
        public PlayerState PlayerState { get; private set; }

        public int Hp { get; private set; }

        [SerializeField] private int _maxHp = 100;
        [SerializeField] private float _deathLength = 5f;

        [Space]
        [SerializeField] private GameObject _playerDeathCameraPrefab;

        private bool _isDead;

        private PlayerArmsBobbing _armsBobbing;

        [SerializeField] private float _eyeHeight = 1.5f;
        [SerializeField] private float _crouchEyeHeight = 1.1f;

        private void Awake()
        {       
            PlayerCamera = GetComponent<PlayerCamera>();
            PlayerWeapon = GetComponent<PlayerWeapon>();
            PlayerMovement = GetComponent<PlayerMovement>();
            PlayerState = GetComponent<PlayerState>();

            _armsBobbing = GetComponent<PlayerArmsBobbing>();

            SubscribeToEvents();

            GetComponent<PlayerInputManager>().PawnInputs.Add(this);
        }

        private void Start()
        {
            PlayerCamera.DesiredPosition = Vector3.up * _eyeHeight;

            Hp = _maxHp;
        }

        private void Update()
        {
            PlayerMovement.DirectionTransform.rotation = Quaternion.Euler(0f, PlayerCamera.GetCameraTransform().eulerAngles.y, 0f);
        }

        public override void OnInput(object sender)
        {
            PlayerMovement.SetAxisInput(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));

            if (Input.GetButtonDown("Jump"))
            {
                PlayerMovement.Jump();
            }
            if(Input.GetKeyDown(KeyCode.LeftControl) && PlayerState.OnGround)
            {
                PlayerMovement.StartCrouch();
            }
            if(Input.GetKeyUp(KeyCode.LeftControl) && PlayerState.IsCrouching)
            {
                PlayerMovement.StopCrouch();
            }
        }

        private void OnJump()
        {
            PlayerCamera.AddCameraOffset(Vector3.up * 0.15f);
            PlayerCamera.TriggerRecoil(new Vector3(1f, 0f, 0f));

            if(PlayerState.IsCrouching)
            {
                PlayerMovement.StopCrouch();
            }
        }

        private void OnLand()
        {
            var velocity = Vector3.ClampMagnitude(PlayerMovement.Rigidbody.velocity, 1f);

            PlayerCamera.AddCameraOffset(Vector3.down * 0.1f);
            PlayerCamera.TriggerRecoil(new Vector3(-1.4f, 0f, 1.3f) * velocity.y);
        }

        private void OnStartCrouch()
        {
            PlayerCamera.AddCameraOffset(Vector3.down * 0.1f);
            PlayerCamera.DesiredPosition = new Vector3(0f, _crouchEyeHeight, 0f);
        }

        private void OnStartSlide()
        {
            PlayerCamera.AddCameraOffset(-Vector3.forward * 0.1f);
            PlayerCamera.Tilt = -3f;
        }

        private void OnStopCrouch()
        {
            PlayerCamera.AddCameraOffset(Vector3.up * 0.1f);
            PlayerCamera.Tilt = 0f;
            PlayerCamera.DesiredPosition = new Vector3(0f, _eyeHeight, 0f);
        }

        protected override void SubscribeToEvents()
        {
            PlayerMovement.OnJump += OnJump;
            PlayerMovement.OnLand += OnLand;
            PlayerMovement.OnStartCrouch += OnStartCrouch;
            PlayerMovement.OnStopCrouch += OnStopCrouch;
            PlayerMovement.OnStartSlide += OnStartSlide;
        }

        protected override void UnsubscribeFromEvents()
        {
            PlayerMovement.OnJump -= OnJump;
            PlayerMovement.OnLand -= OnLand;
            PlayerMovement.OnStartCrouch -= OnStartCrouch;
            PlayerMovement.OnStopCrouch -= OnStopCrouch;
            PlayerMovement.OnStartSlide -= OnStartSlide;
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        private void OnEnable()
        {
            SubscribeToEvents();
        }

        public override void OnPocces(PlayerControllerBase sender)
        {
            
        }

        public void TakeDamage(Damage damage)
        {
            Hp -= damage.Value;

            if(Hp <= 0)
            {
                Coroutines.StartRoutine(DeathRoutine());
            }
        }

        public IEnumerator DeathRoutine()
        {
            _isDead = true;

            var camera = Instantiate(_playerDeathCameraPrefab, transform.position, PlayerMovement.DirectionTransform.rotation);

            gameObject.SetActive(false);

            yield return new WaitForSeconds(_deathLength);

            gameObject.SetActive(true);

            _isDead = false;

            Destroy(camera);
        }
    }
}