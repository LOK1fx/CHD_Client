using UnityEngine;
using LOK1game.Player;
using LOK1game.Weapon;
using RiptideNetworking;

namespace LOK1game.New.Networking
{
    [RequireComponent(typeof(PlayerWeapon))]
    public class NetworkLocalPlayerWeapon : MonoBehaviour
    {
        private PlayerWeapon _weapon;

        private void Awake()
        {
            _weapon = GetComponent<PlayerWeapon>();
            _weapon.OnEquip += OnWeaponEquip;
        }

        private void OnWeaponEquip(WeaponData data)
        {
            var message = Message.Create(MessageSendMode.reliable, EClientToServerId.SwitchWeapon);
            var slotIndex = (int)data.Type - 1;

            message.AddInt(slotIndex);

            NetworkManager.Instance.Client.Send(message);
        }
    }
}