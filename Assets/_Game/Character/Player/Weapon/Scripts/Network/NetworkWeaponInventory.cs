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
        #region Events

        public event Action<int> OnWeaponSwitch;

        #endregion

        public List<EWeaponId> Weapons { get; private set; } = new List<EWeaponId>();

        [SerializeField] private UnityEvent<EWeaponId> _onAddWeapon;

        public int CurrentSlotIndex { get; private set; }

        public void AddWeapon(EWeaponId id)
        {
            Weapons.Add(id);

            _onAddWeapon?.Invoke(id);
        }

        public void SetSlot(int index)
        {
            CurrentSlotIndex = index;

            OnWeaponSwitch?.Invoke(CurrentSlotIndex);
        }
    }
}
