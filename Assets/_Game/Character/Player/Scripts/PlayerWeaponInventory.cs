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
    }
}