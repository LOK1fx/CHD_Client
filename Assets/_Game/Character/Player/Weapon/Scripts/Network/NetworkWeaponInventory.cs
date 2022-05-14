using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using RiptideNetworking;
using LOK1game.Weapon;

namespace LOK1game.New.Networking
{
    public class NetworkWeaponInventory : MonoBehaviour
    {
        [SerializeField] private List<EWeaponId> _weapons = new List<EWeaponId>();

        public void AddWeapon(EWeaponId id)
        {
            _weapons.Add(id);
        }
    }
}
