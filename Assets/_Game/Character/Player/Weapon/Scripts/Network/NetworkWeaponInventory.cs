using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using RiptideNetworking;
using LOK1game.Weapon;

namespace LOK1game.New.Networking
{
    public class NetworkWeaponInventory : MonoBehaviour
    {
        public List<EWeaponId> Weapons { get; private set; } = new List<EWeaponId>();

        [SerializeField] private UnityEvent<EWeaponId> _onAddWeapon;

        public void AddWeapon(EWeaponId id)
        {
            Weapons.Add(id);

            _onAddWeapon?.Invoke(id);
        }
    }
}
