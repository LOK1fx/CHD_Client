using LOK1game.Player;
using UnityEngine;
using UnityEngine.Events;

namespace LOK1game.Weapon
{
    public class Gun : BaseWeapon
    {
        [SerializeField] private Transform _muzzleTransform;

        protected override void Equip(Player.Player player)
        {
        }

        protected override void Atack(Player.Player player, PlayerHand hand)
        {
            for (int i = 0; i < data.BulletsPerShoot; i++)
            {
                var camera = player.Camera.GetRecoilCameraTransform();

                var shootTransform = camera.transform;
                var projectilePos = shootTransform.position;
                var direction = shootTransform.forward;

                if (data.ShootsFromMuzzle)
                {
                    shootTransform = _muzzleTransform;
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

                player.Camera.AddCameraOffset(camera.forward * data.ShootCameraOffset.z);
            }
        }

        protected override void Dequip(Player.Player player)
        {
        }
    }
}