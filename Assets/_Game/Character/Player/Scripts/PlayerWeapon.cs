using LOK1game.Weapon;
using System;
using UnityEngine;

namespace LOK1game.Player
{
    [RequireComponent(typeof(Player))]
    public class PlayerWeapon : Actor, IPawn
    {
        #region Events

        public event Action<WeaponData> OnEquip;
        public event Action<WeaponData> OnDequip;
        public event Action OnKick;

        #endregion

        [SerializeField] private PlayerHand[] _playerHands = new PlayerHand[2];
        public PlayerHand[] PlayerHands => _playerHands;

        public bool HasGun { get; private set; }
        public int CurrentSlotIndex { get; private set; }

        [SerializeField] private Animator _armsAnimator;

        private RuntimeAnimatorController _defaultAnimatorController;

        private Player _player;

        private void Start()
        {
            if(_playerHands.Length > 2)
            {
                throw new Exception("Player hands is not valid: Length > 2");
            }

            _player = GetComponent<Player>();
            _defaultAnimatorController = _armsAnimator.runtimeAnimatorController;

            _player.Movement.OnJump += () => _armsAnimator.SetTrigger("Jump");

            GetComponent<PlayerInputManager>().PawnInputs.Add(this);
        }

        public void OnInput(object sender)
        {
            if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                OnKick?.Invoke();
            }

            if (HasGun)
            {
                HandleGunArmInput(PlayerHand.Side.Right); // Right hand
                HandleGunArmInput(PlayerHand.Side.Left); // Left hand

                if (Input.GetKeyDown(KeyCode.F))
                {
                    _armsAnimator.SetTrigger("Inspect");
                }
                if(Input.GetKeyDown(KeyCode.G))
                {
                    foreach (var hand in _playerHands)
                    {
                        if(hand.CurrentWeaponObject != null)
                        {
                            DropGun(hand.HandSide);
                        }
                    } 
                }
            }
        }

        private void HandleGunArmInput(PlayerHand.Side hand)
        {
            var index = hand == PlayerHand.Side.Right ? 0 : 1;

            if (_playerHands[index].CurrentWeaponData != null)
            {
                var gun = _playerHands[index].CurrentWeaponData;

                if(gun.BurstMode == GunBurstMode.Semi)
                {
                    if (Input.GetKeyDown(gun.UseKey))
                    {
                        Shoot(hand);
                    }
                }
                else
                {
                    if (Input.GetKey(gun.UseKey))
                    {
                        Shoot(hand);
                    }
                }
            }
        }

        public void Shoot(PlayerHand.Side side)
        {
            var hand = GetHandBySide(side);

            if(hand == null || hand.CurrentWeaponData == null || hand.CurrentWeaponObject == null) { return; }

            if(hand.CurrentWeapon.TryAtack(_player, hand))
            {
                _armsAnimator.Play("Atack", 0, 0);

                var weaponRecoil = hand.CurrentWeaponData.RecoilCameraRotation;

                _player.Camera.TriggerRecoil(weaponRecoil);
            }
        }

        public void Equip(WeaponData gunData, PlayerHand.Side side)
        {
            Dequip(side);

            var hand = GetHandBySide(side);

            if(hand.CurrentWeaponObject != null)
            {
                hand.ClearHand();
            }

            var weaponObject = Instantiate(gunData.GunPrefab.gameObject, hand.Socket.transform);

            var weapon = new WeaponStruct()
            {
                Data = gunData,
                GameObject = weaponObject,
                Weapon = weaponObject.GetComponent<IWeapon>()
            };

            hand.SetWeapon(weapon);
            weapon.Weapon.OnEquip(_player);

            HasGun = true;

            CurrentSlotIndex = (int)weapon.Data.Type - 1;

            _armsAnimator.runtimeAnimatorController = gunData.AnimatorController;
            _armsAnimator.Play("Draw", 0, 0);

            OnEquip?.Invoke(weapon.Weapon.GetData());
        }

        public void Dequip(PlayerHand.Side side)
        {
            var hand = GetHandBySide(side);

            if(hand.CurrentWeapon != null)
            {
                hand.CurrentWeapon.OnDequip(_player);

                HasGun = false;

                _armsAnimator.runtimeAnimatorController = _defaultAnimatorController;

                OnDequip?.Invoke(hand.CurrentWeaponData);

                hand.ClearHand();
            }
        }

        private void DropGun(PlayerHand.Side side)
        {
            Dequip(side);

            _armsAnimator.Play("Drop", 0, 0);
        }

        public PlayerHand GetHandBySide(PlayerHand.Side side)
        {
            foreach (var hand in _playerHands)
            {
                if(hand.HandSide == side)
                {
                    return hand;
                }
            }

            return null;
        }

        public void OnPocces(PlayerControllerBase sender)
        {
            throw new NotImplementedException();
        }
    }
}