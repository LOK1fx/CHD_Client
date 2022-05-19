public static class Constants
{
    public static class Tags
    {
        public const string MAIN_CAMERA = "MainCamera";
        public const string RESPAWN = "Respawn";
        public const string FINISH = "Finish";
        public const string PLAYER = "Player";
        public const string GAME_CONTROLLER = "GameControlled";
        public const string EDITOR_ONLY = "EditorOnly";
        public const string UNTAGGED = "Untagged";
    }

    public static class Gameplay
    {
        public const int MAXIMUM_DAMAGE = 99999999;
        public const float PLAYER_HEIGHT = 1.71875f;
        public const int MAX_CAMERA_TIMER = 999;
    }

    public static class Network
    {
        public const int SERVER_PORT = 9600;
        public const int SERVER_TICK_RATE = 64;
        public const int SERVER_INPUT_BUFFER_SIZE = 1024;
        public const int CLIENT_INPUT_BUFFER_SIZE = 1024;
        public const string LOCAL_HOST = "127.0.0.1";
    }

    public static class Editor
    {
        public const string WEAPON_DATA_PATH = "Assets/_Game/Character/Player/Weapon/_Data";
        public const string WEAPON_PREFAB_PATH = "Assets/_Game/Character/Player/Weapon/_Guns";
        public const string APP_PATH = "Assets/_Game/GameSystem/App/Resources/[App].prefab";
        public const int APP_INSTANCE_ID = -1; //not currently definded
    }
}