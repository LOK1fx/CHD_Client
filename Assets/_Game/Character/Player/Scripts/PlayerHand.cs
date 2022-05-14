using LOK1game.Weapon;
using UnityEngine;

namespace LOK1game
{
    public class PlayerHand : MonoBehaviour
    {
        public enum Side
        {
            Right,
            Left
        }

        [SerializeField] private Transform _socket;

        public Transform Socket => _socket;

        [Space]
        [SerializeField] private Side _handSide;

        public Side HandSide => _handSide;

        public WeaponData CurrentWeaponData { get; private set; }
        public GameObject CurrentWeaponObject { get; private set; }
        public IWeapon CurrentWeapon { get; private set; }

        public void SetWeapon(WeaponStruct weapon)
        {
            CurrentWeaponData = weapon.Data;
            CurrentWeaponObject = weapon.GameObject;
            CurrentWeapon = weapon.Weapon;
        }

        public void ClearHand()
        {
            Destroy(CurrentWeaponObject.gameObject);

            CurrentWeaponData = null;
            CurrentWeaponObject = null;
            CurrentWeapon = null;
        }
    }
}