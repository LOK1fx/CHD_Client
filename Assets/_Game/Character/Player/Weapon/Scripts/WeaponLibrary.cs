using System.Collections.Generic;

namespace LOK1game.Weapon
{
    public enum EWeaponId : ushort
    {
        None = 1,
        DefAr,
        Revolver,
        Drill,
        GroundCrystal,
    }

    public static class WeaponLibrary
    {
        public static bool IsInitialized { get; private set; }

        private static Dictionary<EWeaponId, WeaponData> _weapons = new Dictionary<EWeaponId, WeaponData>();

        public static void Intialize(WeaponData[] datas)
        {
            if(IsInitialized)
            {
                throw new System.Exception("Weapon library is already initialized!");
            }

            foreach (var data in datas)
            {
                if(data.Id == EWeaponId.None)
                {
                    throw new System.Exception($"{data.name}'s id is seted to {EWeaponId.None}!");
                }

                _weapons.Add(data.Id, data);
            }

            IsInitialized = true;
        }

        public static WeaponData GetWeaponData(EWeaponId id)
        {
            if(id == EWeaponId.None)
            {
                throw new System.Exception($"{id} is not valid id!");
            }

            return _weapons[id];
        }

#if UNITY_EDITOR

        public static void Editor_AddWeapon(WeaponData data)
        {
            _weapons.Add(data.Id, data);
        }

#endif
    }
}