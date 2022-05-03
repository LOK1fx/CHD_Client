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

        public GunData CurrentGun { get; private set; }
        public BaseGun CurrentGunObject { get; private set; }

        public void SetGun(GunData data)
        {
            CurrentGun = data;
        }

        public void SetGunObject(BaseGun gun)
        {
            CurrentGunObject = gun;
        }

        public void ClearHand()
        {
            Destroy(CurrentGunObject.gameObject);

            CurrentGun = null;
            CurrentGunObject = null;
        }
    }
}