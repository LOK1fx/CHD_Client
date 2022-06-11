using UnityEngine;
using LOK1game.Weapon;

namespace LOK1game.New.Networking
{
    [RequireComponent(typeof(NetworkWeaponInventory))]
    public class NetworkWorldPlayerWeaponSpawner : MonoBehaviour
    {
        [SerializeField] private LayerMask _weaponLayer;

        [Space]
        [SerializeField] private PlayerHand[] _hands = new PlayerHand[2];

        private NetworkWeaponInventory _inventory;

        private void Awake()
        {
            _inventory = GetComponent<NetworkWeaponInventory>();
        }

        private void Start()
        {
            _inventory.OnWeaponSwitch += SetWeapon;
        }

        public void SetWeapon(int index)
        {
            var id = _inventory.Weapons[index];
            var weapon = WeaponLibrary.GetWeaponData(id);

            EquipWeapon(weapon, weapon.Hand);
        }

        private void EquipWeapon(WeaponData data, PlayerHand.ESide side)
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

            foreach (var transform in weapon.GameObject.GetComponentsInChildren<Transform>())
            {
                transform.gameObject.layer = _weaponLayer.value;
            }

            hand.SetWeapon(weapon);
        }

        private PlayerHand GetHandBySide(PlayerHand.ESide side)
        {
            if(side == PlayerHand.ESide.Right)
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