using LOK1game.Tools;
using System;
using System.Collections;
using UnityEngine;
using LOK1game.Weapon;

namespace LOK1game.Player
{
    [RequireComponent(typeof(PlayerCamera), typeof(PlayerWeapon), typeof(PlayerMovement))]
    public class Player : Pawn
    {
        public PlayerCamera Camera { get; private set; }
        public PlayerWeapon Weapon { get; private set; }
        public PlayerWeaponInventory WeaponInventory { get; private set; }
        public PlayerMovement Movement { get; private set; }
        public PlayerState State { get; private set; }

        [Space]
        [SerializeField] private GameObject _playerDeathCameraPrefab;
        [SerializeField] private float _deathLength = 5f;

        private PlayerArmsBobbing _armsBobbing;

        [SerializeField] private float _eyeHeight = 1.5f;
        [SerializeField] private float _crouchEyeHeight = 1.1f;

        private void Awake()
        {       
            Camera = GetComponent<PlayerCamera>();
            Weapon = GetComponent<PlayerWeapon>();
            WeaponInventory = GetComponent<PlayerWeaponInventory>();
            Movement = GetComponent<PlayerMovement>();
            State = GetComponent<PlayerState>();

            _armsBobbing = GetComponent<PlayerArmsBobbing>();

            SubscribeToEvents();

            GetComponent<PlayerInputManager>().PawnInputs.Add(this);
        }

        private void Start()
        {
            Camera.DesiredPosition = Vector3.up * _eyeHeight;

            if(WeaponInventory.PrimaryWeapon != null)
            {
                EquipWeapon(WeaponInventory.PrimaryWeapon);
            }
        }

        private void Update()
        {
            Movement.DirectionTransform.rotation = Quaternion.Euler(0f, Camera.GetCameraTransform().eulerAngles.y, 0f);
        }

        public override void OnInput(object sender)
        {
            Movement.SetAxisInput(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));

            if (Input.GetButtonDown("Jump"))
            {
                Movement.Jump();
            }
            if(Input.GetKeyDown(KeyCode.LeftControl) && State.OnGround)
            {
                Movement.StartCrouch();
            }
            if(Input.GetKeyUp(KeyCode.LeftControl) && State.IsCrouching)
            {
                Movement.StopCrouch();
            }

            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                EquipWeapon(WeaponInventory.PrimaryWeapon);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                EquipWeapon(WeaponInventory.SecondaryWeapon);
            }
            if(Input.GetKeyDown(KeyCode.Q))
            {
                EquipWeapon(WeaponInventory.Utility);
            }
            if (Input.GetKeyDown(WeaponInventory.Abilities[0].UseKey))
            {
                EquipWeapon(WeaponInventory.Abilities[0]);
            }
        }

        private void EquipWeapon(WeaponData data)
        {
            Weapon.Equip(data, data.Hand);
        }

        private void OnJump()
        {
            Camera.AddCameraOffset(Vector3.up * 0.15f);
            Camera.TriggerRecoil(new Vector3(1f, 0f, 0f));

            if(State.IsCrouching)
            {
                Movement.StopCrouch();
            }
        }

        private void OnLand()
        {
            var velocity = Vector3.ClampMagnitude(Movement.Rigidbody.velocity, 1f);

            Camera.AddCameraOffset(Vector3.down * 0.1f);
            Camera.TriggerRecoil(new Vector3(-1.4f, 0f, 1.3f) * velocity.y);
        }

        private void OnStartCrouch()
        {
            Camera.AddCameraOffset(Vector3.down * 0.1f);
            Camera.DesiredPosition = new Vector3(0f, _crouchEyeHeight, 0f);
        }

        private void OnStartSlide()
        {
            Camera.AddCameraOffset(-Vector3.forward * 0.1f);
            Camera.Tilt = -3f;
        }

        private void OnStopCrouch()
        {
            Camera.AddCameraOffset(Vector3.up * 0.1f);
            Camera.Tilt = 0f;
            Camera.DesiredPosition = new Vector3(0f, _eyeHeight, 0f);
        }

        protected override void SubscribeToEvents()
        {
            Movement.OnJump += OnJump;
            Movement.OnLand += OnLand;
            Movement.OnStartCrouch += OnStartCrouch;
            Movement.OnStopCrouch += OnStopCrouch;
            Movement.OnStartSlide += OnStartSlide;
        }

        protected override void UnsubscribeFromEvents()
        {
            Movement.OnJump -= OnJump;
            Movement.OnLand -= OnLand;
            Movement.OnStartCrouch -= OnStartCrouch;
            Movement.OnStopCrouch -= OnStopCrouch;
            Movement.OnStartSlide -= OnStartSlide;
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

        public IEnumerator DeathRoutine()
        {
            var camera = Instantiate(_playerDeathCameraPrefab, transform.position, Movement.DirectionTransform.rotation);

            gameObject.SetActive(false);

            yield return new WaitForSeconds(_deathLength);

            gameObject.SetActive(true);

            Destroy(camera);
        }
    }
}