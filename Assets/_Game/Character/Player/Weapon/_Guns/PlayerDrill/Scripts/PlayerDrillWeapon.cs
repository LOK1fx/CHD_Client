using LOK1game.Player;
using UnityEngine;

namespace LOK1game.Weapon
{
    public class PlayerDrillWeapon : BaseWeapon
    {
        [SerializeField] private LayerMask _hitableLayer;

        protected override void Equip(Player.Player player)
        {
        }

        protected override void Atack(Player.Player player, PlayerHand hand)
        {
            var camera = player.Camera.GetRecoilCameraTransform();

            player.Camera.TriggerRecoil(data.RecoilCameraRotation);
            player.Camera.AddCameraOffset(camera.forward * data.ShootCameraOffset.z);

            if (Physics.Raycast(camera.position, camera.forward, out var hit, data.ShootDistance, _hitableLayer, QueryTriggerInteraction.Ignore))
            {
                if(hit.collider.gameObject.TryGetComponent<IDamagable>(out var damagable))
                {
                    var damage = new Damage(data.Damage, Damage.Type.Drill, player)
                    {
                        HitPoint = hit.point,
                        HitNormal = hit.normal
                    };

                    damagable.TakeDamage(damage);
                }
            }
        }

        protected override void Dequip(Player.Player player)
        {
        }
    }
}