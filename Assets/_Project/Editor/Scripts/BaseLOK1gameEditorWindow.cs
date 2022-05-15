using UnityEngine;
using UnityEditor;

namespace LOK1game.Editor
{
    public class BaseLOK1gameEditorWindow : EditorWindow
    {
        #region Paths

        public const string MENU_ITEM_NAME = "LOK1game Tools";
        public const string BRAND_PATH = "Assets/_Project/Brand";
        public const string WEAPON_DATA_PATH = "Assets/_Game/Character/Player/Weapon/_Data";
        public const string WEAPON_PREFAB_PATH = "Assets/_Game/Character/Player/Weapon/_Guns";
        public const string APP_PATH = "Assets/_Game/GameSystem/App/Resources/[App].prefab";

        #endregion

        public Texture GetLogoTexture()
        {
            return AssetDatabase.LoadAssetAtPath<Texture>($"{BRAND_PATH}/Logo.png");
        }

        public App GetApp()
        {
            return AssetDatabase.LoadAssetAtPath<App>(APP_PATH);
        }
    }
}