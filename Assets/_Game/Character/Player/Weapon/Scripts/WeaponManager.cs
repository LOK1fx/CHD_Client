using System;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game.Weapon
{
    [Serializable]
    public class WeaponManager
    {
        [SerializeField] private List<WeaponData> _list = new List<WeaponData>();

        public void Initialize()
        {
            if(WeaponLibrary.IsInitialized == false)
            {
                WeaponLibrary.Intialize(_list.ToArray());
            }
        }

#if UNITY_EDITOR

        public void Editor_AddWeapon(WeaponData data)
        {
            _list.Add(data);
        }

#endif
    }
}
