using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LOK1game.Weapon
{
    public abstract class BaseWeapon : Actor, IWeapon
    {
        #region Events

        public event Action OnWeaponAttack;
        public event Action OnWeaponEquip;
        public event Action OnWeaponDequip;

        #endregion

        public bool InADS { get; protected set; }

        [SerializeField] protected WeaponData data;

        private float _timeToNextShoot;

        public bool TryAttack(object sender, PlayerHand hand)
        {
            if(sender is Player.Player)
            {
                if (Time.time > _timeToNextShoot)
                {
                    _timeToNextShoot = Time.time + 1f / data.FireRate;

                    Atack(sender as Player.Player, hand);

                    OnWeaponAttack?.Invoke();

                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public void OnEquip(object sender)
        {
            if(sender is Player.Player)
            {
                Equip(sender as Player.Player);

                OnWeaponEquip?.Invoke();
            }
        }

        public void OnDequip(object sender)
        {
            if (sender is Player.Player)
            {
                Dequip(sender as Player.Player);

                OnWeaponDequip?.Invoke();
            }
        }

        protected abstract void Atack(Player.Player player, PlayerHand hand);

        protected abstract void Equip(Player.Player player);

        protected abstract void Dequip(Player.Player player);

        protected Vector3 GetBloom(Transform firePoint)
        {
            var bloom = firePoint.position + firePoint.forward * data.ShootDistance;

            bloom += CalculateBloom(firePoint.up) * data.BloomYMultiplier;
            bloom += CalculateBloom(firePoint.right) * data.BloomXMultiplier;
            bloom -= firePoint.position;

            return bloom.normalized;
        }

        private Vector3 CalculateBloom(Vector3 direction)
        {
            return Random.Range(-data.Bloom * 10f, data.Bloom * 10f) * direction;
        }

        public WeaponData GetData()
        {
            return data;
        }

#if UNITY_EDITOR

        public void Editor_SetData(WeaponData weaponData)
        {
            data = weaponData;
        }

#endif
    }
}