using UnityEngine;
using UnityEditor;
using LOK1game.Weapon;

namespace LOK1game.Editor
{
    public enum EWeaponBaseScript
    {
        Gun,
        Drill,
        GroundCrystal,
    }

    public class EditorCreateWeaponWindow : EditorWindow
    {
        #region Paths

        private const string DATA_PATH = "Assets/_Game/Character/Player/Weapon/_Data";
        private const string PREFAB_PATH = "Assets/_Game/Character/Player/Weapon/_Guns";

        #endregion

        private string _weaponName = "new Weapon";
        private GameObject _weaponModel;
        private EWeaponType _weaponType = EWeaponType.Primary;
        private EWeaponBaseScript _weaponBaseScript;

        [MenuItem("LOK1game Tools/Create new weapon")]
        public static void ShowWindow()
        {
            GetWindow<EditorCreateWeaponWindow>("Create weapon");
        }

        private void OnGUI()
        {
            _weaponName = EditorGUILayout.TextField("Weapon name: ", _weaponName);
            _weaponModel = (GameObject)EditorGUILayout.ObjectField("Weapon model: ", _weaponModel, typeof(GameObject));
            _weaponType = (EWeaponType)EditorGUILayout.EnumPopup("Weapon type: ", _weaponType);
            _weaponBaseScript = (EWeaponBaseScript)EditorGUILayout.EnumPopup("Weapon base script: ", _weaponBaseScript);

            if(GUILayout.Button("Create weapon"))
            {
                var parent = new GameObject(_weaponName);
                var weapon = GetBaseWeaponClass(_weaponBaseScript, parent);
                Instantiate(_weaponModel, parent.transform);

                var data = CreateInstance<WeaponData>();
                data.name = _weaponName;

                weapon.SetData(data);
                data.SetData(_weaponType, weapon);

                AssetDatabase.CreateAsset(data, GetCurrentPath(DATA_PATH, false));
                AssetDatabase.CreateFolder(PREFAB_PATH, _weaponName);
                PrefabUtility.SaveAsPrefabAsset(parent, GetCurrentPath($"{PREFAB_PATH}/{_weaponName}", true));

                DestroyImmediate(parent);
            }
        }

        private string GetCurrentPath(string path, bool prefab)
        {
            if (prefab)
            {
                return $"{path}/{_weaponName}.prefab";
            }
            else
            {
                return $"{path}/{_weaponName}.asset";
            }
        }

        private BaseWeapon GetBaseWeaponClass(EWeaponBaseScript baseScript, GameObject parent)
        {
            switch (baseScript)
            {
                case EWeaponBaseScript.Gun:
                    return parent.AddComponent<Gun>();
                case EWeaponBaseScript.Drill:
                    return parent.AddComponent<PlayerDrillWeapon>();
                case EWeaponBaseScript.GroundCrystal:
                    return parent.AddComponent<GroundCrystalWeapon>();
                default:
                    return parent.AddComponent<Gun>();
            }
        }
    }
}