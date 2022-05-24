using UnityEngine;

namespace LOK1game.UI
{
    public class PlayerHud : MonoBehaviour
    {
        public static PlayerHud Instance
        {
            get => _instance;

            private set
            {
                if (_instance == null)
                {
                    _instance = value;
                }
                else if (_instance != value)
                {
                    Debug.LogWarning($"{nameof(PlayerHud)} instane already exist!");

                    Destroy(value);

                    Debug.Log($"Duplicate of {nameof(PlayerHud)} has been destroyed.");
                }
            }
        }

        private static PlayerHud _instance;

        private void Awake()
        {
            _instance = this;
        }
    }
}
