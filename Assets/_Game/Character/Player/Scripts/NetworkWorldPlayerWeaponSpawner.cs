using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LOK1game.Weapon;

namespace LOK1game.New.Networking
{
    public class NetworkWorldPlayerWeaponSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerHand[] _hands = new PlayerHand[2];

        public void SetWeapon(EWeaponId id)
        {
            var weapon = WeaponLibrary.GetWeaponData(id);

            EquipWeapon(weapon, weapon.Hand);
        }

        private void EquipWeapon(WeaponData data, PlayerHand.Side side)
        {
            var hand = GetHandBySide(side);

            if(hand.CurrentWeaponObject != null)
            {
                hand.ClearHand();
            }

            var weaponObject = Instantiate(data.GunPrefab.gameObject, hand.Socket);
            var weapon = new WeaponStruct()
            {
                Data = data,
                GameObject = weaponObject,
                Weapon = null,
            };

            hand.SetWeapon(weapon);
        }

        private PlayerHand GetHandBySide(PlayerHand.Side side)
        {
            if(side == PlayerHand.Side.Right)
            {
                return _hands[0];
            }
            else
            {
                return _hands[1];
            }
        }
    }
}