using LOK1game.Weapon;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game.Player
{
    public class PlayerWeaponInventory : MonoBehaviour
    {
        public List<WeaponData> Weapons => _weapons;
        public List<WeaponData> Abilities => _abilities;
        public WeaponData Utility => _utility;

        [SerializeField] private List<WeaponData> _weapons;
        [SerializeField] private List<WeaponData> _abilities;
        [SerializeField] private WeaponData _utility;

        public void SetWeapon(EWeaponId id)
        {
            var weapon = WeaponLibrary.GetWeaponData(id);

            switch (weapon.Type)
            {
                case EWeaponType.Primary:
                    _weapons[0] = weapon;
                    break;
                case EWeaponType.Secondary:
                    _weapons[1] = weapon;
                    break;
                case EWeaponType.Ability:
                    _abilities[0] = weapon;
                    break;
                case EWeaponType.Utility:
                    _utility = weapon;
                    break;
            }
        }
    }
}