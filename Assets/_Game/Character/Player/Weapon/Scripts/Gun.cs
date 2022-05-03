using UnityEngine;
using UnityEngine.Events;

namespace LOK1game.Weapon
{
    public class Gun : BaseGun
    {
        public event UnityAction OnShoot;
        public event UnityAction OnEquip;

        [Space]
        [SerializeField] private Vector3 _adsGunPositon;

        private Vector3 _defaultGunPosition;
        private Quaternion _defaultGunRotation;

        private void Start()
        {
            _defaultGunPosition = sightTransform.localPosition;
            _defaultGunRotation = sightTransform.localRotation;
        }

        public override void Equip(Player.Player player)
        {
            OnEquip?.Invoke();
        }

        public override void UpdateAds(Player.Player player)
        {
            
        }

        public override void Shoot(Player.Player player, PlayerHand hand)
        {
            for (int i = 0; i < data.BulletsPerShoot; i++)
            {
                var camera = player.PlayerCamera.GetRecoilCameraTransform();

                var shootTransform = camera.transform;
                var projectilePos = shootTransform.position;
                var direction = shootTransform.forward;

                if (data.ShootsFromMuzzle)
                {
                    shootTransform = muzzleTransform;
                    projectilePos = shootTransform.position;
                    direction = shootTransform.forward;
                }

                var projectile = Instantiate(data.ProjectilePrefab, projectilePos, Quaternion.identity);

                if (i != 0)
                {
                    direction += GetBloom(shootTransform);
                }

                var damage = new Damage(data.Damage, player);

                projectile.Shoot(direction, data.StartBulletForce, damage);

                player.PlayerCamera.AddCameraOffset(camera.forward * hand.CurrentGun.ShootCameraOffset.z);

                OnShoot?.Invoke();
            }
        }
    }
}