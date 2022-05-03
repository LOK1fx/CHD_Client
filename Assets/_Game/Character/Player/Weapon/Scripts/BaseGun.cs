using UnityEngine;

namespace LOK1game.Weapon
{
    public abstract class BaseGun : MonoBehaviour
    {
        public bool InADS { get; protected set; }

        [SerializeField] protected GunData data;

        [Space]
        [SerializeField] protected Transform muzzleTransform;
        [SerializeField] protected Transform sightTransform;

        private float _timeToNextShoot;

        public bool TryShoot(Player.Player player, PlayerHand hand)
        {
            if (Time.time > _timeToNextShoot)
            {
                _timeToNextShoot = Time.time + 1f / data.FireRate;

                Shoot(player, hand);

                return true;
            }
            else
            {
                return false;
            }
        }

        public abstract void Shoot(Player.Player player, PlayerHand hand);

        public abstract void Equip(Player.Player player);

        public virtual void SetAdsStatus(Player.Player player, bool ads)
        {
            InADS = ads;

            UpdateAds(player);
        }

        public abstract void UpdateAds(Player.Player player);

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

        public Transform GetSightTransform()
        {
            return sightTransform;
        }
    }
}