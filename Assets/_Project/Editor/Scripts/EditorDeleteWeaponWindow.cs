using UnityEditor;
using UnityEngine;

namespace LOK1game.Editor
{
    public class EditorDeleteWeaponWindow : BaseLOK1gameEditorWindow
    {
        private string _weaponName;

        [MenuItem(MENU_ITEM_NAME + "/Delete weapon")]
        public static void ShowWindow()
        {
            GetWindow<EditorDeleteWeaponWindow>("Delete weapon");
        }

        private void OnGUI()
        {
            GUILayout.Space(20f);

            _weaponName = EditorGUILayout.TextField("Weapon name: ", _weaponName);

            GUILayout.Space(20f);

            if (GUILayout.Button("Delete"))
            {
                if (string.IsNullOrEmpty(_weaponName) || _weaponName == "*")
                {
                    return;
                }

                if (EditorUtility.DisplayDialog("Weapon deletion",
                    "Are you sure that you wanna delete this weapon?" +
                    "You will not have any chance to restore it.", "Yes", "Cancel"))
                {
                    var dataPath = Constants.Editor.WEAPON_DATA_PATH + "/" + _weaponName + ".asset";
                    var prefabData = Constants.Editor.WEAPON_PREFAB_PATH + "/" + _weaponName;

                    AssetDatabase.DeleteAsset(dataPath);
                    AssetDatabase.DeleteAsset(prefabData);
                }
            }

            DrawLogo(50f);
        }
    }
}