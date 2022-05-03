using LOK1game.Weapon;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game.Player
{
    [RequireComponent(typeof(Player))]
    public class PlayerWeapon : Actor, IPawn
    {
        public event Action<BaseGun> OnEquip;
        public event Action<GunData> OnDequip;
        public event Action OnKick;

        [SerializeField] private PlayerHand[] _playerHands = new PlayerHand[2];
        public PlayerHand[] PlayerHands => _playerHands;

        public bool HasGun { get; private set; }

        public List<GunData> WeaponInventory = new List<GunData>();

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

            if(WeaponInventory.Count > 0 && WeaponInventory[0] != null)
            {
                Equip(WeaponInventory[0], WeaponInventory[0].Hand);

                if(WeaponInventory.Count > 1 && WeaponInventory[0].Hand == PlayerHand.Side.Left)
                {
                    Equip(WeaponInventory[1], WeaponInventory[1].Hand);
                }
            }

            _player.PlayerMovement.OnJump += () => _armsAnimator.SetTrigger("Jump");

            GetComponent<PlayerInputManager>().PawnInputs.Add(this);
        }

        public void OnInput(object sender)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (WeaponInventory.Count > 0)
                {
                    Equip(WeaponInventory[0], WeaponInventory[0].Hand);
                }
            }
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
                        if(hand.CurrentGunObject != null)
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

            if (_playerHands[index].CurrentGun != null)
            {
                var gun = _playerHands[index].CurrentGun;

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

            if(hand == null || hand.CurrentGun == null || hand.CurrentGunObject == null) { return; }

            if(hand.CurrentGunObject.TryShoot(_player, hand))
            {
                _armsAnimator.Play("Atack", 0, 0);

                var weaponRecoil = WeaponInventory[0].RecoilCameraRotation;

                _player.PlayerCamera.TriggerRecoil(weaponRecoil);
            }
        }

        public void Equip(GunData gunData, PlayerHand.Side side)
        {
            var hand = GetHandBySide(side);

            if(hand.CurrentGunObject != null)
            {
                hand.ClearHand();
            }

            hand.SetGun(gunData);

            BaseGun gunObject;

            gunObject = Instantiate(gunData.GunPrefab, hand.Socket.transform);
            gunObject.Equip(_player);

            hand.SetGunObject(gunObject);

            HasGun = true;

            _armsAnimator.runtimeAnimatorController = gunData.AnimatorController;
            _armsAnimator.Play("Draw", 0, 0);

            OnEquip?.Invoke(gunObject);
        }

        public void Dequip(PlayerHand.Side side)
        {
            var hand = GetHandBySide(side);

            HasGun = false;

            _armsAnimator.runtimeAnimatorController = _defaultAnimatorController;

            OnDequip?.Invoke(hand.CurrentGun);

            hand.ClearHand();
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