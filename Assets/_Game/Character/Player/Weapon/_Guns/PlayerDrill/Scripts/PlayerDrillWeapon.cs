using System.Collections;
using System.Collections.Generic;
using LOK1game.Player;
using UnityEngine;

namespace LOK1game.Weapon
{
    public class PlayerDrillWeapon : BaseGun
    {
        [SerializeField] private LayerMask _hitableLayer;

        public override void Equip(Player.Player player)
        {
            
        }

        public override void Shoot(Player.Player player, PlayerHand hand)
        {
            player.PlayerCamera.TriggerRecoil(data.RecoilCameraRotation);

            var camera = player.PlayerCamera.GetRecoilCameraTransform();

            if(Physics.Raycast(camera.position, camera.forward, out var hit, data.ShootDistance, _hitableLayer, QueryTriggerInteraction.Ignore))
            {
                if(hit.collider.gameObject.TryGetComponent<IDamagable>(out var damagable))
                {
                    var damage = new Damage(data.Damage, Damage.Type.Drill, player);

                    damagable.TakeDamage(damage);
                }
            }
        }

        public override void UpdateAds(Player.Player player)
        {
            
        }
    }
}