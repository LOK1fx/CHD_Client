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
            _weaponName = EditorGUILayout.TextField("Weapon name: ", _weaponName);

            if(GUILayout.Button("Delete"))
            {
                if(EditorUtility.DisplayDialog("Are you sure that you wanna delete this weapon?",
                    "You will not have any chance to restore weapon.", "Yes.", "Cancel."))
                {
                    var dataPath = Constants.Editor.WEAPON_DATA_PATH + "/" + _weaponName + ".asset";
                    var prefabData = Constants.Editor.WEAPON_PREFAB_PATH + "/" + _weaponName;

                    AssetDatabase.DeleteAsset(dataPath);
                    AssetDatabase.DeleteAsset(prefabData);
                }
            }
        }
    }
}